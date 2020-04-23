namespace VinylExchange.Web.Infrastructure.Hubs.SaleChat
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;
    using Models.ResourceModels.SaleMessages;
    using Models.Utility.Sales;
    using Services.Data.HelperServices.Sales.SaleMessages;
    using Services.Data.MainServices.Sales.Contracts;

    [Authorize]
    public class SaleChatHub : Hub<ISaleChatClient>
    {
        private readonly ISaleMessagesService saleMessagesService;

        private readonly ISalesService salesService;

        public SaleChatHub(ISalesService salesService, ISaleMessagesService saleMessagesService)
        {
            this.salesService = salesService;
            this.saleMessagesService = saleMessagesService;
        }

        public async Task JoinRoom(Guid saleId)
        {
            var roomName = saleId.ToString();

            var sale = await salesService.GetSale<GetSaleInfoUtilityModel>(saleId);

            var userId = Guid.Parse(GetUserId());

            if (sale != null)
            {
                if (sale.SellerId == userId ||
                    sale.BuyerId == userId)
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
                }
            }
        }

        public async Task LeaveRoom(string roomName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }

        public async Task LoadMessageHistory(Guid saleId)
        {
            var sale = await salesService.GetSale<GetSaleInfoUtilityModel>(saleId);

            var userId = Guid.Parse(GetUserId());

            if (sale != null)
            {
                if (sale.SellerId == userId ||
                    sale.BuyerId == userId)
                {
                    var messages =
                        await saleMessagesService.GetMessagesForSale<GetMessagesForSaleResourceModel>(saleId);

                    await Clients.Caller.LoadMessageHistory(messages);
                }
            }
        }

        public async Task SendMessage(Guid? saleId, string messageContent)
        {
            var roomName = saleId.ToString();

            var userId = Guid.Parse(GetUserId());

            var message =
                await saleMessagesService.AddMessageToSale<AddMessageToSaleResourceModel>(
                    saleId,
                    userId,
                    messageContent);

            await Clients.Group(roomName).NewMessage(message);
        }

        private string GetUserId()
        {
            return Context.User.FindFirst("sub").Value;
        }
    }
}
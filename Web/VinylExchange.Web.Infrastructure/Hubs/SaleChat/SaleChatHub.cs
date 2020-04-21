namespace VinylExchange.Web.Infrastructure.Hubs.SaleChat
{
    #region

    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;

    using VinylExchange.Services.Data.HelperServices.Sales.SaleMessages;
    using VinylExchange.Services.Data.MainServices.Sales;
    using VinylExchange.Services.Data.MainServices.Sales.Contracts;
    using VinylExchange.Web.Models.ResourceModels.SaleMessages;
    using VinylExchange.Web.Models.Utility.Sales;

    #endregion

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

            var sale = await this.salesService.GetSale<GetSaleInfoUtilityModel>(saleId);

            var userId = Guid.Parse(this.GetUserId());

            if (sale != null)
            {
                if (sale.SellerId == userId
                    || sale.BuyerId == userId)
                {
                    await this.Groups.AddToGroupAsync(this.Context.ConnectionId, roomName);
                }
            }
        }

        public async Task LeaveRoom(string roomName)
        {
            await this.Groups.RemoveFromGroupAsync(this.Context.ConnectionId, roomName);
        }

        public async Task LoadMessageHistory(Guid saleId)
        {
            var sale = await this.salesService.GetSale<GetSaleInfoUtilityModel>(saleId);

            var userId = Guid.Parse(this.GetUserId());

            if (sale != null)
            {
                if (sale.SellerId == userId
                    || sale.BuyerId == userId)
                {
                    var messages =
                        await this.saleMessagesService.GetMessagesForSale<GetMessagesForSaleResourceModel>(saleId);

                    await this.Clients.Caller.LoadMessageHistory(messages);
                }
            }
        }

        public async Task SendMessage(Guid? saleId, string messageContent)
        {
            var roomName = saleId.ToString();

            var userId = Guid.Parse(this.GetUserId());

            var message =
                await this.saleMessagesService.AddMessageToSale<AddMessageToSaleResourceModel>(
                    saleId,
                    userId,
                    messageContent);

            await this.Clients.Group(roomName).NewMessage(message);
        }

        private string GetUserId()
        {
            return this.Context.User.FindFirst("sub").Value;
        }
    }
}
namespace VinylExchange.Hubs
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;

    using VinylExchange.Models.ResourceModels.SaleMessages;
    using VinylExchange.Models.Utility;
    using VinylExchange.Services.Data.HelperServices.Sales;
    using VinylExchange.Services.Data.MainServices.Sales;

    [Authorize]
    public class SaleChatHub : Hub
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
            string roomName = saleId.ToString();

            GetSaleInfoUtilityModel sale = await this.salesService.GetSaleInfo(saleId);

            Guid userId = Guid.Parse(this.GetUserId());

            if (sale != null)
            {
                if (sale.SellerId == userId || sale.BuyerId == userId)
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
            GetSaleInfoUtilityModel sale = await this.salesService.GetSaleInfo(saleId);

            Guid userId = Guid.Parse(this.GetUserId());

            if (sale != null)
            {
                if (sale.SellerId == userId || sale.BuyerId == userId)
                {
                    IEnumerable<GetMessagesForSaleResourceModel> messages =
                        await this.saleMessagesService.GetMessagesForSale(saleId);

                    await this.Clients.Caller.SendAsync("LoadMessageHistory", messages);
                }
            }
        }

        public async Task SendMessage(Guid saleId, string messageContent)
        {
            string roomName = saleId.ToString();

            Guid userId = Guid.Parse(this.GetUserId());

            AddMessageToSaleResourceModel message =
                await this.saleMessagesService.AddMessageToSale(saleId, userId, messageContent);

            await this.Clients.Group(roomName).SendAsync("NewMessage", message);
        }

        private string GetUserId()
        {
            return this.Context.User.FindFirst("sub").Value;
        }
    }
}
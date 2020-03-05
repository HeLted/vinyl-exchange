using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using VinylExchange.Services.Data.HelperServices.Sales;
using VinylExchange.Services.Data.MainServices.Sales;

namespace VinylExchange.Hubs
{
    [Authorize]
    public class SaleChatHub : Hub
    {
        private readonly ISalesService salesService;
        private readonly ISaleMessagesService saleMessagesService;

        public SaleChatHub(ISalesService salesService, ISaleMessagesService saleMessagesService)
        {
            this.salesService = salesService;
            this.saleMessagesService = saleMessagesService;
        }

        public async Task JoinRoom(Guid saleId)
        {
            var roomName = saleId.ToString();

            var sale = await salesService.GetSaleInfo(saleId);

            var userId = Guid.Parse(this.GetUserId());

            if (sale != null)
            {
                if (sale.SellerId == userId || sale.BuyerId == userId)
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
                }
            }

        }

        public async Task LeaveRoom(string roomName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }

        public async Task SendMessage(Guid saleId, string messageContent)
        {
            var roomName = saleId.ToString();

            var userId = Guid.Parse(this.GetUserId());

            var message = await this.saleMessagesService.AddMessageToSale(saleId,userId, messageContent);

            await this.Clients.Group(roomName).SendAsync("NewMessage", message);
        }

        public async Task LoadMessageHistory(Guid saleId)
        {
            var sale = await salesService.GetSaleInfo(saleId);

            var userId = Guid.Parse(this.GetUserId());

            if (sale != null)
            {
                if (sale.SellerId == userId || sale.BuyerId == userId)
                {
                    var messages = await this.saleMessagesService.GetMessagesForSale(saleId);

                    await this.Clients.Caller.SendAsync("LoadMessageHistory", messages);
                }
            }
            
        }

        private string GetUserId()
        {
            return this.Context.User.FindFirst("sub").Value;
        }
    }
}

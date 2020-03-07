namespace VinylExchange.Web.Hubs
{
    using Microsoft.AspNetCore.Authorization;
    using System;
    using System.Threading.Tasks;
    using VinylExchange.Services.Data.HelperServices.Sales;
    using VinylExchange.Services.Data.MainServices.Sales;
    using VinylExchange.Web.Models.Utility;

    [Authorize]
    public class SaleLogHub : BaseHub
    {
        private readonly ISalesService salesService;
        private readonly ISaleMessagesService saleMessagesService;

        public SaleLogHub(ISalesService salesService, ISaleMessagesService saleMessagesService)
        {
            this.salesService = salesService;       
            this.saleMessagesService = saleMessagesService;
        }

        public async Task SubscribeToLog(Guid saleId)
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
    }
}

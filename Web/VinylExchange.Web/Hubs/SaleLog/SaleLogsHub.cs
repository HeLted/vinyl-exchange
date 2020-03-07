namespace VinylExchange.Web.Hubs.SaleLog
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;
    using VinylExchange.Services.Data.HelperServices.Sales.SaleMessages;
    using VinylExchange.Services.Data.MainServices.Sales;
    using VinylExchange.Web.Models.Utility;

    [Authorize]
    public class SaleLogsHub : Hub<ISaleLogsClient>
    {
        private readonly ISaleMessagesService saleMessagesService;

        private readonly ISalesService salesService;

        public SaleLogsHub(ISalesService salesService, ISaleMessagesService saleMessagesService)
        {
            this.salesService = salesService;
            this.saleMessagesService = saleMessagesService;
        }

        public async Task SubscribeToLog(Guid saleId)
        {
            var subscriberGroupName = saleId.ToString();

            GetSaleInfoUtilityModel sale = await this.salesService.GetSaleInfo(saleId);

            Guid userId = Guid.Parse(this.GetUserId());

            if (sale != null)
            {
                if (sale.SellerId == userId || sale.BuyerId == userId)
                {
                    await this.Groups.AddToGroupAsync(this.Context.ConnectionId, subscriberGroupName);
                }
            }
        }

        private string GetUserId()
        {
            return this.Context.User.FindFirst("sub").Value;
        }
    }
}
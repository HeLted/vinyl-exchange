namespace VinylExchange.Web.Infrastructure.Hubs.SaleLog
{
    #region

    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;

    using VinylExchange.Services.Data.HelperServices.Sales.SaleLogs;
    using VinylExchange.Services.Data.MainServices.Sales;
    using VinylExchange.Web.Models.Utility.Sales;

    #endregion

    [Authorize]
    public class SaleLogsHub : Hub<ISaleLogsClient>
    {
        private readonly ISaleLogsService saleLogsService;

        private readonly ISalesService salesService;

        public SaleLogsHub(ISalesService salesService, ISaleLogsService saleLogsService)
        {
            this.salesService = salesService;
            this.saleLogsService = saleLogsService;
        }

        public async Task LoadLogHistory(Guid saleId)
        {
            var sale = await this.salesService.GetSale<GetSaleInfoUtilityModel>(saleId);

            var userId = Guid.Parse(this.GetUserId());

            if (sale != null)
            {
                if (sale.SellerId == userId || sale.BuyerId == userId)
                {
                    var logs = await this.saleLogsService.GetLogsForSale(saleId);

                    await this.Clients.Caller.LoadLogHistory(logs);
                }
            }
        }

        public async Task SubscribeToLog(Guid saleId)
        {
            var subscriberGroupName = saleId.ToString();

            var sale = await this.salesService.GetSale<GetSaleInfoUtilityModel>(saleId);

            var userId = Guid.Parse(this.GetUserId());

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
namespace VinylExchange.Web.Infrastructure.Hubs.SaleLog
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;
    using Models.ResourceModels.SaleLogs;
    using Models.Utility.Sales;
    using Services.Data.HelperServices.Sales.SaleLogs;
    using Services.Data.MainServices.Sales.Contracts;

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
            var sale = await salesService.GetSale<GetSaleInfoUtilityModel>(saleId);

            var userId = Guid.Parse(GetUserId());

            if (sale != null)
            {
                if (sale.SellerId == userId ||
                    sale.BuyerId == userId)
                {
                    var logs = await saleLogsService.GetLogsForSale<GetLogsForSaleResourceModel>(saleId);

                    await Clients.Caller.LoadLogHistory(logs);
                }
            }
        }

        public async Task SubscribeToLog(Guid saleId)
        {
            var subscriberGroupName = saleId.ToString();

            var sale = await salesService.GetSale<GetSaleInfoUtilityModel>(saleId);

            var userId = Guid.Parse(GetUserId());

            if (sale != null)
            {
                if (sale.SellerId == userId ||
                    sale.BuyerId == userId)
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, subscriberGroupName);
                }
            }
        }

        private string GetUserId()
        {
            return Context.User.FindFirst("sub").Value;
        }
    }
}
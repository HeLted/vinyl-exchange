namespace VinylExchange.Web.Infrastructure.Hubs.SaleLog
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.ResourceModels.SaleLogs;

    public interface ISaleLogsClient
    {
        Task LoadLogHistory(IEnumerable<GetLogsForSaleResourceModel> messages);

        Task RecieveLogNotification(string notificationContent);
    }
}
namespace VinylExchange.Web.Hubs.SaleLog
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VinylExchange.Web.Models.ResourceModels.SaleLogs;

    public interface ISaleLogsClient
    {
        Task LoadLogHistory(IEnumerable<GetLogsForSaleResourceModel> messages);

        Task RecieveLogNotification(string notificationContent);
    }
}
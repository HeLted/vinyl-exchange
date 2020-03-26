namespace VinylExchange.Web.Infrastructure.Hubs.SaleLog
{
    #region

    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VinylExchange.Web.Models.ResourceModels.SaleLogs;

    #endregion

    public interface ISaleLogsClient
    {
        Task LoadLogHistory(IEnumerable<GetLogsForSaleResourceModel> messages);

        Task RecieveLogNotification(string notificationContent);
    }
}
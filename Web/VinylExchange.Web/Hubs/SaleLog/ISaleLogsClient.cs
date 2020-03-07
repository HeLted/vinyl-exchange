namespace VinylExchange.Web.Hubs.SaleLog
{
    using System.Threading.Tasks;

    public interface ISaleLogsClient
    {
        Task RecieveNewLog(string logContent);
    }
}
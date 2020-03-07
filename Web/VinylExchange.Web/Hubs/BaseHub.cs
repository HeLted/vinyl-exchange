using Microsoft.AspNetCore.SignalR;

namespace VinylExchange.Web.Hubs
{
    public class BaseHub : Hub
    {
        protected string GetUserId()
        {
            return this.Context.User.FindFirst("sub").Value;
        }
    }
}

namespace VinylExchange.Services.Data.MainServices.Users.Contracts
{
    using System;
    using System.Threading.Tasks;
    using VinylExchange.Data.Models;

    public interface IUsersEntityRetriever
    {
        Task<VinylExchangeUser> GetUser(Guid? userId);
    }
}
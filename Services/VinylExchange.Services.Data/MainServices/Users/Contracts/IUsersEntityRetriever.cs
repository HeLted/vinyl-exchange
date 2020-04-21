﻿namespace VinylExchange.Services.Data.MainServices.Users.Contracts
{
    #region

    using System;
    using System.Threading.Tasks;

    using VinylExchange.Data.Models;

    #endregion

    public interface IUsersEntityRetriever
    {
        Task<VinylExchangeUser> GetUser(Guid? userId);
    }
}
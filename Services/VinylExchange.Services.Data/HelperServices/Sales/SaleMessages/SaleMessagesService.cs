namespace VinylExchange.Services.Data.HelperServices.Sales.SaleMessages
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Data.MainServices.Sales;
    using VinylExchange.Services.Data.MainServices.Sales.Contracts;
    using VinylExchange.Services.Data.MainServices.Users;
    using VinylExchange.Services.Data.MainServices.Users.Contracts;
    using VinylExchange.Services.Mapping;
    using VinylExchange.Web.Models.ResourceModels.SaleMessages;

    using static VinylExchange.Common.Constants.NullReferenceExceptionsConstants;

    #endregion

    public class SaleMessagesService : ISaleMessagesService
    {
        private readonly VinylExchangeDbContext dbContext;

        private readonly ISalesEntityRetriever salesEntityRetriever;

        private readonly IUsersEntityRetriever usersEntityRetriever;

        public SaleMessagesService(
            VinylExchangeDbContext dbContext,
            ISalesEntityRetriever salesEntityRetriever,
            IUsersEntityRetriever usersEntityRetriever)
        {
            this.dbContext = dbContext;
            this.salesEntityRetriever = salesEntityRetriever;
            this.usersEntityRetriever = usersEntityRetriever;
        }

        public async Task<TModel> AddMessageToSale<TModel>(Guid? saleId, Guid? userId, string message)
        {
            var user = await this.usersEntityRetriever.GetUser(userId);

            var sale = await this.salesEntityRetriever.GetSale(saleId);

            if (user == null)
            {
                throw new NullReferenceException(UserNotFound);
            }

            if (sale == null)
            {
                throw new NullReferenceException(SaleNotFound);
            }

            var saleMessage =
                (await this.dbContext.SaleMessages.AddAsync(
                     new SaleMessage { Content = message, SaleId = saleId, UserId = userId })).Entity
                .To<AddMessageToSaleResourceModel>();

            await this.dbContext.SaveChangesAsync();

            return saleMessage.To<TModel>();
        }

        public async Task<int> ClearSaleMessages(Guid? saleId)
        {
            var sale = await this.salesEntityRetriever.GetSale(saleId);

            if (sale == null)
            {
                throw new NullReferenceException(SaleNotFound);
            }

            var saleMessages = this.dbContext.SaleMessages.Where(sm => sm.SaleId == sale.Id).ToList();

            var messagesToBeClearedNumber = saleMessages.Count;

            this.dbContext.SaleMessages.RemoveRange(saleMessages);

            await this.dbContext.SaveChangesAsync();

            return messagesToBeClearedNumber;
        }

        public async Task<IEnumerable<TModel>> GetMessagesForSale<TModel>(Guid? saleId)
        {
            return await this.dbContext.SaleMessages.Where(sm => sm.SaleId == saleId).OrderBy(sm => sm.CreatedOn)
                       .To<TModel>().ToListAsync();
        }
    }
}
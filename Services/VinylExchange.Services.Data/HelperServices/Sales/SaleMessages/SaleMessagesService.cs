namespace VinylExchange.Services.Data.HelperServices.Sales.SaleMessages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MainServices.Sales.Contracts;
    using MainServices.Users.Contracts;
    using Mapping;
    using Microsoft.EntityFrameworkCore;
    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using Web.Models.ResourceModels.SaleMessages;
    using static Common.Constants.NullReferenceExceptionsConstants;


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
            var user = await usersEntityRetriever.GetUser(userId);

            var sale = await salesEntityRetriever.GetSale(saleId);

            if (user == null)
            {
                throw new NullReferenceException(UserNotFound);
            }

            if (sale == null)
            {
                throw new NullReferenceException(SaleNotFound);
            }

            var saleMessage =
                (await dbContext.SaleMessages.AddAsync(
                    new SaleMessage {Content = message, SaleId = saleId, UserId = userId})).Entity
                .To<AddMessageToSaleResourceModel>();

            await dbContext.SaveChangesAsync();

            return saleMessage.To<TModel>();
        }

        public async Task<int> ClearSaleMessages(Guid? saleId)
        {
            var sale = await salesEntityRetriever.GetSale(saleId);

            if (sale == null)
            {
                throw new NullReferenceException(SaleNotFound);
            }

            var saleMessages = dbContext.SaleMessages.Where(sm => sm.SaleId == sale.Id).ToList();

            var messagesToBeClearedNumber = saleMessages.Count;

            dbContext.SaleMessages.RemoveRange(saleMessages);

            await dbContext.SaveChangesAsync();

            return messagesToBeClearedNumber;
        }

        public async Task<IEnumerable<TModel>> GetMessagesForSale<TModel>(Guid? saleId)
        {
            return await dbContext.SaleMessages.Where(sm => sm.SaleId == saleId).OrderBy(sm => sm.CreatedOn)
                .To<TModel>().ToListAsync();
        }
    }
}
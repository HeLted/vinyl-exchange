namespace VinylExchange.Services.Data.HelperServices.Sales
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;
    using VinylExchange.Web.Models.ResourceModels.SaleMessages;

    public class SaleMessagesService : ISaleMessagesService
    {
        private readonly VinylExchangeDbContext dbContext;

        public SaleMessagesService(VinylExchangeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<AddMessageToSaleResourceModel> AddMessageToSale(Guid saleId, Guid userId, string message)
        {
            AddMessageToSaleResourceModel saleMessage =
                (await this.dbContext.SaleMessages.AddAsync(
                     new SaleMessage() { Content = message, SaleId = saleId, UserId = userId })).Entity
                .To<AddMessageToSaleResourceModel>();

            await this.dbContext.SaveChangesAsync();

            return saleMessage;
        }

        public async Task<IEnumerable<GetMessagesForSaleResourceModel>> GetMessagesForSale(Guid saleId)
        {
            return await this.dbContext.SaleMessages.Where(sm => sm.SaleId == saleId).OrderBy(sm => sm.CreatedOn)
                       .To<GetMessagesForSaleResourceModel>().ToListAsync();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylExchange.Data;
using VinylExchange.Data.Common.Enumerations;
using VinylExchange.Data.Models;
using VinylExchange.Models.InputModels.Sales;
using VinylExchange.Models.ResourceModels.Sales;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Services.Data.MainServices.Sales
{

    public class SalesService : ISalesService
    {
        private readonly VinylExchangeDbContext dbContext;

        public SalesService(VinylExchangeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<GetSaleResourceModel> GetSale(Guid saleId)
        => await this.dbContext.Sales
                 .Where(s => s.Id == saleId)
                 .To<GetSaleResourceModel>()
                 .FirstOrDefaultAsync();



        public async Task<Sale> CreateSale(CreateSaleInputModel inputModel,Guid sellerId)
        {
            inputModel.SellerId = sellerId;

            var sale = inputModel.To<Sale>();

            var trackedSale = await this.dbContext.Sales.AddAsync(sale);

            await this.dbContext.SaveChangesAsync();

            return trackedSale.Entity;

        }

        public async Task<IEnumerable<GetAllSalesForReleaseResouceModel>> GetAllSalesForRelease(Guid releaseId)
            => await this.dbContext.Sales
            .Where(s => s.ReleaseId == releaseId)
            .Where(s=> s.Status == Status.Open)
            .To<GetAllSalesForReleaseResouceModel>()
            .ToListAsync();

    }
}

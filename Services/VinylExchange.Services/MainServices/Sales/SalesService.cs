using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using VinylExchange.Data;
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
        {
            return await this.dbContext.Sales
                 .Where(s => s.Id == saleId)
                 .To<GetSaleResourceModel>()
                 .FirstOrDefaultAsync();

        }

    }
}

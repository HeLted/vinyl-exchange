using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using VinylExchange.Common.Constants;
using VinylExchange.Common.Enumerations;
using VinylExchange.Data;
using VinylExchange.Data.Models;
using VinylExchange.Services.Data.HelperServices.Sales.SaleLogs;
using VinylExchange.Services.Data.Tests.TestFactories;
using VinylExchange.Web.Models.ResourceModels.SaleLogs;
using Xunit;

namespace VinylExchange.Services.Data.Tests
{
    public class SaleLogsServiceTests
    {
        private readonly VinylExchangeDbContext dbContext;

        private readonly ISaleLogsService saleLogsService;

        public SaleLogsServiceTests()
        {
            this.dbContext = DbFactory.CreateDbContext();

            this.saleLogsService = new SaleLogsService(dbContext);
        }

        [Fact]
        public async Task AddTogToSaleShouldAddLogToSale()
        {
            var sale = new Sale();

            await this.dbContext.Sales.AddAsync(sale);

            await this.dbContext.SaveChangesAsync();

            await this.saleLogsService.AddLogToSale<AddLogToSaleResourceModel>(sale.Id, SaleLogs.ItemRecieved);

            var log = await this.dbContext.SaleLogs.FirstOrDefaultAsync(sl => sl.SaleId == sale.Id);

            Assert.NotNull(log);
        }

        [Theory]
        [InlineData(SaleLogs.ItemRecieved)]
        [InlineData(SaleLogs.ItemSent)]
        [InlineData(SaleLogs.Paid)]
        [InlineData(SaleLogs.PlacedOrder)]
        [InlineData(SaleLogs.SaleEdited)]
        [InlineData(SaleLogs.SettedShippingPrice)]

        public async Task AddTogToSaleShouldAddLogToSaleWithCorrectLogMessageAndCorrectSaleId(SaleLogs logType)
        {
            var sale = new Sale();

            await this.dbContext.Sales.AddAsync(sale);

            await this.dbContext.SaveChangesAsync();

            await this.saleLogsService.AddLogToSale<AddLogToSaleResourceModel>(sale.Id, logType);

            var log = await this.dbContext.SaleLogs.FirstOrDefaultAsync(sl => sl.SaleId == sale.Id);

            var saleLogMessagesMessagesConstantField = typeof(SaleLogsMessages).GetFields(BindingFlags.Public | BindingFlags.Static |
               BindingFlags.FlattenHierarchy)
               .Where(fi => fi.IsLiteral && !fi.IsInitOnly).First(fi=> fi.Name == logType.ToString());

            if (saleLogMessagesMessagesConstantField != null)
            {
                  Assert.Equal((string)saleLogMessagesMessagesConstantField.GetRawConstantValue(), log.Content);
            }
            else
            {
                throw new NullReferenceException("Provided enum value has no correspodning logType message in SaleLogsMessages!");
            }

        }

    }
}

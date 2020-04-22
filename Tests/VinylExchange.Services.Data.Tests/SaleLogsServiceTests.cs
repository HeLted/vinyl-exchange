namespace VinylExchange.Services.Data.Tests
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Moq;

    using VinylExchange.Common.Constants;
    using VinylExchange.Common.Enumerations;
    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Data.HelperServices.Sales.SaleLogs;
    using VinylExchange.Services.Data.MainServices.Sales.Contracts;
    using VinylExchange.Services.Data.Tests.TestFactories;
    using VinylExchange.Web.Models.ResourceModels.SaleLogs;

    using Xunit;

    using static VinylExchange.Common.Constants.NullReferenceExceptionsConstants;

    #endregion

    public class SaleLogsServiceTests
    {
        private readonly VinylExchangeDbContext dbContext;

        private readonly ISaleLogsService saleLogsService;

        private readonly Mock<ISalesEntityRetriever> salesEntityRetrieverMock;

        public SaleLogsServiceTests()
        {
            this.dbContext = DbFactory.CreateDbContext();

            this.salesEntityRetrieverMock = new Mock<ISalesEntityRetriever>();

            this.saleLogsService = new SaleLogsService(this.dbContext, this.salesEntityRetrieverMock.Object);
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

            var saleLogMessagesMessagesConstantField = typeof(SaleLogsMessages)
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly).First(fi => fi.Name == logType.ToString());

            if (saleLogMessagesMessagesConstantField != null)
            {
                Assert.Equal((string)saleLogMessagesMessagesConstantField.GetRawConstantValue(), log.Content);
            }
            else
            {
                throw new NullReferenceException(
                    "Provided enum value has no correspodning logType message in SaleLogsMessages!");
            }
        }

        [Fact]
        public async Task ClearSaleLogsShouldRemoveSaleLogsFromDb()
        {
            var sale = new Sale();

            this.salesEntityRetrieverMock.Setup(x => x.GetSale(It.IsAny<Guid?>())).ReturnsAsync(sale);

            for (var i = 0; i < 10; i++)
            {
                await this.dbContext.SaleLogs.AddAsync(new SaleLog { SaleId = sale.Id });
            }

            await this.dbContext.SaveChangesAsync();

            await this.saleLogsService.ClearSaleLogs(sale.Id);

            var logs = await this.dbContext.SaleLogs.Where(sl => sl.SaleId == sale.Id).ToListAsync();

            Assert.True(logs.Count == 0);
        }

        [Fact]
        public async Task ClearSaleLogsShouldThrowNullReferenceExceptionIfSaleIsIdIsNotPresentInDb()
        {
            this.salesEntityRetrieverMock.Setup(x => x.GetSale(It.IsAny<Guid?>())).ReturnsAsync((Sale)null);

            var exception = await Assert.ThrowsAsync<NullReferenceException>(
                                async () => await this.saleLogsService.ClearSaleLogs(Guid.NewGuid()));

            Assert.Equal(SaleNotFound, exception.Message);
        }

        [Fact]
        public async Task GetLogsForSaleShouldGetLogsForSale()
        {
            var sale = new Sale();

            this.salesEntityRetrieverMock.Setup(x => x.GetSale(It.IsAny<Guid?>())).ReturnsAsync(sale);

            var addedSaleLogsIds = new List<Guid?>();

            for (var i = 0; i < 10; i++)
            {
                var saleLog = new SaleLog { SaleId = sale.Id };

                await this.dbContext.SaleLogs.AddAsync(saleLog);

                addedSaleLogsIds.Add(saleLog.Id);
            }

            await this.dbContext.SaveChangesAsync();

            var saleLogs = await this.saleLogsService.GetLogsForSale<GetLogsForSaleResourceModel>(sale.Id);

            Assert.True(saleLogs.Select(sl => addedSaleLogsIds.Contains(sl.Id)).All(x => x));
        }

        [Fact]
        public async Task GetLogsForSaleShoulReturnEmptyListIfThereIsNoSaleLogsForSale()
        {
            var sale = new Sale();

            this.salesEntityRetrieverMock.Setup(x => x.GetSale(It.IsAny<Guid?>())).ReturnsAsync(sale);

            var saleLogs = await this.saleLogsService.GetLogsForSale<GetLogsForSaleResourceModel>(sale.Id);

            Assert.True(saleLogs.Count() == 0);
        }
    }
}
namespace VinylExchange.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Common.Constants;
    using Common.Enumerations;
    using HelperServices.Sales.SaleLogs;
    using MainServices.Sales.Contracts;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using TestFactories;
    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using Web.Models.ResourceModels.SaleLogs;
    using Xunit;
    using static Common.Constants.NullReferenceExceptionsConstants;


    public class SaleLogsServiceTests
    {
        private readonly VinylExchangeDbContext dbContext;

        private readonly ISaleLogsService saleLogsService;

        private readonly Mock<ISalesEntityRetriever> salesEntityRetrieverMock;

        public SaleLogsServiceTests()
        {
            dbContext = DbFactory.CreateDbContext();

            salesEntityRetrieverMock = new Mock<ISalesEntityRetriever>();

            saleLogsService = new SaleLogsService(dbContext, salesEntityRetrieverMock.Object);
        }

        [Fact]
        public async Task AddTogToSaleShouldAddLogToSale()
        {
            var sale = new Sale();

            await dbContext.Sales.AddAsync(sale);

            await dbContext.SaveChangesAsync();

            await saleLogsService.AddLogToSale<AddLogToSaleResourceModel>(sale.Id, SaleLogs.ItemRecieved);

            var log = await dbContext.SaleLogs.FirstOrDefaultAsync(sl => sl.SaleId == sale.Id);

            Assert.NotNull(log);
        }

        [Fact]
        public async Task AddTogToSaleShouldThrowNullReferenceExceptionIfSaleIsNotInDb()
        {
            var exception = await Assert.ThrowsAsync<NullReferenceException>(async () =>
                await saleLogsService.AddLogToSale<AddLogToSaleResourceModel>(Guid.NewGuid(), SaleLogs.ItemRecieved));

            Assert.Equal(SaleNotFound, exception.Message);
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

            await dbContext.Sales.AddAsync(sale);

            await dbContext.SaveChangesAsync();

            await saleLogsService.AddLogToSale<AddLogToSaleResourceModel>(sale.Id, logType);

            var log = await dbContext.SaleLogs.FirstOrDefaultAsync(sl => sl.SaleId == sale.Id);

            var saleLogMessagesMessagesConstantField = typeof(SaleLogsMessages)
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly).First(fi => fi.Name == logType.ToString());

            if (saleLogMessagesMessagesConstantField != null)
            {
                Assert.Equal((string) saleLogMessagesMessagesConstantField.GetRawConstantValue(), log.Content);
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

            salesEntityRetrieverMock.Setup(x => x.GetSale(It.IsAny<Guid?>())).ReturnsAsync(sale);

            for (var i = 0; i < 10; i++)
            {
                await dbContext.SaleLogs.AddAsync(new SaleLog {SaleId = sale.Id});
            }

            await dbContext.SaveChangesAsync();

            await saleLogsService.ClearSaleLogs(sale.Id);

            var logs = await dbContext.SaleLogs.Where(sl => sl.SaleId == sale.Id).ToListAsync();

            Assert.True(logs.Count == 0);
        }

        [Fact]
        public async Task ClearSaleLogsShouldThrowNullReferenceExceptionIfSaleIsIdIsNotPresentInDb()
        {
            salesEntityRetrieverMock.Setup(x => x.GetSale(It.IsAny<Guid?>())).ReturnsAsync((Sale) null);

            var exception = await Assert.ThrowsAsync<NullReferenceException>(
                async () => await saleLogsService.ClearSaleLogs(Guid.NewGuid()));

            Assert.Equal(SaleNotFound, exception.Message);
        }

        [Fact]
        public async Task GetLogsForSaleShouldGetLogsForSale()
        {
            var sale = new Sale();

            salesEntityRetrieverMock.Setup(x => x.GetSale(It.IsAny<Guid?>())).ReturnsAsync(sale);

            var addedSaleLogsIds = new List<Guid?>();

            for (var i = 0; i < 10; i++)
            {
                var saleLog = new SaleLog {SaleId = sale.Id};

                await dbContext.SaleLogs.AddAsync(saleLog);

                addedSaleLogsIds.Add(saleLog.Id);
            }

            await dbContext.SaveChangesAsync();

            var saleLogs = await saleLogsService.GetLogsForSale<GetLogsForSaleResourceModel>(sale.Id);

            Assert.True(saleLogs.Select(sl => addedSaleLogsIds.Contains(sl.Id)).All(x => x));
        }

        [Fact]
        public async Task GetLogsForSaleShoulReturnEmptyListIfThereIsNoSaleLogsForSale()
        {
            var sale = new Sale();

            salesEntityRetrieverMock.Setup(x => x.GetSale(It.IsAny<Guid?>())).ReturnsAsync(sale);

            var saleLogs = await saleLogsService.GetLogsForSale<GetLogsForSaleResourceModel>(sale.Id);

            Assert.True(saleLogs.Count() == 0);
        }
    }
}
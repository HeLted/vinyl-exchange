namespace VinylExchange.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using HelperServices.Sales.SaleMessages;
    using MainServices.Sales.Contracts;
    using MainServices.Users.Contracts;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using TestFactories;
    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using Web.Models.ResourceModels.SaleMessages;
    using Xunit;
    using static Common.Constants.NullReferenceExceptionsConstants;


    public class SaleMessagesServiceTests
    {
        private readonly VinylExchangeDbContext dbContext;

        private readonly ISaleMessagesService saleMessagesService;

        private readonly Mock<ISalesEntityRetriever> salesEntityRetrieverMock;

        private readonly Mock<IUsersEntityRetriever> usersEntityRetrieverMock;

        public SaleMessagesServiceTests()
        {
            dbContext = DbFactory.CreateDbContext();

            salesEntityRetrieverMock = new Mock<ISalesEntityRetriever>();

            usersEntityRetrieverMock = new Mock<IUsersEntityRetriever>();

            saleMessagesService = new SaleMessagesService(
                dbContext,
                salesEntityRetrieverMock.Object,
                usersEntityRetrieverMock.Object);
        }

        [Fact]
        public async Task AddMessageToSaleShouldAddMessageToSale()
        {
            var sale = new Sale();

            var user = new VinylExchangeUser();

            usersEntityRetrieverMock.Setup(x => x.GetUser(It.IsAny<Guid?>())).ReturnsAsync(user);

            salesEntityRetrieverMock.Setup(x => x.GetSale(It.IsAny<Guid?>())).ReturnsAsync(sale);

            await saleMessagesService.AddMessageToSale<AddMessageToSaleResourceModel>(
                sale.Id,
                user.Id,
                "Test Message");

            var message = await dbContext.SaleMessages.FirstOrDefaultAsync(sl => sl.SaleId == sale.Id);

            Assert.NotNull(message);
        }

        [Fact]
        public async Task AddMessageToSaleShouldThrowNullReferenceExceptionIfSaleIsNotInDb()
        {
            var user = new VinylExchangeUser();

            usersEntityRetrieverMock.Setup(x => x.GetUser(It.IsAny<Guid?>())).ReturnsAsync(user);

            salesEntityRetrieverMock.Setup(x => x.GetSale(It.IsAny<Guid?>())).ReturnsAsync((Sale) null);

            var exception = await Assert.ThrowsAsync<NullReferenceException>(
                async () => await saleMessagesService
                    .AddMessageToSale<AddMessageToSaleResourceModel>(
                        Guid.NewGuid(),
                        user.Id,
                        "Test Message"));

            Assert.Equal(SaleNotFound, exception.Message);
        }

        [Fact]
        public async Task AddMessageToSaleShouldThrowNullReferenceExceptionIfUserIsNotInDb()
        {
            var sale = new Sale();

            usersEntityRetrieverMock.Setup(x => x.GetUser(It.IsAny<Guid?>()))
                .ReturnsAsync((VinylExchangeUser) null);

            salesEntityRetrieverMock.Setup(x => x.GetSale(It.IsAny<Guid?>())).ReturnsAsync(sale);

            var exception = await Assert.ThrowsAsync<NullReferenceException>(
                async () => await saleMessagesService
                    .AddMessageToSale<AddMessageToSaleResourceModel>(
                        sale.Id,
                        Guid.NewGuid(),
                        "Test Message"));

            Assert.Equal(UserNotFound, exception.Message);
        }

        [Fact]
        public async Task ClearSaleMessagesShouldDeleteMessagesForSaleFromDb()
        {
            var sale = new Sale();

            salesEntityRetrieverMock.Setup(x => x.GetSale(It.IsAny<Guid?>())).ReturnsAsync(sale);

            for (var i = 0; i < 10; i++)
            {
                await dbContext.SaleMessages.AddAsync(new SaleMessage {SaleId = sale.Id});
            }

            await dbContext.SaveChangesAsync();

            await saleMessagesService.ClearSaleMessages(sale.Id);

            var logs = await dbContext.SaleMessages.Where(sl => sl.SaleId == sale.Id).ToListAsync();

            Assert.True(logs.Count == 0);
        }

        [Fact]
        public async Task ClearSaleLogsShouldThrowNullReferenceExceptionIfSaleIsIdIsNotPresentInDb()
        {
            salesEntityRetrieverMock.Setup(x => x.GetSale(It.IsAny<Guid?>())).ReturnsAsync((Sale) null);

            var exception = await Assert.ThrowsAsync<NullReferenceException>(
                async () => await saleMessagesService.ClearSaleMessages(Guid.NewGuid()));

            Assert.Equal(SaleNotFound, exception.Message);
        }

        [Fact]
        public async Task GetMessagesForSaleShouldGetMessagesForSale()
        {
            var sale = new Sale();

            salesEntityRetrieverMock.Setup(x => x.GetSale(It.IsAny<Guid?>())).ReturnsAsync(sale);

            var addedSaleMessagesIds = new List<Guid?>();

            for (var i = 0; i < 10; i++)
            {
                var saleMessage = new SaleMessage {SaleId = sale.Id};

                await dbContext.SaleMessages.AddAsync(saleMessage);

                addedSaleMessagesIds.Add(saleMessage.Id);
            }

            await dbContext.SaveChangesAsync();

            var saleMessages =
                await saleMessagesService.GetMessagesForSale<GetMessagesForSaleResourceModel>(sale.Id);

            Assert.True(saleMessages.Select(sl => addedSaleMessagesIds.Contains(sl.Id)).All(x => x));
        }

        [Fact]
        public async Task GetMessagesForSaleShoulReturnEmptyListIfThereIsNoSaleLogsForSale()
        {
            var sale = new Sale();

            salesEntityRetrieverMock.Setup(x => x.GetSale(It.IsAny<Guid?>())).ReturnsAsync(sale);

            var saleLogs = await saleMessagesService.GetMessagesForSale<GetMessagesForSaleResourceModel>(sale.Id);

            Assert.True(saleLogs.Count() == 0);
        }
    }
}
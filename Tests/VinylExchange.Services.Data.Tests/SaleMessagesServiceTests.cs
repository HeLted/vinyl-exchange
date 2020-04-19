using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VinylExchange.Data;
using VinylExchange.Data.Models;
using VinylExchange.Services.Data.HelperServices.Sales.SaleMessages;
using VinylExchange.Services.Data.MainServices.Sales;
using VinylExchange.Services.Data.MainServices.Users;
using VinylExchange.Services.Data.Tests.TestFactories;
using VinylExchange.Web.Models.ResourceModels.SaleMessages;
using Xunit;

namespace VinylExchange.Services.Data.Tests
{
    public class SaleMessagesServiceTests
    {
        private readonly VinylExchangeDbContext dbContext;

        private readonly ISaleMessagesService saleMessagesService;

        private readonly Mock<ISalesEntityRetriever> salesEntityRetrieverMock;

        private readonly Mock<IUsersEntityRetriever> usersEntityRetrieverMock;
        
        public SaleMessagesServiceTests()
        {
            this.dbContext = DbFactory.CreateDbContext();

            salesEntityRetrieverMock = new Mock<ISalesEntityRetriever>();

            usersEntityRetrieverMock = new Mock<IUsersEntityRetriever>();

            this.saleMessagesService = new SaleMessagesService(this.dbContext, this.salesEntityRetrieverMock.Object,this.usersEntityRetrieverMock.Object);
        }

        [Fact]
        public async Task AddMessageToSaleShouldAddMessageToSale()
        {
            var sale = new Sale();

            var user = new VinylExchangeUser();

            await this.dbContext.Sales.AddAsync(sale);

            await this.dbContext.SaveChangesAsync();

            await this.saleMessagesService.AddMessageToSale<AddMessageToSaleResourceModel>(sale.Id, user.Id, "Test Message");

            var log = await this.dbContext.SaleLogs.FirstOrDefaultAsync(sl => sl.SaleId == sale.Id);

            Assert.NotNull(log);
        }

          [Fact]
        public async Task AddMessageToSaleShouldThrowNullReferenceExceptionIfSaleIsNotInDb()
        {

        }
    }
}

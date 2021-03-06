﻿namespace VinylExchange.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MainServices.Addresses.Contracts;
    using MainServices.Releases.Contracts;
    using MainServices.Sales;
    using MainServices.Sales.Contracts;
    using MainServices.Sales.Exceptions;
    using MainServices.Users.Contracts;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using TestFactories;
    using VinylExchange.Data;
    using VinylExchange.Data.Common.Enumerations;
    using VinylExchange.Data.Models;
    using Web.Models.InputModels.Sales;
    using Web.Models.ResourceModels.Sales;
    using Xunit;
    using static Common.Constants.NullReferenceExceptionsConstants;


    public class SalesServiceTests
    {
        public SalesServiceTests()
        {
            this.dbContext = DbFactory.CreateDbContext();

            this.addressesEntityRetrieverMock = new Mock<IAddressesEntityRetriever>();

            this.usersEntityRetrieverMock = new Mock<IUsersEntityRetriever>();

            this.releasesEntityRetrieverMock = new Mock<IReleasesEntityRetriever>();

            this.salesService = new SalesService(this.dbContext, this.addressesEntityRetrieverMock.Object,
                this.usersEntityRetrieverMock.Object, this.releasesEntityRetrieverMock.Object);
        }

        private const Condition VinylGrade = Condition.Mint;

        private const Condition SleeveGrade = Condition.NearMint;

        private const string Description = "Test description";

        private const decimal Price = 100;

        private readonly VinylExchangeDbContext dbContext;

        private readonly ISalesService salesService;

        private readonly Mock<IAddressesEntityRetriever> addressesEntityRetrieverMock;

        private readonly Mock<IUsersEntityRetriever> usersEntityRetrieverMock;

        private readonly Mock<IReleasesEntityRetriever> releasesEntityRetrieverMock;

        private readonly Address testAddress = new Address
        {
            Country = "Bulgaria", Town = "Sofia", PostalCode = "1612", FullAddress = "j.k Lagera blok 123"
        };

        [Theory]
        [InlineData(Status.Finished)]
        [InlineData(Status.Paid)]
        [InlineData(Status.PaymentPending)]
        [InlineData(Status.Sent)]
        [InlineData(Status.ShippingNegotiation)]
        public async Task PlaceOrderShouldThrowInvalidSaleStatusExceptionIfStatusIsNotOpen(Status status)
        {
            var sale = new Sale {Status = status};

            await this.dbContext.Sales.AddAsync(sale);

            await this.dbContext.SaveChangesAsync();

            var address = new Address();

            var user = new VinylExchangeUser();

            this.usersEntityRetrieverMock.Setup(x => x.GetUser(It.IsAny<Guid?>())).ReturnsAsync(user);

            this.addressesEntityRetrieverMock.Setup(x => x.GetAddress(It.IsAny<Guid?>())).ReturnsAsync(address);

            await Assert.ThrowsAsync<InvalidSaleActionException>(
                async () => await this.salesService.PlaceOrder<SaleStatusResourceModel>(sale.Id, address.Id, user.Id));
        }

        [Theory]
        [InlineData(Status.Finished)]
        [InlineData(Status.Paid)]
        [InlineData(Status.Sent)]
        public async Task CancelShouldThrowInvalidSaleStatusExceptionIfStatusIsAfterPaymentPendingOrOpen(Status status)
        {
            var sale = new Sale {Status = status};

            await this.dbContext.Sales.AddAsync(sale);

            await this.dbContext.SaveChangesAsync();

            var user = new VinylExchangeUser();

            this.usersEntityRetrieverMock.Setup(x => x.GetUser(It.IsAny<Guid?>())).ReturnsAsync(user);

            await Assert.ThrowsAsync<InvalidSaleActionException>(
                async () => await this.salesService.CancelOrder<SaleStatusResourceModel>(sale.Id, user.Id));
        }

        [Theory]
        [InlineData(Status.Open)]
        [InlineData(Status.Finished)]
        [InlineData(Status.Paid)]
        [InlineData(Status.Sent)]
        [InlineData(Status.PaymentPending)]
        public async Task SetShippingPriceShouldThrowInvalidSaleStatusExceptionIfStatusIsNotShippingNegotiation(
            Status status)
        {
            var sale = new Sale {Status = status};

            await this.dbContext.Sales.AddAsync(sale);

            await this.dbContext.SaveChangesAsync();

            await Assert.ThrowsAsync<InvalidSaleActionException>(
                async () => await this.salesService.SetShippingPrice<SaleStatusResourceModel>(sale.Id, 300));
        }

        [Theory]
        [InlineData(Status.Open)]
        [InlineData(Status.Finished)]
        [InlineData(Status.ShippingNegotiation)]
        [InlineData(Status.Sent)]
        [InlineData(Status.PaymentPending)]
        public async Task ConfirmItemSentThrowInvalidSaleStatusExceptionIfStatusIsNotPaid(Status status)
        {
            var sale = new Sale {Status = status};

            await this.dbContext.Sales.AddAsync(sale);

            await this.dbContext.SaveChangesAsync();

            await Assert.ThrowsAsync<InvalidSaleActionException>(
                async () => await this.salesService.ConfirmItemSent<SaleStatusResourceModel>(sale.Id));
        }

        [Theory]
        [InlineData(Status.Open)]
        [InlineData(Status.Finished)]
        [InlineData(Status.ShippingNegotiation)]
        [InlineData(Status.Sent)]
        [InlineData(Status.Paid)]
        public async Task CompletePaymentShouldThrowInvalidSaleStatusExceptionIfStatusIsNotPaymentPending(Status status)
        {
            var sale = new Sale {Status = status};

            await this.dbContext.Sales.AddAsync(sale);

            await this.dbContext.SaveChangesAsync();

            await Assert.ThrowsAsync<InvalidSaleActionException>(
                async () => await this.salesService.CompletePayment<SaleStatusResourceModel>(sale.Id, "test order Id"));
        }

        [Theory]
        [InlineData(Status.Open)]
        [InlineData(Status.Finished)]
        [InlineData(Status.ShippingNegotiation)]
        [InlineData(Status.Paid)]
        [InlineData(Status.PaymentPending)]
        public async Task ConfirmItemRecievedShouldThrowInvalidSaleStatusExceptionIfStatusIsNotSent(Status status)
        {
            var sale = new Sale {Status = status};

            await this.dbContext.Sales.AddAsync(sale);

            await this.dbContext.SaveChangesAsync();

            await Assert.ThrowsAsync<InvalidSaleActionException>(
                async () => await this.salesService.ConfirmItemRecieved<SaleStatusResourceModel>(sale.Id));
        }

        [Fact]
        public async Task CancelOrderShouldSetBuyerIdToNull()
        {
            var user = new VinylExchangeUser();

            var sale = new Sale {Status = Status.PaymentPending, BuyerId = user.Id};

            await this.dbContext.Sales.AddAsync(sale);

            await this.dbContext.SaveChangesAsync();

            this.usersEntityRetrieverMock.Setup(x => x.GetUser(It.IsAny<Guid?>())).ReturnsAsync(user);

            await this.salesService.CancelOrder<SaleStatusResourceModel>(sale.Id, Guid.NewGuid());

            var saleFromDb = await this.dbContext.Sales.FirstOrDefaultAsync(s => s.Id == sale.Id);

            Assert.Null(saleFromDb.BuyerId);
        }

        [Fact]
        public async Task CancelOrderShouldSetSaleStatusToOpen()
        {
            var user = new VinylExchangeUser();

            var sale = new Sale {Status = Status.PaymentPending, BuyerId = user.Id};

            await this.dbContext.Sales.AddAsync(sale);

            await this.dbContext.SaveChangesAsync();

            this.usersEntityRetrieverMock.Setup(x => x.GetUser(It.IsAny<Guid?>())).ReturnsAsync(user);

            await this.salesService.CancelOrder<SaleStatusResourceModel>(sale.Id, Guid.NewGuid());

            var saleFromDb = await this.dbContext.Sales.FirstOrDefaultAsync(s => s.Id == sale.Id);

            Assert.Equal(Status.Open, saleFromDb.Status);
        }

        [Fact]
        public async Task CancelOrderShouldThrowNullReferenceExceptionIfSSaleIsNull()
        {
            var user = new VinylExchangeUser();

            this.usersEntityRetrieverMock.Setup(x => x.GetUser(It.IsAny<Guid?>())).ReturnsAsync(user);

            var exception = await Assert.ThrowsAsync<NullReferenceException>(
                async () => await this.salesService.CancelOrder<SaleStatusResourceModel>(
                    Guid.NewGuid(),
                    user.Id));

            Assert.Equal(SaleNotFound, exception.Message);
        }

        [Fact]
        public async Task CancelOrderShouldThrowNullReferenceExceptionIfUserIsNull()
        {
            var sale = new Sale {Status = Status.Open};

            await this.dbContext.Sales.AddAsync(sale);

            await this.dbContext.SaveChangesAsync();

            this.usersEntityRetrieverMock.Setup(x => x.GetUser(It.IsAny<Guid?>()))
                .ReturnsAsync((VinylExchangeUser) null);

            var exception = await Assert.ThrowsAsync<NullReferenceException>(
                async () => await this.salesService.CancelOrder<SaleStatusResourceModel>(
                    sale.Id,
                    Guid.NewGuid()));

            Assert.Equal(UserNotFound, exception.Message);
        }

        [Fact]
        public async Task CompletePaymentShouldSetSaleOrderId()
        {
            var sale = new Sale {Status = Status.PaymentPending};

            await this.dbContext.Sales.AddAsync(sale);

            await this.dbContext.SaveChangesAsync();

            var saleOrderId = "332-442-133";

            await this.salesService.CompletePayment<SaleStatusResourceModel>(sale.Id, saleOrderId);

            var saleFromDb = await this.dbContext.Sales.FirstOrDefaultAsync(s => s.Id == sale.Id);

            Assert.Equal(saleFromDb.OrderId, saleFromDb.OrderId);
        }

        [Fact]
        public async Task CompletePaymentShouldSetStatusToPaid()
        {
            var sale = new Sale {Status = Status.PaymentPending};

            await this.dbContext.Sales.AddAsync(sale);

            await this.dbContext.SaveChangesAsync();

            await this.salesService.CompletePayment<SaleStatusResourceModel>(sale.Id, "test");

            var saleFromDb = await this.dbContext.Sales.FirstOrDefaultAsync(s => s.Id == sale.Id);

            Assert.Equal(Status.Paid, saleFromDb.Status);
        }

        [Fact]
        public async Task CompletePaymentShouldThrowNullReferenceExceptionIfSSaleIsNull()
        {
            var exception = await Assert.ThrowsAsync<NullReferenceException>(
                async () => await this.salesService.CompletePayment<SaleStatusResourceModel>(
                    Guid.NewGuid(),
                    "test order Id"));

            Assert.Equal(SaleNotFound, exception.Message);
        }

        [Fact]
        public async Task ConfirmItemRecievedShouldSetStatusToFinished()
        {
            var sale = new Sale {Status = Status.Sent};

            await this.dbContext.Sales.AddAsync(sale);

            await this.dbContext.SaveChangesAsync();

            await this.salesService.ConfirmItemRecieved<SaleStatusResourceModel>(sale.Id);

            var saleFromDb = await this.dbContext.Sales.FirstOrDefaultAsync(s => s.Id == sale.Id);

            Assert.Equal(Status.Finished, saleFromDb.Status);
        }

        [Fact]
        public async Task ConfirmItemRecievedShouldThrowNullReferenceExceptionIfSSaleIsNull()
        {
            var exception = await Assert.ThrowsAsync<NullReferenceException>(
                async () =>
                    await this.salesService
                        .ConfirmItemRecieved<SaleStatusResourceModel>(Guid.NewGuid()));

            Assert.Equal(SaleNotFound, exception.Message);
        }

        [Fact]
        public async Task ConfirmItemSentShouldSetStatusToSent()
        {
            var sale = new Sale {Status = Status.Paid};

            await this.dbContext.Sales.AddAsync(sale);

            await this.dbContext.SaveChangesAsync();

            await this.salesService.ConfirmItemSent<SaleStatusResourceModel>(sale.Id);

            var saleFromDb = await this.dbContext.Sales.FirstOrDefaultAsync(s => s.Id == sale.Id);

            Assert.Equal(Status.Sent, saleFromDb.Status);
        }

        [Fact]
        public async Task ConfirmItemSentShouldThrowNullReferenceExceptionIfSSaleIsNull()
        {
            var exception = await Assert.ThrowsAsync<NullReferenceException>(
                async () =>
                    await this.salesService.ConfirmItemSent<SaleStatusResourceModel>(Guid.NewGuid()));

            Assert.Equal(SaleNotFound, exception.Message);
        }

        [Fact]
        public async Task CreateSaleShouldCreateSale()
        {
            var release = new Release();

            var seller = new VinylExchangeUser();

            var address = new Address();

            this.releasesEntityRetrieverMock.Setup(x => x.GetRelease(It.IsAny<Guid?>())).ReturnsAsync(release);

            this.usersEntityRetrieverMock.Setup(x => x.GetUser(It.IsAny<Guid?>())).ReturnsAsync(seller);

            this.addressesEntityRetrieverMock.Setup(x => x.GetAddress(It.IsAny<Guid?>())).ReturnsAsync(address);

            var createdSaleModel = await this.salesService.CreateSale<CreateSaleResourceModel>(
                Condition.Fair,
                Condition.Mint,
                "ewewe",
                30,
                address.Id,
                release.Id,
                seller.Id);

            await this.dbContext.SaveChangesAsync();

            var createdSale = await this.dbContext.Sales.FirstOrDefaultAsync(s => s.Id == createdSaleModel.Id);

            Assert.NotNull(createdSale);
        }

        [Fact]
        public async Task CreateSaleShouldCreateSaleWithCorrectData()
        {
            var release = new Release();

            var seller = new VinylExchangeUser();

            var address = this.testAddress;

            this.releasesEntityRetrieverMock.Setup(x => x.GetRelease(It.IsAny<Guid?>())).ReturnsAsync(release);

            this.usersEntityRetrieverMock.Setup(x => x.GetUser(It.IsAny<Guid?>())).ReturnsAsync(seller);

            this.addressesEntityRetrieverMock.Setup(x => x.GetAddress(It.IsAny<Guid?>())).ReturnsAsync(address);

            var addressProperties = new List<string> {address.Country, address.Town};

            var createdSaleModel = await this.salesService.CreateSale<CreateSaleResourceModel>(
                VinylGrade,
                SleeveGrade,
                Description,
                Price,
                address.Id,
                release.Id,
                seller.Id);

            await this.dbContext.SaveChangesAsync();

            var createdSale = await this.dbContext.Sales.FirstOrDefaultAsync(s => s.Id == createdSaleModel.Id);

            Assert.Equal(VinylGrade, createdSale.VinylGrade);
            Assert.Equal(SleeveGrade, createdSale.SleeveGrade);
            Assert.Equal(release.Id, createdSale.ReleaseId);
            Assert.Equal(Price, createdSale.Price);
            Assert.True(addressProperties.Select(ap => createdSale.ShipsFrom.Contains(ap)).All(x => x));
        }

        [Fact]
        public async Task CreateSaleShouldThrowNullRefferenceExceptionIfProvidedAddressIdIsNotInDb()
        {
            var release = new Release();

            var seller = new VinylExchangeUser();

            this.releasesEntityRetrieverMock.Setup(x => x.GetRelease(It.IsAny<Guid?>())).ReturnsAsync(release);

            this.usersEntityRetrieverMock.Setup(x => x.GetUser(It.IsAny<Guid?>())).ReturnsAsync(seller);

            this.addressesEntityRetrieverMock.Setup(x => x.GetAddress(It.IsAny<Guid?>())).ReturnsAsync((Address) null);

            var exception = await Assert.ThrowsAsync<NullReferenceException>(
                async () => await this.salesService.CreateSale<CreateSaleResourceModel>(
                    VinylGrade,
                    SleeveGrade,
                    Description,
                    Price,
                    Guid.NewGuid(),
                    release.Id,
                    seller.Id));

            Assert.Equal(AddressNotFound, exception.Message);
        }

        [Fact]
        public async Task CreateSaleShouldThrowNullRefferenceExceptionIfProvidedReleaseIdIsNotInDb()
        {
            var address = new Address();

            var seller = new VinylExchangeUser();

            this.releasesEntityRetrieverMock.Setup(x => x.GetRelease(It.IsAny<Guid?>())).ReturnsAsync((Release) null);

            this.usersEntityRetrieverMock.Setup(x => x.GetUser(It.IsAny<Guid?>())).ReturnsAsync(seller);

            this.addressesEntityRetrieverMock.Setup(x => x.GetAddress(It.IsAny<Guid?>())).ReturnsAsync(address);

            var exception = await Assert.ThrowsAsync<NullReferenceException>(
                async () => await this.salesService.CreateSale<CreateSaleResourceModel>(
                    VinylGrade,
                    SleeveGrade,
                    Description,
                    Price,
                    address.Id,
                    seller.Id,
                    seller.Id));

            Assert.Equal(ReleaseNotFound, exception.Message);
        }

        [Fact]
        public async Task CreateSaleShouldThrowNullRefferenceExceptionIfProvidedUserIdIsNotInDb()
        {
            var release = new Release();

            var address = new Address();

            this.releasesEntityRetrieverMock.Setup(x => x.GetRelease(It.IsAny<Guid?>())).ReturnsAsync(release);

            this.usersEntityRetrieverMock.Setup(x => x.GetUser(It.IsAny<Guid?>()))
                .ReturnsAsync((VinylExchangeUser) null);

            this.addressesEntityRetrieverMock.Setup(x => x.GetAddress(It.IsAny<Guid?>())).ReturnsAsync(address);

            var exception = await Assert.ThrowsAsync<NullReferenceException>(
                async () => await this.salesService.CreateSale<CreateSaleResourceModel>(
                    VinylGrade,
                    SleeveGrade,
                    Description,
                    Price,
                    address.Id,
                    release.Id,
                    Guid.NewGuid()));

            Assert.Equal(UserNotFound, exception.Message);
        }

        [Fact]
        public async Task EditSaleShouldEditSaleWithCorrectData()
        {
            var release = new Release();

            var user = new VinylExchangeUser();

            var updatedAddress = this.testAddress;

            this.releasesEntityRetrieverMock.Setup(x => x.GetRelease(It.IsAny<Guid?>())).ReturnsAsync(release);

            this.usersEntityRetrieverMock.Setup(x => x.GetUser(It.IsAny<Guid?>())).ReturnsAsync(user);

            this.addressesEntityRetrieverMock.Setup(x => x.GetAddress(It.IsAny<Guid?>())).ReturnsAsync(updatedAddress);

            var addressProperties = new List<string> {updatedAddress.Country, updatedAddress.Town};

            var sale = new Sale
            {
                VinylGrade = Condition.Poor,
                SleeveGrade = Condition.NearMint,
                Description = "blbbebe",
                Price = 50,
                ShipsFrom = "Paris France"
            };

            await this.dbContext.Sales.AddAsync(sale);

            await this.dbContext.SaveChangesAsync();

            var editSaleInputModel = new EditSaleInputModel
            {
                VinylGrade = Condition.Mint,
                SleeveGrade = Condition.Mint,
                Description = "updated description",
                Price = 100,
                SaleId = sale.Id
            };

            await this.salesService.EditSale<EditSaleResourceModel>(editSaleInputModel);

            var updatedSale = await this.dbContext.Sales.FirstOrDefaultAsync(s => s.Id == sale.Id);

            Assert.Equal(editSaleInputModel.SaleId, updatedSale.Id);
            Assert.Equal(editSaleInputModel.VinylGrade, updatedSale.VinylGrade);
            Assert.Equal(editSaleInputModel.SleeveGrade, updatedSale.SleeveGrade);
            Assert.Equal(editSaleInputModel.Description, updatedSale.Description);
            Assert.Equal(editSaleInputModel.Price, updatedSale.Price);
            Assert.True(addressProperties.Select(ap => updatedSale.ShipsFrom.Contains(ap)).All(x => x));
        }

        [Fact]
        public async Task EditSaleShouldThrowNullRefferenceExceptionIfProvidedAddressIdIsNotInDb()
        {
            var sale = new Sale();

            await this.dbContext.Sales.AddAsync(sale);

            await this.dbContext.SaveChangesAsync();

            this.addressesEntityRetrieverMock.Setup(x => x.GetAddress(It.IsAny<Guid?>())).ReturnsAsync((Address) null);

            var editSaleInputModel = new EditSaleInputModel {SaleId = sale.Id, ShipsFromAddressId = Guid.NewGuid()};

            var exception = await Assert.ThrowsAsync<NullReferenceException>(
                async () => await this.salesService.EditSale<EditSaleResourceModel>(
                    editSaleInputModel));

            Assert.Equal(AddressNotFound, exception.Message);
        }

        [Fact]
        public async Task EditSaleShouldThrowNullRefferenceExceptionIfProvidedSaleIdIsNotInDb()
        {
            this.addressesEntityRetrieverMock.Setup(x => x.GetAddress(It.IsAny<Guid?>())).ReturnsAsync(new Address());

            var editSaleInputModel = new EditSaleInputModel();

            var exception = await Assert.ThrowsAsync<NullReferenceException>(
                async () => await this.salesService.EditSale<EditSaleResourceModel>(
                    editSaleInputModel));

            Assert.Equal(SaleNotFound, exception.Message);
        }

        [Fact]
        public async Task GetAllSalesForReleaseShouldGetAllSalesForReleaseIfTheirStatusIsOpen()
        {
            var release = new Release();

            var secondRelease = new Release();

            var saleIds = new List<Guid?>();

            for (var i = 0; i < 5; i++)
            {
                var sale = new Sale {Status = Status.Open, ReleaseId = release.Id};

                await this.dbContext.Sales.AddAsync(sale);

                saleIds.Add(sale.Id);
            }

            for (var i = 0; i < 5; i++)
            {
                var sale = new Sale {Status = Status.Open, ReleaseId = secondRelease.Id};

                await this.dbContext.Sales.AddAsync(sale);

                saleIds.Add(sale.Id);
            }

            await this.dbContext.SaveChangesAsync();

            var saleModels =
                await this.salesService.GetAllSalesForRelease<GetAllSalesForReleaseResouceModel>(release.Id);

            Assert.True(saleModels.Count == release.Sales.Count);
            Assert.True(saleModels.Select(sm => saleIds.Contains(sm.Id)).All(x => x));
        }

        [Fact]
        public async Task GetAllSalesForReleaseShouldNotGetAnySalesIfProvidedReleaseIdIsNotRegisteredByAnySale()
        {
            var release = new Release();

            for (var i = 0; i < 5; i++)
            {
                var sale = new Sale {Status = Status.Finished, ReleaseId = release.Id};

                await this.dbContext.Sales.AddAsync(sale);
            }

            await this.dbContext.SaveChangesAsync();

            var saleModels =
                await this.salesService.GetAllSalesForRelease<GetAllSalesForReleaseResouceModel>(Guid.NewGuid());

            Assert.True(saleModels.Count == 0);
        }

        [Fact]
        public async Task GetAllSalesForReleaseShouldNotGetAnySAlesWithStatusOtherThanOpen()
        {
            var release = new Release();

            for (var i = 0; i < 5; i++)
            {
                var sale = new Sale {Status = Status.Finished, ReleaseId = release.Id};

                await this.dbContext.Sales.AddAsync(sale);
            }

            await this.dbContext.SaveChangesAsync();

            var saleModels =
                await this.salesService.GetAllSalesForRelease<GetAllSalesForReleaseResouceModel>(release.Id);

            Assert.True(saleModels.Count == 0);
        }

        [Fact]
        public async Task GetSaleShouldGetSaleIfProvidedSaleIsInDb()
        {
            var sale = new Sale();

            await this.dbContext.Sales.AddAsync(sale);

            await this.dbContext.SaveChangesAsync();

            var saleModel = this.salesService.GetSale<GetSaleResourceModel>(sale.Id);

            Assert.NotNull(saleModel);
        }

        [Fact]
        public async Task GetSaleShouldReturnNullIfProvidedSaleIsNotInDb()
        {
            var sale = new Sale();

            await this.dbContext.Sales.AddAsync(sale);

            await this.dbContext.SaveChangesAsync();

            var saleModel = await this.salesService.GetSale<GetSaleResourceModel>(Guid.NewGuid());

            Assert.Null(saleModel);
        }

        [Fact]
        public async Task GetUserPurchasesShouldGetUserPurchases()
        {
            var user = new VinylExchangeUser();

            for (var i = 0; i < 5; i++)
            {
                var sale = new Sale {BuyerId = user.Id};

                await this.dbContext.Sales.AddAsync(sale);
            }

            for (var i = 0; i < 5; i++)
            {
                var sale = new Sale {BuyerId = Guid.NewGuid()};

                await this.dbContext.Sales.AddAsync(sale);
            }

            await this.dbContext.SaveChangesAsync();

            var purchasesModels = await this.salesService.GetUserPurchases<GetSaleResourceModel>(user.Id);

            Assert.True(purchasesModels.All(pm => pm.BuyerId == user.Id));
        }

        [Fact]
        public async Task GetUserPurchasesShouldReturnEmptyListIfUserHasNoPurchases()
        {
            var user = new VinylExchangeUser();

            for (var i = 0; i < 10; i++)
            {
                var sale = new Sale {BuyerId = Guid.NewGuid()};

                await this.dbContext.Sales.AddAsync(sale);
            }

            await this.dbContext.SaveChangesAsync();

            var purchasesModels = await this.salesService.GetUserPurchases<GetSaleResourceModel>(user.Id);

            Assert.True(purchasesModels.Count == 0);
        }

        [Fact]
        public async Task GetUserSalesShouldGetUserSales()
        {
            var user = new VinylExchangeUser();

            for (var i = 0; i < 5; i++)
            {
                var sale = new Sale {SellerId = user.Id};

                await this.dbContext.Sales.AddAsync(sale);
            }

            for (var i = 0; i < 5; i++)
            {
                var sale = new Sale {SellerId = Guid.NewGuid()};

                await this.dbContext.Sales.AddAsync(sale);
            }

            await this.dbContext.SaveChangesAsync();

            var saleModels = await this.salesService.GetUserSales<GetSaleResourceModel>(user.Id);

            Assert.True(saleModels.All(sm => sm.SellerId == user.Id));
        }

        [Fact]
        public async Task GetUserSalesShouldReturnEmptyListIfUserHasNoSales()
        {
            var user = new VinylExchangeUser();

            for (var i = 0; i < 10; i++)
            {
                var sale = new Sale {SellerId = Guid.NewGuid()};

                await this.dbContext.Sales.AddAsync(sale);
            }

            await this.dbContext.SaveChangesAsync();

            var salesModels = await this.salesService.GetUserPurchases<GetSaleResourceModel>(user.Id);

            Assert.True(salesModels.Count == 0);
        }

        [Fact]
        public async Task PlaceOrderShouldChangeSaleStatusToShippingNegotiation()
        {
            var sale = new Sale {Status = Status.Open};

            await this.dbContext.Sales.AddAsync(sale);

            await this.dbContext.SaveChangesAsync();

            var address = new Address();

            var user = new VinylExchangeUser();

            this.usersEntityRetrieverMock.Setup(x => x.GetUser(It.IsAny<Guid?>())).ReturnsAsync(user);

            this.addressesEntityRetrieverMock.Setup(x => x.GetAddress(It.IsAny<Guid?>())).ReturnsAsync(address);

            await this.salesService.PlaceOrder<SaleStatusResourceModel>(sale.Id, address.Id, user.Id);

            var changedSale = await this.dbContext.Sales.FirstOrDefaultAsync(s => s.Id == sale.Id);

            Assert.True(Status.ShippingNegotiation == changedSale.Status);
        }

        [Fact]
        public async Task PlaceOrderShouldSetBuyerId()
        {
            var sale = new Sale {Status = Status.Open};

            await this.dbContext.Sales.AddAsync(sale);

            await this.dbContext.SaveChangesAsync();

            var address = new Address();

            var user = new VinylExchangeUser();

            this.usersEntityRetrieverMock.Setup(x => x.GetUser(It.IsAny<Guid?>())).ReturnsAsync(user);

            this.addressesEntityRetrieverMock.Setup(x => x.GetAddress(It.IsAny<Guid?>())).ReturnsAsync(address);

            await this.salesService.PlaceOrder<SaleStatusResourceModel>(sale.Id, address.Id, user.Id);

            var changedSale = await this.dbContext.Sales.FirstOrDefaultAsync(s => s.Id == sale.Id);

            Assert.True(user.Id == changedSale.BuyerId);
        }

        [Fact]
        public async Task PlaceOrderShouldSetShipsToAddress()
        {
            var sale = new Sale {Status = Status.Open};

            await this.dbContext.Sales.AddAsync(sale);

            await this.dbContext.SaveChangesAsync();

            var address = new Address();

            var user = new VinylExchangeUser();

            this.usersEntityRetrieverMock.Setup(x => x.GetUser(It.IsAny<Guid?>())).ReturnsAsync(user);

            this.addressesEntityRetrieverMock.Setup(x => x.GetAddress(It.IsAny<Guid?>())).ReturnsAsync(address);

            await this.salesService.PlaceOrder<SaleStatusResourceModel>(sale.Id, address.Id, user.Id);

            var changedSale = await this.dbContext.Sales.FirstOrDefaultAsync(s => s.Id == sale.Id);

            Assert.True(
                changedSale.ShipsTo
                == $"{address.Country} - {address.Town} - {address.PostalCode} - {address.FullAddress}");
        }

        [Fact]
        public async Task PlaceOrderShouldThrowNullReferenceExceptionIfAddressIsNull()
        {
            var sale = new Sale {Status = Status.Open};

            await this.dbContext.Sales.AddAsync(sale);

            await this.dbContext.SaveChangesAsync();

            var user = new VinylExchangeUser();

            this.usersEntityRetrieverMock.Setup(x => x.GetUser(It.IsAny<Guid?>())).ReturnsAsync(user);

            this.addressesEntityRetrieverMock.Setup(x => x.GetAddress(It.IsAny<Guid?>())).ReturnsAsync((Address) null);

            var exception = await Assert.ThrowsAsync<NullReferenceException>(
                async () => await this.salesService.PlaceOrder<SaleStatusResourceModel>(
                    sale.Id,
                    Guid.NewGuid(),
                    user.Id));

            Assert.Equal(AddressNotFound, exception.Message);
        }

        [Fact]
        public async Task PlaceOrderShouldThrowNullReferenceExceptionIfSSaleIsNull()
        {
            var user = new VinylExchangeUser();

            var address = new Address();

            this.usersEntityRetrieverMock.Setup(x => x.GetUser(It.IsAny<Guid?>())).ReturnsAsync(user);

            this.addressesEntityRetrieverMock.Setup(x => x.GetAddress(It.IsAny<Guid?>())).ReturnsAsync(address);

            var exception = await Assert.ThrowsAsync<NullReferenceException>(
                async () => await this.salesService.PlaceOrder<SaleStatusResourceModel>(
                    Guid.NewGuid(),
                    address.Id,
                    user.Id));

            Assert.Equal(SaleNotFound, exception.Message);
        }

        [Fact]
        public async Task PlaceOrderShouldThrowNullReferenceExceptionIfUserIsNull()
        {
            var sale = new Sale {Status = Status.Open};

            await this.dbContext.Sales.AddAsync(sale);

            await this.dbContext.SaveChangesAsync();

            var address = new Address();

            var user = new VinylExchangeUser();

            this.usersEntityRetrieverMock.Setup(x => x.GetUser(It.IsAny<Guid?>()))
                .ReturnsAsync((VinylExchangeUser) null);

            this.addressesEntityRetrieverMock.Setup(x => x.GetAddress(It.IsAny<Guid?>())).ReturnsAsync(address);

            var exception = await Assert.ThrowsAsync<NullReferenceException>(
                async () => await this.salesService.PlaceOrder<SaleStatusResourceModel>(
                    sale.Id,
                    address.Id,
                    Guid.NewGuid()));

            Assert.Equal(UserNotFound, exception.Message);
        }

        [Fact]
        public async Task RemoveSaleShouldRemoveSale()
        {
            var sale = new Sale();

            await this.dbContext.Sales.AddAsync(sale);

            await this.dbContext.SaveChangesAsync();

            await this.salesService.RemoveSale<RemoveSaleResourceModel>(sale.Id);

            var removedSale = await this.dbContext.Sales.FirstOrDefaultAsync(s => s.Id == sale.Id);

            Assert.Null(removedSale);
        }

        [Fact]
        public async Task RemoveSaleShouldThrowNullReferenceExceptionIfProvidedGenreIdIsNotInDb()
        {
            var exception = await Assert.ThrowsAsync<NullReferenceException>(
                async () => await this.salesService.RemoveSale<RemoveSaleResourceModel>(
                    Guid.NewGuid()));

            Assert.Equal(SaleNotFound, exception.Message);
        }

        [Fact]
        public async Task SetShippingPriceShouldSetSaleShippingPrice()
        {
            var sale = new Sale {Status = Status.ShippingNegotiation};

            await this.dbContext.Sales.AddAsync(sale);

            await this.dbContext.SaveChangesAsync();

            await this.salesService.SetShippingPrice<SaleStatusResourceModel>(sale.Id, 500);

            var saleFromDb = await this.dbContext.Sales.FirstOrDefaultAsync(s => s.Id == sale.Id);

            Assert.Equal(500, saleFromDb.ShippingPrice);
        }

        [Fact]
        public async Task SetShippingPriceShouldSetStatusToPaymentPending()
        {
            var sale = new Sale {Status = Status.ShippingNegotiation};

            await this.dbContext.Sales.AddAsync(sale);

            await this.dbContext.SaveChangesAsync();

            await this.salesService.SetShippingPrice<SaleStatusResourceModel>(sale.Id, 500);

            var saleFromDb = await this.dbContext.Sales.FirstOrDefaultAsync(s => s.Id == sale.Id);

            Assert.Equal(Status.PaymentPending, saleFromDb.Status);
        }

        [Fact]
        public async Task SetShippingPriceShouldThrowNullReferenceExceptionIfSSaleIsNull()
        {
            var exception = await Assert.ThrowsAsync<NullReferenceException>(
                async () => await this.salesService.SetShippingPrice<SaleStatusResourceModel>(
                    Guid.NewGuid(),
                    500));

            Assert.Equal(SaleNotFound, exception.Message);
        }
    }
}
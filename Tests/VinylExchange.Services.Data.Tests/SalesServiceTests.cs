namespace VinylExchange.Services.Data.Tests
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using VinylExchange.Data;
    using VinylExchange.Data.Common.Enumerations;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Data.MainServices.Sales;
    using VinylExchange.Services.Data.Tests.TestFactories;
    using VinylExchange.Web.Models.InputModels.Sales;
    using VinylExchange.Web.Models.ResourceModels.Sales;

    using Xunit;

    using static VinylExchange.Common.Constants.NullReferenceExceptionsConstants;

    #endregion

    [Collection("AutoMapperSetup")]
    public class SalesServiceTests
    {
        private readonly VinylExchangeDbContext dbContext;

        private readonly ISalesService salesService;

        private readonly CreateSaleInputModel testCreateSaleInputModel = new CreateSaleInputModel
        {
            VinylGrade = Condition.Mint,
            SleeveGrade = Condition.Fair,
            Price = 30,
            Description = "test description"
        };

        public SalesServiceTests()
        {
            this.dbContext = DbFactory.CreateVinylExchangeDbContext();

            this.salesService = new SalesService(this.dbContext);

            Task.Run(async () => { await this.AddSalesTestData(); }).Wait();
        }

        [Fact]
        public async Task CreateSaleShouldCreateSale()
        {
            var release = await this.dbContext.Releases.FirstAsync();

            var seller = await this.dbContext.Users.FirstAsync();

            var address = await this.dbContext.Addresses.FirstAsync();

            this.testCreateSaleInputModel.ReleaseId = release.Id;

            this.testCreateSaleInputModel.ShipsFromAddressId = address.Id;

            var createdSaleModel =
                await this.salesService.CreateSale<CreateSaleResourceModel>(this.testCreateSaleInputModel, seller.Id);

            await this.dbContext.SaveChangesAsync();

            var createdSale = await this.dbContext.Sales.FirstOrDefaultAsync(s => s.Id == createdSaleModel.Id);

            Assert.NotNull(createdSale);
        }

        [Fact]
        public async Task CreateSaleShouldCreateSaleWithCorrectData()
        {
            var release = await this.dbContext.Releases.FirstAsync();

            var seller = await this.dbContext.Users.FirstAsync();

            var address = await this.dbContext.Addresses.FirstAsync();

            var addressProperties = new List<string>
            {
                address.Country,

                address.Town
            };

            this.testCreateSaleInputModel.ReleaseId = release.Id;

            this.testCreateSaleInputModel.ShipsFromAddressId = address.Id;

            var createdSaleModel =
                await this.salesService.CreateSale<CreateSaleResourceModel>(this.testCreateSaleInputModel, seller.Id);

            await this.dbContext.SaveChangesAsync();

            var createdSale = await this.dbContext.Sales.FirstOrDefaultAsync(s => s.Id == createdSaleModel.Id);

            Assert.Equal(this.testCreateSaleInputModel.VinylGrade, createdSale.VinylGrade);
            Assert.Equal(this.testCreateSaleInputModel.SleeveGrade, createdSale.SleeveGrade);
            Assert.Equal(this.testCreateSaleInputModel.ReleaseId, createdSale.ReleaseId);
            Assert.Equal(this.testCreateSaleInputModel.Price, createdSale.Price);
            Assert.True(addressProperties.Select(ap => createdSale.ShipsFrom.Contains(ap)).All(x => x));
        }

        [Fact]
        public async Task CreateSaleShouldThrowNullRefferenceExceptionIfProvidedReleaseIdIsNotInDb()
        {
            var address = await this.dbContext.Addresses.FirstAsync();

            var seller = await this.dbContext.Users.FirstAsync();

            this.testCreateSaleInputModel.ShipsFromAddressId = address.Id;

            this.testCreateSaleInputModel.ReleaseId = Guid.NewGuid();

            var exception = await Assert.ThrowsAsync<NullReferenceException>(
                                async () => await this.salesService.CreateSale<CreateSaleResourceModel>(
                                                this.testCreateSaleInputModel,
                                                seller.Id));

            Assert.Equal(ReleaseNotFound, exception.Message);
        }

        [Fact]
        public async Task CreateSaleShouldThrowNullRefferenceExceptionIfProvidedAddressIdIsNotInDb()
        {
            var release = await this.dbContext.Releases.FirstAsync();

            var seller = await this.dbContext.Users.FirstAsync();

            this.testCreateSaleInputModel.ShipsFromAddressId = Guid.NewGuid();

            this.testCreateSaleInputModel.ReleaseId = release.Id;

            var exception = await Assert.ThrowsAsync<NullReferenceException>(
                                async () => await this.salesService.CreateSale<CreateSaleResourceModel>(
                                                this.testCreateSaleInputModel,
                                                seller.Id));

            Assert.Equal(AddressNotFound, exception.Message);
        }

        [Fact]
        public async Task CreateSaleShouldThrowNullRefferenceExceptionIfProvidedUserIdIsNotInDb()
        {
            var release = await this.dbContext.Releases.FirstAsync();

            var address = await this.dbContext.Addresses.FirstAsync();

            this.testCreateSaleInputModel.ShipsFromAddressId = address.Id;
            this.testCreateSaleInputModel.ReleaseId = release.Id;

            var exception = await Assert.ThrowsAsync<NullReferenceException>(
                                async () => await this.salesService.CreateSale<CreateSaleResourceModel>(
                                                this.testCreateSaleInputModel,
                                                Guid.NewGuid()));

            Assert.Equal(UserNotFound, exception.Message);
        }

        [Fact]
        public async Task EditSaleShouldEditSaleWithCorrectData()
        {
            var initialAddress = await this.dbContext.Addresses.FirstAsync();

            var sale = await this.dbContext.Sales.FirstAsync();

            sale.ShipsFrom = $"{initialAddress.Country} - {initialAddress.Town}";

            var updatedAddress = await this.dbContext.Addresses.Skip(1).FirstAsync();

            var addressProperties = new List<string>
            {
                updatedAddress.Country,

                updatedAddress.Town
            };

            var editSaleInputModel = new EditSaleInputModel
            {
                VinylGrade = Condition.Mint,
                SleeveGrade = Condition.Mint,
                Description = "updated description",
                ShipsFromAddressId = updatedAddress.Id,
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
            var sale = await this.dbContext.Sales.FirstAsync();

            var editSaleInputModel = new EditSaleInputModel
            {
                SaleId = sale.Id,
                ShipsFromAddressId = Guid.NewGuid()
            };

            var exception = await Assert.ThrowsAsync<NullReferenceException>(
                                async () => await this.salesService.EditSale<EditSaleResourceModel>(
                                                editSaleInputModel));

            Assert.Equal(AddressNotFound, exception.Message);
        }

        [Fact]
        public async Task EditSaleShouldThrowNullRefferenceExceptionIfProvidedSaleIdIsNotInDb()
        {
            var address = await this.dbContext.Addresses.FirstAsync();

            var editSaleInputModel = new EditSaleInputModel
            {
                SaleId = Guid.NewGuid(),
                ShipsFromAddressId = address.Id
            };

            var exception = await Assert.ThrowsAsync<NullReferenceException>(
                                async () => await this.salesService.EditSale<EditSaleResourceModel>(
                                                editSaleInputModel));

            Assert.Equal(SaleNotFound, exception.Message);
        }

        [Fact]
        public async Task RemoveSaleShouldRemoveSale()
        {
            var sale = await this.dbContext.Sales.FirstAsync();

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
        public async Task GetAllSalesForReleaseShouldGetAllSalesForReleaseIfTheirStatusIsOpen()
        {
            var release = new Release();

            var secondRelease = new Release();

            var saleIds = new List<Guid>();

            for (int i = 0; i < 5; i++)
            {
                var sale = new Sale
                {
                    Status = Status.Open,

                    ReleaseId = release.Id
                };

                await this.dbContext.Sales.AddAsync(sale);

                saleIds.Add(sale.Id);
            }


            for (int i = 0; i < 5; i++)
            {
                var sale = new Sale
                {
                    Status = Status.Open,

                    ReleaseId = secondRelease.Id
                };

                await this.dbContext.Sales.AddAsync(sale);

                saleIds.Add(sale.Id);
            }

            await this.dbContext.SaveChangesAsync();

            var saleModels = await this.salesService.GetAllSalesForRelease<GetAllSalesForReleaseResouceModel>(release.Id);

            Assert.True(saleModels.Count == release.Sales.Count);
            Assert.True(saleModels.Select(sm => saleIds.Contains(sm.Id)).All(x => x == true));
        }

        [Fact]
        public async Task GetAllSalesForReleaseShouldNotGetAnyReleasesWithStatusOtherThanOpen()
        {
            var release = new Release();


            for (int i = 0; i < 5; i++)
            {
                var sale = new Sale
                    {
                        Status = Status.Finished,

                        ReleaseId = release.Id
                    };

                await this.dbContext.Sales.AddAsync(sale);

            }

            await this.dbContext.SaveChangesAsync();

            var saleModels = await this.salesService.GetAllSalesForRelease<GetAllSalesForReleaseResouceModel>(release.Id);

            Assert.True(saleModels.Count == 0);
            
        }

        private async Task AddSalesTestData()
        {
            var releases = new List<Release> { new Release { Artist = "Test Artist", Title = "Test Title" }, new Release { Artist = "Test Artist", Title = "Test Title" } };

            var adresses = new List<Address>
                {
                    new Address
                        {
                            Country = "Bulgaria",
                            Town = "Sofia",
                            PostalCode = "1612",
                            FullAddress = "j.k Lagera blok 123"
                        },
                    new Address
                        {
                            Country = "Bulgaria",
                            Town = "Sofia",
                            PostalCode = "1612",
                            FullAddress = "Mladost blok 123"
                        }
                };

            var users = new List<VinylExchangeUser> { new VinylExchangeUser { Id = Guid.NewGuid() } };

            var sales = new List<Sale>
                {
                    new Sale
                        {
                            VinylGrade = Condition.Poor,
                            SleeveGrade = Condition.NearMint,
                            Description = "blbbebe",
                           Price = 50,

                        }
                };

            await this.dbContext.Releases.AddRangeAsync(releases);

            await this.dbContext.Addresses.AddRangeAsync(adresses);

            await this.dbContext.Users.AddRangeAsync(users);

            await this.dbContext.Sales.AddRangeAsync(sales);

            await this.dbContext.SaveChangesAsync();
        }
    }
}
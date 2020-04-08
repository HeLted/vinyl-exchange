namespace VinylExchange.Services.Data.Tests
{
    #region

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Moq;

    using VinylExchange.Data;
    using VinylExchange.Data.Common.Enumerations;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Data.MainServices.Collections;
    using VinylExchange.Services.Data.MainServices.Releases;
    using VinylExchange.Services.Data.MainServices.Users;
    using VinylExchange.Services.Data.Tests.TestFactories;
    using VinylExchange.Web.Models.InputModels.Collections;
    using VinylExchange.Web.Models.ResourceModels.Collections;

    using Xunit;

    using static VinylExchange.Common.Constants.NullReferenceExceptionsConstants;

    #endregion

    public class CollectionsServiceTests
    {
        private readonly VinylExchangeDbContext dbContext;

        private readonly ICollectionsService collectionsService;

        private readonly Mock<IUsersEntityRetriever> usersEntityRetrieverMock;

        private readonly Mock<IReleasesEntityRetriever> releasesEntityRetrieverMock;

        private readonly AddToCollectionInputModel testAddToCollectionInputModel =
            new AddToCollectionInputModel { VinylGrade = Condition.Poor, SleeveGrade = Condition.Mint };

        public CollectionsServiceTests()
        {
            this.dbContext = DbFactory.CreateDbContext();

            this.releasesEntityRetrieverMock = new Mock<IReleasesEntityRetriever>();

            this.usersEntityRetrieverMock = new Mock<IUsersEntityRetriever>();

            this.collectionsService = new CollectionsService(
                this.dbContext,
                this.releasesEntityRetrieverMock.Object,
                this.usersEntityRetrieverMock.Object);
        }

        [Fact]
        public async Task AddToCollectionShouldAddToCollection()
        {
            var release = new Release();

            var user = new VinylExchangeUser();

            this.releasesEntityRetrieverMock.Setup(x => x.GetRelease(It.IsAny<Guid?>())).ReturnsAsync(release);

            this.usersEntityRetrieverMock.Setup(x => x.GetUser(It.IsAny<Guid?>())).ReturnsAsync(user);

            var createdCollectionItemModel =
                await this.collectionsService.AddToCollection<AddToCollectionResourceModel>(
                    this.testAddToCollectionInputModel,
                    Guid.NewGuid());

            var createdCollectionItem =
                await this.dbContext.Collections.FirstOrDefaultAsync(ci => ci.Id == createdCollectionItemModel.Id);

            Assert.NotNull(createdCollectionItem);
        }

        [Fact]
        public async Task AddToCollectionShouldAddCollectionItemWithCorrectData()
        {
            var release = new Release();

            var user = new VinylExchangeUser();

            this.releasesEntityRetrieverMock.Setup(x => x.GetRelease(It.IsAny<Guid?>())).ReturnsAsync(release);

            this.usersEntityRetrieverMock.Setup(x => x.GetUser(It.IsAny<Guid?>())).ReturnsAsync(user);

            var createdCollectionItemModel =
                await this.collectionsService.AddToCollection<AddToCollectionResourceModel>(
                    this.testAddToCollectionInputModel,
                    Guid.NewGuid());

            var createdCollectionItem =
                await this.dbContext.Collections.FirstOrDefaultAsync(ci => ci.Id == createdCollectionItemModel.Id);

            Assert.Equal(this.testAddToCollectionInputModel.VinylGrade, createdCollectionItem.VinylGrade);
            Assert.Equal(this.testAddToCollectionInputModel.SleeveGrade, createdCollectionItem.SleeveGrade);
            Assert.Equal(this.testAddToCollectionInputModel.Description, createdCollectionItem.Description);
            Assert.Equal(release.Id, createdCollectionItem.ReleaseId);
            Assert.Equal(user.Id, createdCollectionItem.UserId);
        }

        [Fact]
        public async Task AddToCollectionShouldThrowNullRefferenceExceptionIfProvidedReleaseIdIsNotInDb()
        {
            var user = new VinylExchangeUser();

            this.releasesEntityRetrieverMock.Setup(x => x.GetRelease(It.IsAny<Guid?>())).ReturnsAsync((Release)null);

            this.usersEntityRetrieverMock.Setup(x => x.GetUser(It.IsAny<Guid?>())).ReturnsAsync(user);

            var exception = await Assert.ThrowsAsync<NullReferenceException>(
                                async () => await this.collectionsService.AddToCollection<AddToCollectionResourceModel>(
                                                this.testAddToCollectionInputModel,
                                                user.Id));

            Assert.Equal(ReleaseNotFound, exception.Message);
        }

        [Fact]
        public async Task AddToCollectionShouldThrowNullRefferenceExceptionIfProvidedUserIdIsNotInDb()
        {
            var release = new Release();

            this.releasesEntityRetrieverMock.Setup(x => x.GetRelease(It.IsAny<Guid?>())).ReturnsAsync(release);

            this.usersEntityRetrieverMock.Setup(x => x.GetUser(It.IsAny<Guid?>()))
                .ReturnsAsync((VinylExchangeUser)null);

            var exception = await Assert.ThrowsAsync<NullReferenceException>(
                                async () => await this.collectionsService.AddToCollection<AddToCollectionResourceModel>(
                                                this.testAddToCollectionInputModel,
                                                Guid.NewGuid()));

            Assert.Equal(UserNotFound, exception.Message);
        }

        [Fact]
        public async Task RemoveCollectionItemShouldRemoveCollectionItem()
        {
            var collectionItem = new CollectionItem();

            await this.dbContext.Collections.AddAsync(collectionItem);

            await this.dbContext.SaveChangesAsync();

            await this.collectionsService.RemoveCollectionItem<RemoveCollectionItemResourceModel>(collectionItem.Id);

            var removedCollectionItem =
                await this.dbContext.Collections.FirstOrDefaultAsync(ci => ci.Id == collectionItem.Id);

            Assert.Null(removedCollectionItem);
        }

        [Fact]
        public async Task RemoveCollectionItemShouldThrowNullRefferenceExceptionIfProvidedCollectionItemIdIsNotInDb()
        {
            var exception = await Assert.ThrowsAsync<NullReferenceException>(
                                async () => await this.collectionsService
                                                .RemoveCollectionItem<RemoveCollectionItemResourceModel>(
                                                    Guid.NewGuid()));

            Assert.Equal(CollectionItemNotFound, exception.Message);
        }

        [Fact]
        public async Task GetUserCollectionShouldGetUserCollection()
        {
            var user = new VinylExchangeUser();

            var userTwo = new VinylExchangeUser();

            for (var i = 0; i < 6; i++)
            {
                var collectionItem = new CollectionItem { UserId = user.Id };

                await this.dbContext.Collections.AddAsync(collectionItem);
            }

            for (var i = 0; i < 6; i++)
            {
                var collectionItem = new CollectionItem { UserId = userTwo.Id };

                await this.dbContext.Collections.AddAsync(collectionItem);
            }

            await this.dbContext.SaveChangesAsync();

            var userCollectionModels =
                await this.collectionsService.GetUserCollection<GetCollectionItemUserIdResourceModel>(user.Id);

            Assert.True(userCollectionModels.Count == 6);
            Assert.True(userCollectionModels.All(ucm => ucm.UserId == user.Id));
        }

        [Fact]
        public async Task GetUserCollectionShouldReturnEmptyListIfUserHasEmptyCollection()
        {
            var user = new VinylExchangeUser();

            for (var i = 0; i < 6; i++)
            {
                var collectionItem = new CollectionItem { UserId = user.Id };

                await this.dbContext.Collections.AddAsync(collectionItem);
            }

            await this.dbContext.SaveChangesAsync();

            var userCollectionModels =
                await this.collectionsService.GetUserCollection<GetCollectionItemUserIdResourceModel>(Guid.NewGuid());

            Assert.True(userCollectionModels.Count == 0);
        }

        [Fact]
        public async Task GetCollectionItemShouldGetCollectionItemIfProvidedCollectionItemIsInDb()
        {
            var collectionItem = new CollectionItem();

            await this.dbContext.Collections.AddAsync(collectionItem);

            await this.dbContext.SaveChangesAsync();

            var collectionItemModel =
                this.collectionsService.GetCollectionItem<GetCollectionItemResourceModel>(collectionItem.Id);

            Assert.NotNull(collectionItemModel);
        }

        [Fact]
        public async Task GetSaleShouldReturnNullIfProvidedSaleIsNotInDb()
        {
            var collectionItem = new CollectionItem();

            await this.dbContext.Collections.AddAsync(collectionItem);

            await this.dbContext.SaveChangesAsync();

            var collectionItemModel =
                await this.collectionsService.GetCollectionItem<GetCollectionItemResourceModel>(Guid.NewGuid());

            Assert.Null(collectionItemModel);
        }

        [Fact]
        public async Task
            DoesUserCollectionContainReleaseShouldReturnTrueIfUserHasCollectionItemInCollectionWithTheProvidedReleaseId()
        {
            var release = new Release();

            var user = new VinylExchangeUser();

            var collectionItem = new CollectionItem { ReleaseId = release.Id, UserId = user.Id };

            await this.dbContext.Collections.AddAsync(collectionItem);

            await this.dbContext.SaveChangesAsync();

            Assert.True(await this.collectionsService.DoesUserCollectionContainRelease(release.Id, user.Id));
        }

        [Fact]
        public async Task
            DoesUserCollectionContainReleaseShouldReturnFalseIfUserDoesntHaveCollectionItemInCollectionWithTheProvidedReleaseId()
        {
            var release = new Release();

            var user = new VinylExchangeUser();

            var collectionItem = new CollectionItem { ReleaseId = release.Id, UserId = user.Id };

            await this.dbContext.Collections.AddAsync(collectionItem);

            await this.dbContext.SaveChangesAsync();

            Assert.False(await this.collectionsService.DoesUserCollectionContainRelease(release.Id, Guid.NewGuid()));
        }
    }
}
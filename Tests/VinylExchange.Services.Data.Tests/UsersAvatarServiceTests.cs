namespace VinylExchange.Services.Data.Tests
{
    #region

    #region

    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Moq;

    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Data.HelperServices.Users;
    using VinylExchange.Services.Data.MainServices.Users.Contracts;
    using VinylExchange.Services.Data.Tests.TestFactories;

    using Xunit;

    using static VinylExchange.Common.Constants.NullReferenceExceptionsConstants;

    #endregion

    #endregion

    public class UsersAvatarServiceTests
    {
        private readonly VinylExchangeDbContext dbContext;

        private readonly IUsersAvatarService usersAvatarService;

        private readonly Mock<IUsersEntityRetriever> usersEntityRetrieverMock;

        public UsersAvatarServiceTests()
        {
            this.dbContext = DbFactory.CreateDbContext();

            this.usersEntityRetrieverMock = new Mock<IUsersEntityRetriever>();

            this.usersAvatarService = new UsersAvatarService(this.dbContext, this.usersEntityRetrieverMock.Object);
        }

        [Fact]
        public async Task ChangeAvatarShouldChangeAvatar()
        {
            var user = new VinylExchangeUser
                {
                    Avatar = new byte[] { 188, 65, 69, 67, 44, 187, 37, 66, 2, 17, 210, 249, 100 }
                };

            await this.dbContext.Users.AddAsync(user);

            await this.dbContext.SaveChangesAsync();

            this.usersEntityRetrieverMock.Setup(x => x.GetUser(It.IsAny<Guid?>())).ReturnsAsync(user);

            var newAvatar = new byte[]
                {
                    151, 152, 79, 173, 237, 43, 159, 58, 123, 207, 252, 218, 60, 25, 124, 191, 153, 252, 75, 16, 110
                };

            await this.usersAvatarService.ChangeAvatar(newAvatar, user.Id);

            var userFromDb = await this.dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

            Assert.Equal(string.Join(",", newAvatar), string.Join(",", userFromDb.Avatar));
        }

        [Fact]
        public async Task ChangeAvatarShouldThrowNullReferenceExceptionIfUserIsNotInDb()
        {
            var newAvatar = new byte[]
                {
                    151, 152, 79, 173, 237, 43, 159, 58, 123, 207, 252, 218, 60, 25, 124, 191, 153, 252, 75, 16, 110
                };

            var exception = await Assert.ThrowsAsync<NullReferenceException>(
                                async () => await this.usersAvatarService.ChangeAvatar(newAvatar, Guid.NewGuid()));

            Assert.Equal(UserNotFound, exception.Message);
        }

        [Fact]
        public async Task GetUserAvatarShouldGetUserAvatar()
        {
            var user = new VinylExchangeUser
                {
                    Avatar = new byte[] { 188, 65, 69, 67, 44, 187, 37, 66, 2, 17, 210, 249, 100 }
                };

            await this.dbContext.Users.AddAsync(user);

            await this.dbContext.SaveChangesAsync();

            var userAvatar = (await this.dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id)).Avatar;

            Assert.Equal(string.Join(",", user.Avatar), string.Join(",", userAvatar));
        }

        [Fact]
        public async Task GetUserAvatarShouldReturnNullIfUserIsNotInDb()
        {
            var userAvatar = await this.usersAvatarService.GetUserAvatar(Guid.NewGuid());

            Assert.Null(userAvatar);
        }
    }
}
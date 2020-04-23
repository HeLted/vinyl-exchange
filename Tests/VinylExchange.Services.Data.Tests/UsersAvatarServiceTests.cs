namespace VinylExchange.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;
    using HelperServices.Users;
    using MainServices.Users.Contracts;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using TestFactories;
    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using Xunit;
    using static Common.Constants.NullReferenceExceptionsConstants;


    public class UsersAvatarServiceTests
    {
        private readonly VinylExchangeDbContext dbContext;

        private readonly IUsersAvatarService usersAvatarService;

        private readonly Mock<IUsersEntityRetriever> usersEntityRetrieverMock;

        public UsersAvatarServiceTests()
        {
            dbContext = DbFactory.CreateDbContext();

            usersEntityRetrieverMock = new Mock<IUsersEntityRetriever>();

            usersAvatarService = new UsersAvatarService(dbContext, usersEntityRetrieverMock.Object);
        }

        [Fact]
        public async Task ChangeAvatarShouldChangeAvatar()
        {
            var user = new VinylExchangeUser
            {
                Avatar = new byte[] {188, 65, 69, 67, 44, 187, 37, 66, 2, 17, 210, 249, 100}
            };

            await dbContext.Users.AddAsync(user);

            await dbContext.SaveChangesAsync();

            usersEntityRetrieverMock.Setup(x => x.GetUser(It.IsAny<Guid?>())).ReturnsAsync(user);

            var newAvatar = new byte[]
            {
                151, 152, 79, 173, 237, 43, 159, 58, 123, 207, 252, 218, 60, 25, 124, 191, 153, 252, 75, 16, 110
            };

            await usersAvatarService.ChangeAvatar(newAvatar, user.Id);

            var userFromDb = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

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
                async () => await usersAvatarService.ChangeAvatar(newAvatar, Guid.NewGuid()));

            Assert.Equal(UserNotFound, exception.Message);
        }

        [Fact]
        public async Task GetUserAvatarShouldGetUserAvatar()
        {
            var user = new VinylExchangeUser
            {
                Avatar = new byte[] {188, 65, 69, 67, 44, 187, 37, 66, 2, 17, 210, 249, 100}
            };

            await dbContext.Users.AddAsync(user);

            await dbContext.SaveChangesAsync();

            var userAvatar = (await dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id)).Avatar;

            Assert.Equal(string.Join(",", user.Avatar), string.Join(",", userAvatar));
        }

        [Fact]
        public async Task GetUserAvatarShouldReturnNullIfUserIsNotInDb()
        {
            var userAvatar = await usersAvatarService.GetUserAvatar(Guid.NewGuid());

            Assert.Null(userAvatar);
        }
    }
}
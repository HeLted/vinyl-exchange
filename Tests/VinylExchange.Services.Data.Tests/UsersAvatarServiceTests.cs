namespace VinylExchange.Services.Data.Tests
{
    using System.Threading.Tasks;
    #region

    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Data.HelperServices.Users;
    using VinylExchange.Services.Data.Tests.TestFactories;
    using Xunit;

    #endregion

    public class UsersAvatarServiceTests
    {
        private readonly VinylExchangeDbContext dbContext;

        private readonly IUsersAvatarService usersAvatarService;

        public UsersAvatarServiceTests()
        {
            this.dbContext = DbFactory.CreateDbContext();

            this.usersAvatarService = new UsersAvatarService(this.dbContext);
        }

        [Fact]
        public async Task ChangeAvatarShouldChangeAvatar()
        {

            var user = new VinylExchangeUser(){Avatar = new byte[] {188, 65, 69, 67, 44, 187, 37, 66, 2, 17, 210, 249, 100}};

            await this.dbContext.Users.AddAsync(user);

            await this.dbContext.SaveChangesAsync();
         


            this.usersAvatarService.ChangeAvatar(,user.Id);


        }
    }
}
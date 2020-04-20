namespace VinylExchange.Services.Data.Tests
{
    #region

    using VinylExchange.Data;
    using VinylExchange.Services.Data.HelperServices.Users;
    using VinylExchange.Services.Data.Tests.TestFactories;

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
    }
}
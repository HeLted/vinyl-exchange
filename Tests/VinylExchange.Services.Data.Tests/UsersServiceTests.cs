namespace VinylExchange.Services.Data.Tests
{
    #region

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;

    using Moq;
    using System.Threading.Tasks;
    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Data.MainServices.Users;
    using VinylExchange.Services.Data.Tests.TestFactories;
    using VinylExchange.Services.EmailSender;
    using Xunit;

    #endregion

    public class UsersServiceTests
    {
        private readonly VinylExchangeDbContext dbContext;

        private readonly IUsersService usersService;

        private readonly Mock<UserManager<VinylExchangeUser>> userManagerMock;

        private readonly Mock<SignInManager<VinylExchangeUser>> signInManagerMock;

        public UsersServiceTests()
        {
            this.dbContext = DbFactory.CreateDbContext();

            this.userManagerMock = new Mock<UserManager<VinylExchangeUser>>();

            this.signInManagerMock = new Mock<SignInManager<VinylExchangeUser>>();

            this.usersService = new UsersService(
                this.userManagerMock.Object,
                this.signInManagerMock.Object,
                new Mock<IEmailSender>().Object,
                new Mock<IHttpContextAccessor>().Object);
        }      
    }
}
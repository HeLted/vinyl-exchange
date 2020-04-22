namespace VinylExchange.Services.Data.Tests
{
   
    #region
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Moq;
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Data.MainServices.Users;
    using VinylExchange.Services.Data.MainServices.Users.Contracts;
    using VinylExchange.Services.Data.Tests.Helpers;
    using VinylExchange.Services.Data.Tests.TestFactories;
    using VinylExchange.Services.EmailSender;
    using Xunit;

    using static VinylExchange.Common.Constants.NullReferenceExceptionsConstants;

    #endregion

    public class UsersServiceTests
    {
        private readonly VinylExchangeDbContext dbContext;

        private readonly IUsersService usersService;

        private readonly Mock<FakeUserManager> userManagerMock;

        private readonly Mock<SignInManager<VinylExchangeUser>> signInManagerMock;

        private readonly Mock<IHttpContextAccessor> contextAccessorMock;

        private readonly Mock<IEmailSender> emailSenderMock;

        public UsersServiceTests()
        {
            this.dbContext = DbFactory.CreateDbContext();

            this.emailSenderMock = new Mock<IEmailSender>();

            this.userManagerMock = new Mock<FakeUserManager>();

            this.contextAccessorMock = new Mock<IHttpContextAccessor>();

            this.contextAccessorMock.SetupGet(x => x.HttpContext.Request).Returns(new Mock<HttpRequest>().Object);

            this.usersService = new UsersService(
                this.userManagerMock.Object,
                new FakeSignInManager(),
                this.emailSenderMock.Object,
                this.contextAccessorMock.Object);
        }

        [Fact]
        public async Task SendConfirmEmailShouldNotThrowAnyException()
        {
            var user = new VinylExchangeUser();

            this.userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);

            this.emailSenderMock.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();

            var exception = await Record.ExceptionAsync(async () => await this.usersService.SendConfirmEmail(user.Id));

            Assert.Null(exception);

        }

        [Fact]
        public async Task SendConfirmEmailShoulThrowNullReferenceExceptionIfUserIsNotFound()
        {
            this.userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((VinylExchangeUser)null);

            var exception = await Record.ExceptionAsync(async () => await this.usersService.SendConfirmEmail(Guid.NewGuid()));

            Assert.Equal(UserCannotBeNull, exception.Message);
        }

        [Fact]
        public async Task SendChangeEmailEmailShouldNotThrowAnyException()
        {
            var user = new VinylExchangeUser();

            this.userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);

            this.emailSenderMock.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();

            var exception = await Record.ExceptionAsync(async () => await this.usersService.SendChangeEmailEmail("testEmail@abv.bg", user.Id));

            Assert.Null(exception);
        }

        [Fact]
        public async Task SendChangePasswordEmailShouldNotThrowAnyException()
        {
            var user = new VinylExchangeUser();

            this.userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);

            this.emailSenderMock.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();

            var exception = await Record.ExceptionAsync(async () => await this.usersService.SendChangePasswordEmail(user.Id));

            Assert.Null(exception);
        }

        [Fact]
        public async Task SendResetPasswordEmailShouldNotThrowAnyException()
        {
            var user = new VinylExchangeUser();

            this.userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);

            this.emailSenderMock.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();

            var exception = await Record.ExceptionAsync(async () => await this.usersService.SendResetPasswordEmail("test@abv.bg"));

            Assert.Null(exception);
        }
    }
}
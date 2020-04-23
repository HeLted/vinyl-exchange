namespace VinylExchange.Services.Data.Tests
{

    #region
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Moq;
    using System;
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

        private readonly UsersService usersService;

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
        public async Task SendChangeEmailEmailShouldThrowNullReferenceExceptionIfUserIsNotFound()
        {
            this.userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((VinylExchangeUser)null);

            var exception = await Record.ExceptionAsync(async () => await this.usersService.SendChangeEmailEmail("323123@arwrw.bg",Guid.NewGuid()));

            Assert.Equal(UserCannotBeNull, exception.Message);
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
        public async Task  SendChangePasswordEmailShouldThrowNullReferenceExceptionIfUserIsNotFound()
        {
            this.userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((VinylExchangeUser)null);

            var exception = await Record.ExceptionAsync(async () => await this.usersService.SendChangePasswordEmail(Guid.NewGuid()));

            Assert.Equal(UserCannotBeNull, exception.Message);
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

        [Fact]
        public async Task SendResetPasswordEmailShouldThrowNullReferenceExceptionIfUserIsNotFound()
        {
            this.userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((VinylExchangeUser)null);

            var exception = await Record.ExceptionAsync(async () => await this.usersService.SendResetPasswordEmail(Guid.NewGuid().ToString()));

            Assert.Equal(UserWithEmailCannotBeFound, exception.Message);
        }

        [Fact]
        public async Task  ChangeEmailShouldThrowNullReferenceExceptionIfUserIsNotFound()
        {
            this.userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((VinylExchangeUser)null);

            var exception = await Record.ExceptionAsync(async () => await this.usersService.ChangeEmail("ewew","3232",Guid.NewGuid()));

            Assert.Equal(UserCannotBeNull, exception.Message);
        }


        [Fact]
        public async Task  ConfirmEmailShouldThrowNullReferenceExceptionIfUserIsNotFound()
        {
            this.userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((VinylExchangeUser)null);

            var exception = await Record.ExceptionAsync(async () => await this.usersService.ConfirmEmail("ewew", Guid.NewGuid()));

            Assert.Equal(UserCannotBeNull, exception.Message);
        }

        [Fact]
        public async Task  ConfirmEmailShouldReturnIdentityResultSuccess()
        {
            var user = new VinylExchangeUser();

            this.userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);

            this.userManagerMock.Setup(x=> x.ConfirmEmailAsync(It.IsAny<VinylExchangeUser>(),It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            var result = await this.usersService.ConfirmEmail("ewew",Guid.NewGuid());

            Assert.True(result.Succeeded);
        }

        [Fact]
        public async Task  ResetPasswordShouldThrowNullReferenceExceptionIfUserIsNotFound()
        {
            this.userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((VinylExchangeUser)null);

            var exception = await Record.ExceptionAsync(async () => await this.usersService.ResetPassword("ewew","eweww","ewewe"));

            Assert.Equal(UserCannotBeNull, exception.Message);
        }

        [Fact]
        public async Task   ResetPasswordShouldReturnIdentityResultSuccess()
        {
            var user = new VinylExchangeUser();

            this.userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);

            this.userManagerMock.Setup(x=> x.ResetPasswordAsync(It.IsAny<VinylExchangeUser>(),It.IsAny<string>(),It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            var result = await this.usersService.ResetPassword("ewew",Guid.NewGuid().ToString(),"ewew");

            Assert.True(result.Succeeded);
        }

        
        [Fact]
        public async Task  ChangeEmailShouldReturnIdentityResultSuccess()
        {
            var user = new VinylExchangeUser();

            this.userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);

            this.userManagerMock.Setup(x=> x.ChangeEmailAsync(It.IsAny<VinylExchangeUser>(),It.IsAny<string>(),It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            var result = await this.usersService.ChangeEmail("ewew","3232",Guid.NewGuid());

            Assert.True(result.Succeeded);
        }

        [Fact]
        public async Task GetUserShouldGetUser()
        {
            var user = new VinylExchangeUser();

            this.userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);

            var returnedUser = await this.usersService.GetUser(user.Id);

            Assert.NotNull(returnedUser);
        }
    }
}
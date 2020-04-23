namespace VinylExchange.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EmailSender;
    using Helpers;
    using MainServices.Users;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Moq;
    using TestFactories;
    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using Xunit;
    using static Common.Constants.NullReferenceExceptionsConstants;


    public class UsersServiceTests
    {
        public UsersServiceTests()
        {
            this.dbContext = DbFactory.CreateDbContext();

            this.passwordHasherMock = new Mock<IPasswordHasher<VinylExchangeUser>>();

            this.emailSenderMock = new Mock<IEmailSender>();

            this.userManagerMock = new Mock<FakeUserManager>(this.passwordHasherMock);

            this.signInManagerMock = new Mock<FakeSignInManager>(this.passwordHasherMock);

            this.contextAccessorMock = new Mock<IHttpContextAccessor>();

            this.passwordHasherMock.Setup(x => x.HashPassword(It.IsAny<VinylExchangeUser>(), It.IsAny<string>()))
                .Returns("look at me I am a hashed password");

            this.contextAccessorMock.SetupGet(x => x.HttpContext.Request).Returns(new Mock<HttpRequest>().Object);

            this.usersService = new UsersService(this.userManagerMock.Object, this.signInManagerMock.Object,
                this.emailSenderMock.Object, this.contextAccessorMock.Object);
        }

        private readonly VinylExchangeDbContext dbContext;

        private readonly UsersService usersService;

        private readonly Mock<FakeUserManager> userManagerMock;

        private readonly Mock<FakeSignInManager> signInManagerMock;

        private readonly Mock<IHttpContextAccessor> contextAccessorMock;

        private readonly Mock<IEmailSender> emailSenderMock;

        private readonly Mock<IPasswordHasher<VinylExchangeUser>> passwordHasherMock;


        [Fact]
        public async Task ChangeEmailShouldReturnIdentityResultSuccess()
        {
            var user = new VinylExchangeUser();

            this.userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);

            this.userManagerMock
                .Setup(x => x.ChangeEmailAsync(It.IsAny<VinylExchangeUser>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var result = await this.usersService.ChangeEmail("ewew", "3232", Guid.NewGuid());

            Assert.True(result.Succeeded);
        }

        [Fact]
        public async Task ChangeEmailShouldThrowNullReferenceExceptionIfUserIsNotFound()
        {
            this.userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((VinylExchangeUser) null);

            var exception = await Record.ExceptionAsync(async () =>
                await this.usersService.ChangeEmail("ewew", "3232", Guid.NewGuid()));

            Assert.Equal(UserCannotBeNull, exception.Message);
        }

        [Fact]
        public async Task ConfirmEmailShouldReturnIdentityResultSuccess()
        {
            var user = new VinylExchangeUser();

            this.userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);

            this.userManagerMock.Setup(x => x.ConfirmEmailAsync(It.IsAny<VinylExchangeUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var result = await this.usersService.ConfirmEmail("ewew", Guid.NewGuid());

            Assert.True(result.Succeeded);
        }


        [Fact]
        public async Task ConfirmEmailShouldThrowNullReferenceExceptionIfUserIsNotFound()
        {
            this.userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((VinylExchangeUser) null);

            var exception =
                await Record.ExceptionAsync(async () => await this.usersService.ConfirmEmail("ewew", Guid.NewGuid()));

            Assert.Equal(UserCannotBeNull, exception.Message);
        }

        [Fact]
        public void ExternalLoginsShouldGetExternalLoginsProperty()
        {
            this.usersService.ExternalLogins = new List<AuthenticationScheme>();

            Assert.NotNull(this.usersService.ExternalLogins);
        }

        [Fact]
        public async Task GetUserShouldGetUser()
        {
            var user = new VinylExchangeUser();

            this.userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);

            var returnedUser = await this.usersService.GetUser(user.Id);

            Assert.NotNull(returnedUser);
        }


        [Fact]
        public async Task LoginUserShouldReturnSignInResultSuccess()
        {
            var user = new VinylExchangeUser();

            this.signInManagerMock.Setup(x => x.GetExternalAuthenticationSchemesAsync())
                .ReturnsAsync(new List<AuthenticationScheme>());

            this.signInManagerMock
                .Setup(x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(),
                    It.IsAny<bool>())).ReturnsAsync(SignInResult.Success);

            var result = await this.usersService.LoginUser("ewew", "3232", false);

            Assert.True(result.Succeeded);
        }

        [Fact]
        public async Task RegisterUserShouldReturnIdentityResultSuccess()
        {
            var user = new VinylExchangeUser();

            this.userManagerMock.Setup(x => x.CreateAsync(It.IsAny<VinylExchangeUser>()))
                .ReturnsAsync(IdentityResult.Success);

            this.userManagerMock.Setup(x => x.AddToRoleAsync(It.IsAny<VinylExchangeUser>(), It.IsAny<string>()))
                .Verifiable();

            var result = await this.usersService.RegisterUser("ewew", "3232", "eweqw");

            Assert.True(result.Succeeded);
        }

        [Fact]
        public async Task ResetPasswordShouldReturnIdentityResultSuccess()
        {
            var user = new VinylExchangeUser();

            this.userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);

            this.userManagerMock
                .Setup(x => x.ResetPasswordAsync(It.IsAny<VinylExchangeUser>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var result = await this.usersService.ResetPassword("ewew", Guid.NewGuid().ToString(), "ewew");

            Assert.True(result.Succeeded);
        }

        [Fact]
        public async Task ResetPasswordShouldThrowNullReferenceExceptionIfUserIsNotFound()
        {
            this.userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((VinylExchangeUser) null);

            var exception =
                await Record.ExceptionAsync(async () =>
                    await this.usersService.ResetPassword("ewew", "eweww", "ewewe"));

            Assert.Equal(UserCannotBeNull, exception.Message);
        }

        [Fact]
        public async Task SendChangeEmailEmailShouldNotThrowAnyException()
        {
            var user = new VinylExchangeUser();

            this.userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);

            this.emailSenderMock
                .Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Verifiable();

            var exception = await Record.ExceptionAsync(async () =>
                await this.usersService.SendChangeEmailEmail("testEmail@abv.bg", user.Id));

            Assert.Null(exception);
        }

        [Fact]
        public async Task SendChangeEmailEmailShouldThrowNullReferenceExceptionIfUserIsNotFound()
        {
            this.userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((VinylExchangeUser) null);

            var exception = await Record.ExceptionAsync(async () =>
                await this.usersService.SendChangeEmailEmail("323123@arwrw.bg", Guid.NewGuid()));

            Assert.Equal(UserCannotBeNull, exception.Message);
        }

        [Fact]
        public async Task SendChangePasswordEmailShouldNotThrowAnyException()
        {
            var user = new VinylExchangeUser();

            this.userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);

            this.emailSenderMock
                .Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Verifiable();

            var exception =
                await Record.ExceptionAsync(async () => await this.usersService.SendChangePasswordEmail(user.Id));

            Assert.Null(exception);
        }

        [Fact]
        public async Task SendChangePasswordEmailShouldThrowNullReferenceExceptionIfUserIsNotFound()
        {
            this.userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((VinylExchangeUser) null);

            var exception =
                await Record.ExceptionAsync(async () =>
                    await this.usersService.SendChangePasswordEmail(Guid.NewGuid()));

            Assert.Equal(UserCannotBeNull, exception.Message);
        }

        [Fact]
        public async Task SendConfirmEmailShouldNotThrowAnyException()
        {
            var user = new VinylExchangeUser();

            this.userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);

            this.emailSenderMock
                .Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Verifiable();

            var exception = await Record.ExceptionAsync(async () => await this.usersService.SendConfirmEmail(user.Id));

            Assert.Null(exception);
        }

        [Fact]
        public async Task SendConfirmEmailShoulThrowNullReferenceExceptionIfUserIsNotFound()
        {
            this.userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((VinylExchangeUser) null);

            var exception =
                await Record.ExceptionAsync(async () => await this.usersService.SendConfirmEmail(Guid.NewGuid()));

            Assert.Equal(UserCannotBeNull, exception.Message);
        }


        [Fact]
        public async Task SendResetPasswordEmailShouldNotThrowAnyException()
        {
            var user = new VinylExchangeUser();

            this.userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);

            this.emailSenderMock
                .Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Verifiable();

            var exception =
                await Record.ExceptionAsync(async () => await this.usersService.SendResetPasswordEmail("test@abv.bg"));

            Assert.Null(exception);
        }

        [Fact]
        public async Task SendResetPasswordEmailShouldThrowNullReferenceExceptionIfUserIsNotFound()
        {
            this.userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((VinylExchangeUser) null);

            var exception = await Record.ExceptionAsync(async () =>
                await this.usersService.SendResetPasswordEmail(Guid.NewGuid().ToString()));

            Assert.Equal(UserWithEmailCannotBeFound, exception.Message);
        }
    }
}
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
        private readonly VinylExchangeDbContext dbContext;

        private readonly UsersService usersService;

        private readonly Mock<FakeUserManager> userManagerMock;

        private readonly Mock<FakeSignInManager> signInManagerMock;

        private readonly Mock<IHttpContextAccessor> contextAccessorMock;

        private readonly Mock<IEmailSender> emailSenderMock;

        private readonly Mock<IPasswordHasher<VinylExchangeUser>> passwordHasherMock;

        public UsersServiceTests()
        {
            dbContext = DbFactory.CreateDbContext();

            passwordHasherMock = new Mock<IPasswordHasher<VinylExchangeUser>>();

            emailSenderMock = new Mock<IEmailSender>();

            userManagerMock = new Mock<FakeUserManager>(passwordHasherMock);

            signInManagerMock = new Mock<FakeSignInManager>(passwordHasherMock);

            contextAccessorMock = new Mock<IHttpContextAccessor>();

            passwordHasherMock.Setup(x => x.HashPassword(It.IsAny<VinylExchangeUser>(), It.IsAny<string>()))
                .Returns("look at me I am a hashed password");

            contextAccessorMock.SetupGet(x => x.HttpContext.Request).Returns(new Mock<HttpRequest>().Object);

            usersService = new UsersService(
                userManagerMock.Object,
                signInManagerMock.Object,
                emailSenderMock.Object,
                contextAccessorMock.Object);
        }

        [Fact]
        public async Task SendConfirmEmailShouldNotThrowAnyException()
        {
            var user = new VinylExchangeUser();

            userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);

            emailSenderMock.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Verifiable();

            var exception = await Record.ExceptionAsync(async () => await usersService.SendConfirmEmail(user.Id));

            Assert.Null(exception);
        }

        [Fact]
        public async Task SendConfirmEmailShoulThrowNullReferenceExceptionIfUserIsNotFound()
        {
            userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((VinylExchangeUser) null);

            var exception =
                await Record.ExceptionAsync(async () => await usersService.SendConfirmEmail(Guid.NewGuid()));

            Assert.Equal(UserCannotBeNull, exception.Message);
        }

        [Fact]
        public async Task SendChangeEmailEmailShouldNotThrowAnyException()
        {
            var user = new VinylExchangeUser();

            userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);

            emailSenderMock.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Verifiable();

            var exception = await Record.ExceptionAsync(async () =>
                await usersService.SendChangeEmailEmail("testEmail@abv.bg", user.Id));

            Assert.Null(exception);
        }

        [Fact]
        public async Task SendChangeEmailEmailShouldThrowNullReferenceExceptionIfUserIsNotFound()
        {
            userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((VinylExchangeUser) null);

            var exception = await Record.ExceptionAsync(async () =>
                await usersService.SendChangeEmailEmail("323123@arwrw.bg", Guid.NewGuid()));

            Assert.Equal(UserCannotBeNull, exception.Message);
        }

        [Fact]
        public async Task SendChangePasswordEmailShouldNotThrowAnyException()
        {
            var user = new VinylExchangeUser();

            userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);

            emailSenderMock.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Verifiable();

            var exception =
                await Record.ExceptionAsync(async () => await usersService.SendChangePasswordEmail(user.Id));

            Assert.Null(exception);
        }

        [Fact]
        public async Task SendChangePasswordEmailShouldThrowNullReferenceExceptionIfUserIsNotFound()
        {
            userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((VinylExchangeUser) null);

            var exception =
                await Record.ExceptionAsync(async () => await usersService.SendChangePasswordEmail(Guid.NewGuid()));

            Assert.Equal(UserCannotBeNull, exception.Message);
        }


        [Fact]
        public async Task SendResetPasswordEmailShouldNotThrowAnyException()
        {
            var user = new VinylExchangeUser();

            userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);

            emailSenderMock.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Verifiable();

            var exception =
                await Record.ExceptionAsync(async () => await usersService.SendResetPasswordEmail("test@abv.bg"));

            Assert.Null(exception);
        }

        [Fact]
        public async Task SendResetPasswordEmailShouldThrowNullReferenceExceptionIfUserIsNotFound()
        {
            userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((VinylExchangeUser) null);

            var exception = await Record.ExceptionAsync(async () =>
                await usersService.SendResetPasswordEmail(Guid.NewGuid().ToString()));

            Assert.Equal(UserWithEmailCannotBeFound, exception.Message);
        }

        [Fact]
        public async Task ChangeEmailShouldThrowNullReferenceExceptionIfUserIsNotFound()
        {
            userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((VinylExchangeUser) null);

            var exception = await Record.ExceptionAsync(async () =>
                await usersService.ChangeEmail("ewew", "3232", Guid.NewGuid()));

            Assert.Equal(UserCannotBeNull, exception.Message);
        }


        [Fact]
        public async Task ConfirmEmailShouldThrowNullReferenceExceptionIfUserIsNotFound()
        {
            userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((VinylExchangeUser) null);

            var exception =
                await Record.ExceptionAsync(async () => await usersService.ConfirmEmail("ewew", Guid.NewGuid()));

            Assert.Equal(UserCannotBeNull, exception.Message);
        }

        [Fact]
        public async Task ConfirmEmailShouldReturnIdentityResultSuccess()
        {
            var user = new VinylExchangeUser();

            userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);

            userManagerMock.Setup(x => x.ConfirmEmailAsync(It.IsAny<VinylExchangeUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var result = await usersService.ConfirmEmail("ewew", Guid.NewGuid());

            Assert.True(result.Succeeded);
        }

        [Fact]
        public async Task ResetPasswordShouldThrowNullReferenceExceptionIfUserIsNotFound()
        {
            userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((VinylExchangeUser) null);

            var exception =
                await Record.ExceptionAsync(async () => await usersService.ResetPassword("ewew", "eweww", "ewewe"));

            Assert.Equal(UserCannotBeNull, exception.Message);
        }

        [Fact]
        public async Task ResetPasswordShouldReturnIdentityResultSuccess()
        {
            var user = new VinylExchangeUser();

            userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);

            userManagerMock
                .Setup(x => x.ResetPasswordAsync(It.IsAny<VinylExchangeUser>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var result = await usersService.ResetPassword("ewew", Guid.NewGuid().ToString(), "ewew");

            Assert.True(result.Succeeded);
        }


        [Fact]
        public async Task ChangeEmailShouldReturnIdentityResultSuccess()
        {
            var user = new VinylExchangeUser();

            userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);

            userManagerMock
                .Setup(x => x.ChangeEmailAsync(It.IsAny<VinylExchangeUser>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var result = await usersService.ChangeEmail("ewew", "3232", Guid.NewGuid());

            Assert.True(result.Succeeded);
        }


        [Fact]
        public async Task LoginUserShouldReturnSignInResultSuccess()
        {
            var user = new VinylExchangeUser();

            signInManagerMock.Setup(x => x.GetExternalAuthenticationSchemesAsync())
                .ReturnsAsync(new List<AuthenticationScheme>());

            signInManagerMock
                .Setup(x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(),
                    It.IsAny<bool>())).ReturnsAsync(SignInResult.Success);

            var result = await usersService.LoginUser("ewew", "3232", false);

            Assert.True(result.Succeeded);
        }

        [Fact]
        public async Task RegisterUserShouldReturnIdentityResultSuccess()
        {
            var user = new VinylExchangeUser();

            userManagerMock.Setup(x => x.CreateAsync(It.IsAny<VinylExchangeUser>()))
                .ReturnsAsync(IdentityResult.Success);

            userManagerMock.Setup(x => x.AddToRoleAsync(It.IsAny<VinylExchangeUser>(), It.IsAny<string>()))
                .Verifiable();

            var result = await usersService.RegisterUser("ewew", "3232", "eweqw");

            Assert.True(result.Succeeded);
        }

        [Fact]
        public async Task GetUserShouldGetUser()
        {
            var user = new VinylExchangeUser();

            userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);

            var returnedUser = await usersService.GetUser(user.Id);

            Assert.NotNull(returnedUser);
        }

        [Fact]
        public void ExternalLoginsShouldGetExternalLoginsProperty()
        {
            usersService.ExternalLogins = new List<AuthenticationScheme>();

            Assert.NotNull(usersService.ExternalLogins);
        }
    }
}
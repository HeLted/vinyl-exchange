namespace VinylExchange.Services.Data.MainServices.Users
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;

    using VinylExchange.Common.Constants;
    using VinylExchange.Data.Models;
    using VinylExchange.Models.InputModels.Users;
    using VinylExchange.Services.EmailSender;
    using VinylExchange.Services.Mapping;
    using VinylExchange.Web.Models.InputModels.Users;
    using static VinylExchange.Common.Constants.NullReferenceExceptionsConstants;

    #endregion

    public class UsersService : IUsersService, IUsersEntityRetriever
    {
        private readonly IHttpContextAccessor contextAccessor;

        private readonly IEmailSender emailSender;

        private readonly SignInManager<VinylExchangeUser> signInManager;

        private readonly UserManager<VinylExchangeUser> userManager;

        public UsersService(
            UserManager<VinylExchangeUser> userManager,
            SignInManager<VinylExchangeUser> signInManager,
            IEmailSender emailSender,
            IHttpContextAccessor contextAccessor)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
            this.contextAccessor = contextAccessor;
        }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public async Task<IdentityResult> RegisterUser(RegisterUserInputModel inputModel)
        {
            var user = inputModel.To<VinylExchangeUser>();

            user.PasswordHash = this.userManager.PasswordHasher.HashPassword(user, inputModel.Password);

            var identityResult = await this.userManager.CreateAsync(user);

            await this.userManager.AddToRoleAsync(user, Roles.User);

            return identityResult;
        }

        public async Task<SignInResult> LoginUser(string username, string password, bool rememberMe)
        {
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            var identityResult = await this.signInManager.PasswordSignInAsync(
                                     username,
                                     password,
                                     rememberMe,
                                     false);

            return identityResult;
        }

        public async Task<IdentityResult> ConfirmEmail(string emailConfirmToken, Guid userId)
        {
            var user = await this.userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                throw new NullReferenceException(NullReferenceExceptionsConstants.UserCannotBeNull);
            }

            var identityResult = await this.userManager.ConfirmEmailAsync(user, emailConfirmToken);

            return identityResult;
        }

        public async Task<IdentityResult> ChangeEmail(string changeEmailToken, string newEmail, Guid userId)
        {
            var user = await this.userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                throw new NullReferenceException(NullReferenceExceptionsConstants.UserCannotBeNull);
            }

            var identityResult = await this.userManager.ChangeEmailAsync(
                                     user,
                                     newEmail,
                                     changeEmailToken);

            return identityResult;
        }

        public async Task<IdentityResult> ResetPassword(string resetPasswordToken, string email, string newPassword)
        {
            var user = await this.userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new NullReferenceException(NullReferenceExceptionsConstants.UserCannotBeNull);
            }

            var identityResult = await this.userManager.ResetPasswordAsync(
                user,
                resetPasswordToken, 
                newPassword);

            return identityResult;
        }

        public async Task SendConfirmEmail(Guid userId)
        {
            var user = await this.userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                throw new NullReferenceException(NullReferenceExceptionsConstants.UserCannotBeNull);
            }

            var emailContent = await this.ConstructConfirmationEmailContent(user);

            await this.emailSender.SendEmailAsync(user.Email, "Vinyl Exchange Confirmation Email", emailContent);
        }

        public async Task SendChangeEmailEmail(string newEmail, Guid userId)
        {
            var user = await this.userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                throw new NullReferenceException(NullReferenceExceptionsConstants.UserCannotBeNull);
            }

            var emailContent = await this.ConstructChangeEmailEmailContent(user, newEmail);

            await this.emailSender.SendEmailAsync(user.Email, "Vinyl Exchange Change Your Email", emailContent);
        }

        public async Task SendChangePasswordEmail(Guid userId)
        {
            var user = await this.userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                throw new NullReferenceException(NullReferenceExceptionsConstants.UserCannotBeNull);
            }

            var emailContent = await this.ConstructChangePasswordEmailContent(user);

            await this.emailSender.SendEmailAsync(user.Email, "Vinyl Exchange Password Change", emailContent);
        }

        public async Task SendResetPasswordEmail(string email)
        {
            var user = await this.userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new NullReferenceException(NullReferenceExceptionsConstants.UserWithEmailCannotBeFound);
            }

            var emailContent = await this.ConstructChangePasswordEmailContent(user);

            await this.emailSender.SendEmailAsync(user.Email, "Vinyl Exchange Password Reset", emailContent);
        }

        public async Task<VinylExchangeUser> GetUser(Guid? userId)
        {
            var user = await this.userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                throw new NullReferenceException(UserNotFound);
            }

            return user;
        }

        private async Task<string> ConstructConfirmationEmailContent(VinylExchangeUser user)
        {
            var emailConfirmationToken = await this.userManager.GenerateEmailConfirmationTokenAsync(user);

            var request = this.contextAccessor.HttpContext.Request;

            var emailConfirmationUrl = request.Scheme + "://" + request.Host
                                       + $"/Authentication/EmailConfirm?cofirmToken={emailConfirmationToken}";

            var confirmEmailHtmlContent =
                $@"<h1>Confirm Your Vinyl Exchange Account</h1>.Follow This <a href=""{emailConfirmationUrl}"">Link</a>";

            return confirmEmailHtmlContent;
        }

        private async Task<string> ConstructChangeEmailEmailContent(VinylExchangeUser user, string newEmail)
        {
            var changeEmailConfirmationToken = await this.userManager.GenerateChangeEmailTokenAsync(user, newEmail);

            var request = this.contextAccessor.HttpContext.Request;

            var emailChangeUrl = request.Scheme + "://" + request.Host
                                 + $"/Authentication/ChangeEmail?cofirmToken={changeEmailConfirmationToken}";

            var changeEmailHtmlContent =
                $@"<h1>Change Your Vinyl Exchange Email</h1>.Follow This <a href=""{emailChangeUrl}"">Link</a>";

            return changeEmailHtmlContent;
        }

        private async Task<string> ConstructChangePasswordEmailContent(VinylExchangeUser user)
        {
            var changePasswordConfirmationToken = await this.userManager.GeneratePasswordResetTokenAsync(user);

            var changePasswordHtmlContent =
                $@"<h1>Change Your Vinyl Exchange Password</h1><h3>Your password change/reset token is: {changePasswordConfirmationToken}<h3>";

            return changePasswordHtmlContent;
        }
    }
}
namespace VinylExchange.Services.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;

    using VinylExchange.Data.Models;
    using VinylExchange.Models.InputModels.Users;
    using VinylExchange.Services.EmaiSender;
    using VinylExchange.Services.Mapping;
    using static VinylExchange.Common.Constants.RolesConstants;
    using static VinylExchange.Common.Constants.NullReferenceExceptionsConstants;

    public class UsersService : IUsersService
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

        public async Task<IdentityResult> ConfirmUserEmail(ConfirmEmailInputModel inputModel)
        {
            VinylExchangeUser user = await this.userManager.FindByIdAsync(inputModel.UserId.ToString());

            if (user == null)
            {
                throw new NullReferenceException(UserCannotBeNull);
            }

            IdentityResult identityResult =
                await this.userManager.ConfirmEmailAsync(user, inputModel.EmailConfirmToken);

            return identityResult;
        }

        public async Task<SignInResult> LoginUser(LoginUserInputModel inputModel)
        {
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            SignInResult identityResult = await this.signInManager.PasswordSignInAsync(
                                              inputModel.Username,
                                              inputModel.Password,
                                              inputModel.RememberMe,
                                              lockoutOnFailure: false);

            return identityResult;
        }

        public async Task<IdentityResult> RegisterUser(RegisterUserInputModel inputModel)
        {
            VinylExchangeUser user = inputModel.To<VinylExchangeUser>();

            user.PasswordHash = this.userManager.PasswordHasher.HashPassword(user, inputModel.Password);

            IdentityResult identityResult = await this.userManager.CreateAsync(user);

            await this.userManager.AddToRoleAsync(user, User);


            return identityResult;
        }

        public async Task SendConfirmEmail(Guid userId)
        {
            VinylExchangeUser user = await this.userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                throw new NullReferenceException(UserCannotBeNull);
            }

            await this.ConstructConfirmationEmailContent(user);
        }

        private async Task<string> ConstructConfirmationEmailContent(VinylExchangeUser user)
        {
            string emailConfirmationToken = await this.userManager.GenerateEmailConfirmationTokenAsync(user);

            HttpRequest request = this.contextAccessor.HttpContext.Request;

            string emailConfirmationUrl = request.Scheme + "://" + request.Host
                + $"/Authentication/EmailConfirm?cofirmToken={emailConfirmationToken}&userId={user.Id}";

            string confirmEmailHtmlContent =
                $@"<h1>Confirm Your Vinyl Exchange Account</h1>.Follow This <a href=""{emailConfirmationUrl}"">Link</a>";
            
            return confirmEmailHtmlContent;
        }
    }
}
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
                throw new NullReferenceException("Confirming Email Failed.User Cannot Be Null!");
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

            await this.userManager.AddToRoleAsync(user, "User");

            if (identityResult.Succeeded)
            {
                await this.SendConfirmationEmail(user);
            }

            return identityResult;
        }

        private async Task SendConfirmationEmail(VinylExchangeUser user)
        {
            string emailConfirmationToken = await this.userManager.GenerateEmailConfirmationTokenAsync(user);

            string emailConfirmationUrl = this.contextAccessor.HttpContext.Request.Scheme + "://"
                                                                                          + this.contextAccessor
                                                                                              .HttpContext.Request.Host
                                                                                          + $"/Authentication/EmailConfirm?cofirmToken={emailConfirmationToken}&userId={user.Id}";

            await this.emailSender.SendEmailAsync(
                user.Email,
                "Vinyl Exchange Confirm Email",
                $@"<h1>Confirm Your Vinyl Exchange Account</h1>.Follow This <a href=""{emailConfirmationUrl}"">Link</a>");
        }
    }
}
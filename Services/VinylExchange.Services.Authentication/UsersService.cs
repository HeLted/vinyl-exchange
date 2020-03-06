using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylExchange.Data.Models;
using VinylExchange.Models.InputModels.Users;
using VinylExchange.Services.EmaiSender;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Services.Authentication
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<VinylExchangeUser> _userManager;
        private readonly SignInManager<VinylExchangeUser> _signInManager;
        private readonly IEmailSender emailSender;
        private readonly IHttpContextAccessor contextAccessor;

        public UsersService(UserManager<VinylExchangeUser> userManager,
            SignInManager<VinylExchangeUser> signInManager,
            IEmailSender emailSender, IHttpContextAccessor contextAccessor)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this.emailSender = emailSender;
            this.contextAccessor = contextAccessor;
        }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public async Task<IdentityResult> RegisterUser(RegisterUserInputModel inputModel)
        {

            var user = inputModel.To<VinylExchangeUser>();

            user.PasswordHash = this._userManager.PasswordHasher.HashPassword(user, inputModel.Password);


            var identityResult = await this._userManager.CreateAsync(user);

            await this._userManager.AddToRoleAsync(user, "User");

            if (identityResult.Succeeded)
            {
                this.SendConfirmationEmail(user);
            }

            return identityResult;
        }

        public async Task<SignInResult> LoginUser(LoginUserInputModel inputModel)
        {
            ExternalLogins = (await this._signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            var identityResult = await _signInManager.PasswordSignInAsync(inputModel.Username, inputModel.Password, inputModel.RememberMe, lockoutOnFailure: false);

            return identityResult;
        }

        public async Task<IdentityResult> ConfirmUserEmail(ConfirmEmailInputModel inputModel)
        {
            var user = await _userManager.FindByIdAsync(inputModel.UserId.ToString());

            if (user == null)
            {
                throw new NullReferenceException("Confirming Email Failed.User Cannot Be Null!");
            }

            var identityResult = await this._userManager.ConfirmEmailAsync(user, inputModel.EmailConfirmToken);
                     
            return identityResult;

        }

        private async Task SendConfirmationEmail(VinylExchangeUser user)
        {

            var emailConfirmationToken = await this._userManager.GenerateEmailConfirmationTokenAsync(user);

            string emailConfirmationUrl = contextAccessor.HttpContext.Request.Scheme
              + "://"
              + contextAccessor.HttpContext.Request.Host + $"/Authentication/EmailConfirm?cofirmToken={emailConfirmationToken}&userId={user.Id}";
                        
            await this.emailSender.SendEmailAsync(user.Email, "Vinyl Exchange Confirm Email", $@"<h1>Confirm Your Vinyl Exchange Account</h1>.Follow This <a href=""{emailConfirmationUrl}"">Link</a>");
        }

    }
}

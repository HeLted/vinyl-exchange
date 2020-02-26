using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylExchange.Data.Models;
using VinylExchange.Models.InputModels.Users;
using VinylExchange.Models.Utility;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Services.Authentication
{
    public class UserService : IUserService
    {
        private readonly UserManager<VinylExchangeUser> _userManager;
        private readonly SignInManager<VinylExchangeUser>_signInManager;


        public UserService(UserManager<VinylExchangeUser> userManager, SignInManager<VinylExchangeUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public async Task<IdentityResult> RegisterUser(RegisterUserInputModel inputModel)
        {

            var user = inputModel.To<VinylExchangeUser>();

            user.PasswordHash =this._userManager.PasswordHasher.HashPassword(user, inputModel.Password);


            var identityResult = await this._userManager.CreateAsync(user);

            await this._userManager.AddToRoleAsync(user, "User");

            return identityResult;
        }

        public async Task<SignInResult> LoginUser(LoginUserInputModel inputModel)
        {
            ExternalLogins = (await this._signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            var identityResult = await _signInManager.PasswordSignInAsync(inputModel.Username, inputModel.Password, inputModel.RememberMe, lockoutOnFailure: false);

            return identityResult;
        }

    }
}

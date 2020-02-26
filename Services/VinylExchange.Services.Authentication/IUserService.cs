using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VinylExchange.Models.InputModels.Users;

namespace VinylExchange.Services.Authentication
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterUser(RegisterUserInputModel inputModel);

        Task<SignInResult> LoginUser(LoginUserInputModel inputModel);
    }
}

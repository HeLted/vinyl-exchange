using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VinylExchange.Data.Models;
using VinylExchange.Models.InputModels.Users;

namespace VinylExchange.Services.Authentication
{
    public interface IUsersService
    {
        Task<IdentityResult> RegisterUser(RegisterUserInputModel inputModel);

        Task<SignInResult> LoginUser(LoginUserInputModel inputModel);

        Task<IdentityResult> ConfirmUserEmail(ConfirmEmailInputModel inputModel);
    }
}

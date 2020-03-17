namespace VinylExchange.Services.Authentication
{
    #region

    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;

    using VinylExchange.Models.InputModels.Users;

    #endregion

    public interface IUsersService
    {
        Task<IdentityResult> ConfirmUserEmail(ConfirmEmailInputModel inputModel);

        Task<SignInResult> LoginUser(LoginUserInputModel inputModel);

        Task<IdentityResult> RegisterUser(RegisterUserInputModel inputModel);

        Task SendConfirmEmail(Guid userId);
    }
}
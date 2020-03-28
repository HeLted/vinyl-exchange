namespace VinylExchange.Services.Authentication
{
    #region

    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;

    using VinylExchange.Models.InputModels.Users;
    using VinylExchange.Web.Models.InputModels.Users;

    #endregion

    public interface IUsersService
    {
        Task<IdentityResult> ConfirmEmail(ConfirmEmailInputModel inputModel,Guid userId);

        Task<IdentityResult> ChangeEmail(ChangeEmailInputModel inputModel,Guid userId);

        Task<SignInResult> LoginUser(LoginUserInputModel inputModel);

        Task<IdentityResult> RegisterUser(RegisterUserInputModel inputModel);

        Task SendConfirmEmail(Guid userId);

        Task SendChangeEmailEmail(SendChangeEmailEmailInputModel inputModel,Guid userId);

        Task SendChangePasswordEmail(Guid userId);
    }
}
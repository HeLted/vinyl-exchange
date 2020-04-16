namespace VinylExchange.Services.Data.MainServices.Users
{
    #region

    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;

    using VinylExchange.Models.InputModels.Users;

    #endregion

    public interface IUsersService
    {
        Task<IdentityResult> RegisterUser(RegisterUserInputModel inputModel);

        Task<SignInResult> LoginUser(string username, string password, bool rememberMe);

        Task<IdentityResult> ConfirmEmail(string emailConfirmToken, Guid userId);

        Task<IdentityResult> ChangeEmail(string changeEmailToken, string newEmail, Guid userId);

        Task<IdentityResult> ResetPassword(string resetPasswordToken, string email, string newPassword);

        Task SendConfirmEmail(Guid userId);

        Task SendChangeEmailEmail(string newEmail, Guid userId);

        Task SendChangePasswordEmail(Guid userId);

        Task SendResetPasswordEmail(string email);
    }
}
namespace VinylExchange.Services.Authentication
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;

    using VinylExchange.Models.InputModels.Users;

    public interface IUsersService
    {
        Task<IdentityResult> ConfirmUserEmail(ConfirmEmailInputModel inputModel);

        Task<SignInResult> LoginUser(LoginUserInputModel inputModel);

        Task<IdentityResult> RegisterUser(RegisterUserInputModel inputModel);
    }
}
namespace VinylExchange.Services.Data.HelperServices.Users
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    using VinylExchange.Data.Models;
    using VinylExchange.Web.Models.ResourceModels.UsersAvatar;

    public interface IUsersAvatarService
    {
        Task<VinylExchangeUser> ChangeUserAvatar(IFormFile avatar, Guid userId);

        Task<GetUserAvatarResourceModel> GetUserAvatar(Guid userId);
    }
}
namespace VinylExchange.Services.Data.HelperServices.Users
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Threading.Tasks;
    using VinylExchange.Data.Models;
    using VinylExchange.Web.Models.ResourceModels.UsersAvatar;

    public interface IUsersAvatarService
    {
        Task<VinylExchangeUser> ChangeUserAvatar(IFormFile avatar, Guid userId);

        Task<GetUserAvatarResourceModel> GetUserAvatar(Guid userId);
    }
}
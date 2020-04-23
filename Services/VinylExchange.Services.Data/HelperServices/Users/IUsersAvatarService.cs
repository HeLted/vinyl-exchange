namespace VinylExchange.Services.Data.HelperServices.Users
{
    using System;
    using System.Threading.Tasks;
    using VinylExchange.Data.Models;
    using Web.Models.ResourceModels.UsersAvatar;

    public interface IUsersAvatarService
    {
        Task<VinylExchangeUser> ChangeAvatar(byte[] avatar, Guid? userId);

        Task<GetUserAvatarResourceModel> GetUserAvatar(Guid? userId);
    }
}
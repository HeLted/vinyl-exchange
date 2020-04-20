namespace VinylExchange.Services.Data.HelperServices.Users
{
    #region

    using System;
    using System.Threading.Tasks;

    using VinylExchange.Data.Models;
    using VinylExchange.Models.InputModels.Users;
    using VinylExchange.Web.Models.ResourceModels.UsersAvatar;

    #endregion

    public interface IUsersAvatarService
    {
        Task<VinylExchangeUser> ChangeAvatar(byte[] avatar, Guid? userId);

        Task<GetUserAvatarResourceModel> GetUserAvatar(Guid? userId);
    }
}
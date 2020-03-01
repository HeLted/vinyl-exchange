using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VinylExchange.Data.Models;
using VinylExchange.Models.ResourceModels.UsersAvatar;

namespace VinylExchange.Services.Data.HelperServices
{
    public interface IUsersAvatarService
    {
        Task<GetUserAvatarResourceModel> GetUserAvatar(Guid userId);

        Task<VinylExchangeUser> ChangeUserAvatar(IFormFile avatar, Guid userId);
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VinylExchange.Data;
using VinylExchange.Data.Models;
using VinylExchange.Models.ResourceModels.UsersAvatar;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Services.Data.HelperServices.Users
{
    public class UsersAvatarService : IUsersAvatarService
    {
        private readonly VinylExchangeDbContext dbContext;

        public UsersAvatarService(VinylExchangeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<GetUserAvatarResourceModel> GetUserAvatar(Guid userId)
        => await dbContext.Users
                .Where(u => u.Id == userId)
                .To<GetUserAvatarResourceModel>()
                .FirstOrDefaultAsync();


        public async Task<VinylExchangeUser> ChangeUserAvatar(IFormFile avatar, Guid userId)
        {

            var imageByteArray = new byte[1];

            using (MemoryStream ms = new MemoryStream())
            {
                avatar.CopyTo(ms);
                imageByteArray = ms.ToArray();
            }

           var user = await dbContext.Users
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            if(user == null)
            {
                throw new NullReferenceException("User with this Id not found");
            }

            user.Avatar = imageByteArray;

            await this.dbContext.SaveChangesAsync();

            return user;
        }

    }
}

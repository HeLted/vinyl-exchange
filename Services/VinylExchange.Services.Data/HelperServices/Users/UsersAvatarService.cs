namespace VinylExchange.Services.Data.HelperServices.Users
{
    #region

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using VinylExchange.Common.Constants;
    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Data.MainServices.Users;
    using VinylExchange.Services.Mapping;
    using VinylExchange.Web.Models.ResourceModels.UsersAvatar;

    #endregion

    public class UsersAvatarService : IUsersAvatarService
    {
        private readonly VinylExchangeDbContext dbContext;
        
        private readonly IUsersEntityRetriever usersEntityRetriever;

        public UsersAvatarService(VinylExchangeDbContext dbContext,IUsersEntityRetriever usersEntityRetriever)
        {
            this.dbContext = dbContext;
            this.usersEntityRetriever = usersEntityRetriever;
        }

        public async Task<VinylExchangeUser> ChangeAvatar(byte[] avatar, Guid? userId)
        {
            var user = await this.usersEntityRetriever.GetUser(userId);

            if (user == null)
            {
                throw new NullReferenceException(NullReferenceExceptionsConstants.UserNotFound);
            }

            user.Avatar = avatar;

            await this.dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<GetUserAvatarResourceModel> GetUserAvatar(Guid? userId)
        {
            return await QueryableMappingExtensions
                       .To<GetUserAvatarResourceModel>(this.dbContext.Users.Where(u => u.Id == userId))
                       .FirstOrDefaultAsync();
        }
    }
}
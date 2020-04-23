namespace VinylExchange.Services.Data.HelperServices.Users
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Constants;
    using MainServices.Users.Contracts;
    using Mapping;
    using Microsoft.EntityFrameworkCore;
    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using Web.Models.ResourceModels.UsersAvatar;

    public class UsersAvatarService : IUsersAvatarService
    {
        private readonly VinylExchangeDbContext dbContext;

        private readonly IUsersEntityRetriever usersEntityRetriever;

        public UsersAvatarService(VinylExchangeDbContext dbContext, IUsersEntityRetriever usersEntityRetriever)
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
            return await this.dbContext.Users.Where(u => u.Id == userId).To<GetUserAvatarResourceModel>()
                .FirstOrDefaultAsync();
        }
    }
}
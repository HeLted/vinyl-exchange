namespace VinylExchange.Data.Seeding
{
    #region

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using VinylExchange.Common.Constants;
    using VinylExchange.Data.Models;
    using VinylExchange.Data.Seeding.Contracts;

    #endregion

    internal class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(VinylExchangeDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<VinylExchangeUser>>();

            var configuration = serviceProvider.GetRequiredService<IConfiguration>();

            await SeedAdminAsync(dbContext, userManager, configuration["SysAdminUserName"],  configuration["SysAdminPassword"], "sysadmin@vinylexchange.com");
            await SeedUserAsync(dbContext, userManager, "testUserOne", "testUserOnePassword", "testUserOne@gmail.com");
            await SeedUserAsync(dbContext, userManager, "testUserTwo", "testUserTwoPassword", "testUserTwo@gmail.com");
        }

        private static async Task SeedAdminAsync(
            VinylExchangeDbContext dbContext,
            UserManager<VinylExchangeUser> userManager,
            string username,
            string password,
            string email)
        {
            if (!dbContext.Users.Any())
            {
                var admin = await CreateUserAsync(userManager, username, password, email);

                var setAdminRoleResult = await userManager.AddToRoleAsync(admin, Roles.Admin);

                if (!setAdminRoleResult.Succeeded)
                {
                    throw new Exception(
                        string.Join(Environment.NewLine, setAdminRoleResult.Errors.Select(e => e.Description)));
                }
            }
        }

        private static async Task SeedUserAsync(
            VinylExchangeDbContext dbContext,
            UserManager<VinylExchangeUser> userManager,
            string username,
            string password,
            string email)
        {
            if (!dbContext.Users.Where(u => u.UserName != "sysadmin").Any())
            {
                var user = await CreateUserAsync(userManager, username, password, email);

                var setAdminRoleResult = await userManager.AddToRoleAsync(user, Roles.User);

                if (!setAdminRoleResult.Succeeded)
                {
                    throw new Exception(
                        string.Join(Environment.NewLine, setAdminRoleResult.Errors.Select(e => e.Description)));
                }
            }
        }

        private static async Task<VinylExchangeUser> CreateUserAsync(
            UserManager<VinylExchangeUser> userManager,
            string username,
            string password,
            string email)
        {
            var user = new VinylExchangeUser { UserName = username, Email = email };

            user.PasswordHash = userManager.PasswordHasher.HashPassword(user, password);

            var createUserResult = await userManager.CreateAsync(user);

            if (!createUserResult.Succeeded)
            {
                throw new Exception(
                    string.Join(Environment.NewLine, createUserResult.Errors.Select(e => e.Description)));
            }

            return user;
        }
    }
}
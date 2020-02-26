using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylExchange.Data.Models;

namespace VinylExchange.Roles
{
    public class UserSeeder
    {
        private readonly UserManager<VinylExchangeUser> userManager;
       
        public UserSeeder(UserManager<VinylExchangeUser> userManager)
        {
            this.userManager = userManager;
         
        }

        public async Task SeedAdmin()
        {
            if(await userManager.FindByNameAsync("sysadmin") == null)
            {
                var admin = new VinylExchangeUser()
                {
                    UserName = "sysadmin",
                    Email = "sysadmin@vinylexchange.com",

                };

                admin.PasswordHash = userManager.PasswordHasher.HashPassword(admin, "aphextwindrukqs22");

                await userManager.CreateAsync(admin);
            }
                       
        }
    }
}

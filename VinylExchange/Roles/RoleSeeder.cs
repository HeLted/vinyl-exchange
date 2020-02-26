using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylExchange.Data.Models;

namespace VinylExchange.Roles
{
    public  class RoleSeeder
    {
        private readonly UserManager<VinylExchangeUser> userManager;
        private readonly RoleManager<VinylExchangeRole> roleManager;

        public RoleSeeder(UserManager<VinylExchangeUser> userManager, RoleManager<VinylExchangeRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task SeedRoles()
        {
     
            bool adminRoleExists = await this.roleManager.RoleExistsAsync("Admin");
            if (!adminRoleExists)
            {               
                await this.roleManager.CreateAsync(new VinylExchangeRole("Admin"));
            }
            
            bool userRoleExists = await this.roleManager.RoleExistsAsync("User");

            if (!userRoleExists)
            {                
                await this.roleManager.CreateAsync(new VinylExchangeRole("User"));
            }


            VinylExchangeUser user = await userManager.FindByNameAsync("sysadmin");
            if (!await userManager.IsInRoleAsync(user, "Admin"))
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}

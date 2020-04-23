namespace VinylExchange.Web.Infrastructure.IdentityServer.Profile
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Data.Models;
    using IdentityModel;
    using IdentityServer4.Models;
    using IdentityServer4.Services;
    using Microsoft.AspNetCore.Identity;

    public class ProfileService : IProfileService
    {
        private readonly UserManager<VinylExchangeUser> userManager;

        public ProfileService(UserManager<VinylExchangeUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await this.userManager.GetUserAsync(context.Subject);
            var roles = await this.userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Name, user.UserName),
                new Claim(JwtClaimTypes.EmailVerified, user.EmailConfirmed.ToString().ToLower()),
                new Claim(JwtClaimTypes.Role, roles.Any() ? roles.First() : "User"),
                new Claim(JwtClaimTypes.Role, roles.Any() ? roles.First() : "Admin")
            };

            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await this.userManager.GetUserAsync(context.Subject);
            context.IsActive = user != null && user.LockoutEnabled;
        }
    }
}
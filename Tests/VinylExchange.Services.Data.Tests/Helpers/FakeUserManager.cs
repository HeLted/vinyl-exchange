using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VinylExchange.Data.Models;

namespace VinylExchange.Services.Data.Tests.Helpers
{
    public class FakeUserManager : UserManager<VinylExchangeUser>
    {
        public FakeUserManager()
            : base(new Mock<IUserStore<VinylExchangeUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<VinylExchangeUser>>().Object,
                new IUserValidator<VinylExchangeUser>[0],
                new IPasswordValidator<VinylExchangeUser>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<VinylExchangeUser>>>().Object)
        { }

        public override Task<IdentityResult> CreateAsync(VinylExchangeUser user, string password)
        {
            return Task.FromResult(IdentityResult.Success);
        }

        public override Task<IdentityResult> AddToRoleAsync(VinylExchangeUser user, string role)
        {
            return Task.FromResult(IdentityResult.Success);
        }

        public override Task<string> GenerateEmailConfirmationTokenAsync(VinylExchangeUser user)
        {
            return Task.FromResult(Guid.NewGuid().ToString());
        }

    }
}

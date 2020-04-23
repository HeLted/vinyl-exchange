using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using VinylExchange.Data.Models;

namespace VinylExchange.Services.Data.Tests.Helpers
{
    public class FakeSignInManager : SignInManager<VinylExchangeUser>
    {
        public FakeSignInManager(Mock<IPasswordHasher<VinylExchangeUser>> passwordHasherMock)
            : base(new FakeUserManager(passwordHasherMock),
                new Mock<IHttpContextAccessor>().Object,
                new Mock<IUserClaimsPrincipalFactory<VinylExchangeUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<VinylExchangeUser>>>().Object,
                new Mock<IAuthenticationSchemeProvider>().Object,null)
        { }        
    }
}

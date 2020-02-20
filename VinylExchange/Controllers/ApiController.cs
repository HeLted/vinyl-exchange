using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace VinylExchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {     
        protected Guid GetUserId(ClaimsPrincipal user)
        {
            return  Guid.Parse(user.FindFirst("sub").Value);
        }

    }
}

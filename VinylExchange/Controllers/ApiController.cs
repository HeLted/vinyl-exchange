using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace VinylExchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {     
        protected string GetUserId(ClaimsPrincipal user)
        {
            return user.FindFirst("sub").Value;
        }

    }
}

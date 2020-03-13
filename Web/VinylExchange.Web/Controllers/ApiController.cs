namespace VinylExchange.Web.Controllers
{
    using System;
    using System.Net;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
 

    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    public class ApiController : ControllerBase
    {
        protected Guid GetUserId(ClaimsPrincipal user)
        {
            return Guid.Parse(user.FindFirst("sub").Value);
        }

        protected ActionResult<T> Created<T>(T value)
        {
            return base.StatusCode((int)HttpStatusCode.Created, value);
        }


    }
}
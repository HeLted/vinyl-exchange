namespace VinylExchange.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Net;
    using System.Security.Claims;


    [Route("api/[controller]")]
    [ApiController]   
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
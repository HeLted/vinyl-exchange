namespace VinylExchange.Web.Controllers
{
    using System;
    using System.Net;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        protected ActionResult<T> Created<T>(T value)
        {
            return this.StatusCode((int) HttpStatusCode.Created, value);
        }

        protected Guid GetUserId(ClaimsPrincipal user)
        {
            return Guid.Parse(user.FindFirst("sub").Value);
        }
    }
}
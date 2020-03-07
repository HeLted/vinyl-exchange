namespace VinylExchange.Controllers
{
    using System;
    using System.Net;
    using System.Security.Claims;

    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        protected Guid GetUserId(ClaimsPrincipal user)
        {
            return Guid.Parse(user.FindFirst("sub").Value);
        }

        protected IActionResult StatusCode(HttpStatusCode statusCode, object value)
        {
            return base.StatusCode((int)statusCode, value);
        }
    }
}
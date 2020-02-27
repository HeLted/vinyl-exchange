using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylExchange.Data.Models;
using VinylExchange.Models.InputModels.Users;
using VinylExchange.Services.Authentication;
using VinylExchange.Services.Logging;

namespace VinylExchange.Controllers
{
    [Route("authentication/[controller]")]
    public class UsersController : ApiController
    {
        private readonly IUserService userService;
        private readonly ILoggerService loggerService;

        public UsersController(IUserService userService,ILoggerService loggerService)
        {
            this.userService = userService;
            this.loggerService = loggerService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterUserInputModel inputModel)
        {
          
            try
            {
                var registerUserIdentityResult = await this.userService.RegisterUser(inputModel);

                if (registerUserIdentityResult.Succeeded)
                {
                    return Ok();
                }
                else
                {                  
                    return BadRequest(registerUserIdentityResult.Errors);
                }
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }

        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginUserInputModel inputModel)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            try
            {
                var registerUserIdentityResult = await this.userService.LoginUser(inputModel);

                if (registerUserIdentityResult.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }

        }

        [HttpPost]
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailInputModel inputModel)
        {
      
            try
            {
                var confirmEmailIdentityResult = await this.userService.ConfirmUserEmail(inputModel);

                if (confirmEmailIdentityResult.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(confirmEmailIdentityResult.Errors);
                }
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }

        }

    }
}

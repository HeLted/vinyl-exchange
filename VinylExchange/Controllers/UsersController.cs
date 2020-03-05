using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VinylExchange.Data.Models;
using VinylExchange.Models.InputModels.Users;
using VinylExchange.Services.Authentication;
using VinylExchange.Services.Data.HelperServices;
using VinylExchange.Services.Data.HelperServices.Users;
using VinylExchange.Services.Logging;

namespace VinylExchange.Controllers
{    
    public class UsersController : ApiController
    {
        private readonly IUsersService userService;
        private readonly IUsersAvatarService usersAvatarService;
        private readonly ILoggerService loggerService;

        public UsersController(IUsersService userService,
            IUsersAvatarService usersAvatarService,
            ILoggerService loggerService)
        {
            this.userService = userService;
            this.usersAvatarService = usersAvatarService;
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

        [HttpPut]
        [Authorize]
        [Route("ChangeUserAvatar")]
        public async Task<IActionResult> ChangeUserAvatar(IFormFile avatar)
        {
            try
            {
                await this.usersAvatarService.ChangeUserAvatar(avatar, this.GetUserId(this.User));

                return NoContent();
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }

        }

        [HttpGet]
        [Authorize]
        [Route("GetUserAvatar/{id}")]
        public async Task<IActionResult> GetUserAvatar(Guid id)
        {

            var userAvatar = await this.usersAvatarService.GetUserAvatar(id);

            if (userAvatar == null)
            {
                return NotFound();
            }

            return Ok(userAvatar);

        }


        [HttpGet]
        [Authorize]
        [Route("GetCurrentUserAvatar")]
        public async Task<IActionResult> GetUserAvatar()
        {

            var userAvatar = await this.usersAvatarService.GetUserAvatar(this.GetUserId(this.User));

            if (userAvatar == null)
            {
                return NotFound();
            }

            return Ok(userAvatar);

        }

    }
}

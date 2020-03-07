namespace VinylExchange.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using VinylExchange.Models.InputModels.Users;
    using VinylExchange.Services.Authentication;
    using VinylExchange.Services.Data.HelperServices.Users;
    using VinylExchange.Services.Logging;
    using VinylExchange.Web.Models.ResourceModels.UsersAvatar;

    using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

    public class UsersController : ApiController
    {
        private readonly ILoggerService loggerService;

        private readonly IUsersAvatarService usersAvatarService;

        private readonly IUsersService userService;

        public UsersController(
            IUsersService userService,
            IUsersAvatarService usersAvatarService,
            ILoggerService loggerService)
        {
            this.userService = userService;
            this.usersAvatarService = usersAvatarService;
            this.loggerService = loggerService;
        }

        [HttpPut]
        [Authorize]
        [Route("ChangeUserAvatar")]
        public async Task<IActionResult> ChangeUserAvatar(IFormFile avatar)
        {
            try
            {
                await this.usersAvatarService.ChangeUserAvatar(avatar, this.GetUserId(this.User));

                return this.NoContent();
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpPost]
        [Authorize]
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailInputModel inputModel)
        {
            try
            {
                IdentityResult confirmEmailIdentityResult = await this.userService.ConfirmUserEmail(inputModel);

                if (confirmEmailIdentityResult.Succeeded)
                {
                    return this.Ok();
                }
                else
                {
                    return this.BadRequest(confirmEmailIdentityResult.Errors);
                }
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpGet]
        [Authorize]
        [Route("GetUserAvatar/{id}")]
        public async Task<IActionResult> GetUserAvatar(Guid id)
        {
            GetUserAvatarResourceModel userAvatar = await this.usersAvatarService.GetUserAvatar(id);

            if (userAvatar == null)
            {
                return this.NotFound();
            }

            return this.Ok(userAvatar);
        }

        [HttpGet]
        [Authorize]
        [Route("GetCurrentUserAvatar")]
        public async Task<IActionResult> GetUserAvatar()
        {
            GetUserAvatarResourceModel userAvatar =
                await this.usersAvatarService.GetUserAvatar(this.GetUserId(this.User));

            if (userAvatar == null)
            {
                return this.NotFound();
            }

            return this.Ok(userAvatar);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginUserInputModel inputModel)
        {
            await this.HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            try
            {
                SignInResult registerUserIdentityResult = await this.userService.LoginUser(inputModel);

                if (registerUserIdentityResult.Succeeded)
                {
                    return this.Ok();
                }
                else
                {
                    return this.Unauthorized();
                }
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterUserInputModel inputModel)
        {
            try
            {
                IdentityResult registerUserIdentityResult = await this.userService.RegisterUser(inputModel);

                if (registerUserIdentityResult.Succeeded)
                {
                    return this.Ok();
                }
                else
                {
                    return this.BadRequest(registerUserIdentityResult.Errors);
                }
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }
    }
}
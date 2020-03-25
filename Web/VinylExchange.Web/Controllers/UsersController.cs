﻿namespace VinylExchange.Web.Controllers
{
    #region

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

    #endregion

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
        public async Task<ActionResult> ChangeUserAvatar([FromForm]ChangeAvatarInputModel inputModel)
        {
            try
            {
                await this.usersAvatarService.ChangeUserAvatar(inputModel, this.GetUserId(this.User));

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
        public async Task<ActionResult> ConfirmEmail(ConfirmEmailInputModel inputModel)
        {
            try
            {
                var confirmEmailIdentityResult = await this.userService.ConfirmUserEmail(inputModel);

                if (confirmEmailIdentityResult.Succeeded) return this.Ok();

                return this.BadRequest(confirmEmailIdentityResult.Errors);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpGet]
        [Route("GetUserAvatar/{id}")]
        public async Task<ActionResult<GetUserAvatarResourceModel>> GetUserAvatar(Guid id)
        {
            var userAvatarModel = await this.usersAvatarService.GetUserAvatar(id);

            if (userAvatarModel == null) return this.NotFound();

            return userAvatarModel;
        }

        [HttpGet]
        [Authorize]
        [Route("GetCurrentUserAvatar")]
        public async Task<ActionResult<GetUserAvatarResourceModel>> GetUserAvatar()
        {
            var userAvatarModel = await this.usersAvatarService.GetUserAvatar(this.GetUserId(this.User));

            if (userAvatarModel == null) return this.NotFound();

            return userAvatarModel;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(LoginUserInputModel inputModel)
        {
            await this.HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            try
            {
                var registerUserIdentityResult = await this.userService.LoginUser(inputModel);

                if (registerUserIdentityResult.Succeeded) return this.Ok();

                return this.Unauthorized();
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register(RegisterUserInputModel inputModel)
        {
            try
            {
                var registerUserIdentityResult = await this.userService.RegisterUser(inputModel);

                if (registerUserIdentityResult.Succeeded) return this.Ok();

                return this.BadRequest(registerUserIdentityResult.Errors);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpPost]
        [Authorize]
        [Route("SendConfirmEmail")]
        public async Task<ActionResult> SendConfirmEmail()
        {
            try
            {
                await this.userService.SendConfirmEmail(this.GetUserId(this.User));

                return this.Ok();
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }
    }
}
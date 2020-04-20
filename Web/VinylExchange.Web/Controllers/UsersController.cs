namespace VinylExchange.Web.Controllers
{
    #region

    using System;
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using VinylExchange.Models.InputModels.Users;
    using VinylExchange.Services.Data.HelperServices.Users;
    using VinylExchange.Services.Data.MainServices.Users;
    using VinylExchange.Services.Logging;
    using VinylExchange.Web.Models.InputModels.Users;
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
        [Route("ChangeAvatar")]
        public async Task<ActionResult> ChangeAvatar([FromForm] ChangeAvatarInputModel inputModel)
        {
            try
            {
                var imageByteArray = new byte[1];

                using (var ms = new MemoryStream())
                {
                    inputModel.Avatar.CopyTo(ms);
                    imageByteArray = ms.ToArray();
                }

                await this.usersAvatarService.ChangeAvatar(imageByteArray, this.GetUserId(this.User));

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
                var confirmEmailIdentityResult = await this.userService.ConfirmEmail(
                                                     inputModel.EmailConfirmToken,
                                                     this.GetUserId(this.User));

                if (confirmEmailIdentityResult.Succeeded)
                {
                    return this.Ok();
                }

                return this.BadRequest(confirmEmailIdentityResult.Errors);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpPost]
        [Authorize]
        [Route("ChangeEmail")]
        public async Task<ActionResult> ChangeEmail(ChangeEmailInputModel inputModel)
        {
            try
            {
                var confirmEmailIdentityResult = await this.userService.ChangeEmail(
                                                     inputModel.ChangeEmailToken,
                                                     inputModel.NewEmail,
                                                     this.GetUserId(this.User));

                if (confirmEmailIdentityResult.Succeeded)
                {
                    return this.Ok();
                }

                return this.BadRequest(confirmEmailIdentityResult.Errors);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<ActionResult> ResetPassword(ResetPasswordInputModel inputModel)
        {
            try
            {
                var confirmEmailIdentityResult = await this.userService.ResetPassword(
                                                     inputModel.ResetPasswordToken,
                                                     inputModel.Email,
                                                     inputModel.NewPassword);

                if (confirmEmailIdentityResult.Succeeded)
                {
                    return this.Ok();
                }

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

            if (userAvatarModel == null)
            {
                return this.NotFound();
            }

            return userAvatarModel;
        }

        [HttpGet]
        [Authorize]
        [Route("GetCurrentUserAvatar")]
        public async Task<ActionResult<GetUserAvatarResourceModel>> GetUserAvatar()
        {
            var userAvatarModel = await this.usersAvatarService.GetUserAvatar(this.GetUserId(this.User));

            if (userAvatarModel == null)
            {
                return this.NotFound();
            }

            return userAvatarModel;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(LoginUserInputModel inputModel)
        {
            await this.HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            try
            {
                var registerUserIdentityResult = await this.userService.LoginUser(
                                                     inputModel.Username,
                                                     inputModel.Password,
                                                     inputModel.RememberMe);

                if (registerUserIdentityResult.Succeeded)
                {
                    return this.Ok();
                }

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

                if (registerUserIdentityResult.Succeeded)
                {
                    return this.Ok();
                }

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

        [HttpPost]
        [Authorize]
        [Route("SendChangeEmailEmail")]
        public async Task<ActionResult> SendChangeEmailEmail(SendChangeEmailEmailInputModel inputModel)
        {
            try
            {
                await this.userService.SendChangeEmailEmail(inputModel.NewEmail, this.GetUserId(this.User));

                return this.Ok();
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpPost]
        [Authorize]
        [Route("SendChangePasswordEmail")]
        public async Task<ActionResult> SendChangePasswordEmail()
        {
            try
            {
                await this.userService.SendChangePasswordEmail(this.GetUserId(this.User));

                return this.Ok();
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpPost]
        [Route("SendResetPasswordEmail")]
        public async Task<ActionResult> SendResetPasswordEmail(SendResetPasswordEmailInputModel inputModel)
        {
            try
            {
                await this.userService.SendResetPasswordEmail(inputModel.Email);

                return this.Ok();
            }
            catch (NullReferenceException ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }
    }
}
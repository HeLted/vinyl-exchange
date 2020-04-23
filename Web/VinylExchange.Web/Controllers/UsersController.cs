namespace VinylExchange.Web.Controllers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.InputModels.Users;
    using Models.ResourceModels.UsersAvatar;
    using Services.Data.HelperServices.Users;
    using Services.Data.MainServices.Users.Contracts;
    using Services.Logging;
    using VinylExchange.Models.InputModels.Users;

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

                await usersAvatarService.ChangeAvatar(imageByteArray, GetUserId(User));

                return NoContent();
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize]
        [Route("ConfirmEmail")]
        public async Task<ActionResult> ConfirmEmail(ConfirmEmailInputModel inputModel)
        {
            try
            {
                var confirmEmailIdentityResult = await userService.ConfirmEmail(
                    inputModel.EmailConfirmToken,
                    GetUserId(User));

                if (confirmEmailIdentityResult.Succeeded)
                {
                    return Ok();
                }

                return BadRequest(confirmEmailIdentityResult.Errors);
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize]
        [Route("ChangeEmail")]
        public async Task<ActionResult> ChangeEmail(ChangeEmailInputModel inputModel)
        {
            try
            {
                var confirmEmailIdentityResult = await userService.ChangeEmail(
                    inputModel.ChangeEmailToken,
                    inputModel.NewEmail,
                    GetUserId(User));

                if (confirmEmailIdentityResult.Succeeded)
                {
                    return Ok();
                }

                return BadRequest(confirmEmailIdentityResult.Errors);
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<ActionResult> ResetPassword(ResetPasswordInputModel inputModel)
        {
            try
            {
                var confirmEmailIdentityResult = await userService.ResetPassword(
                    inputModel.ResetPasswordToken,
                    inputModel.Email,
                    inputModel.NewPassword);

                if (confirmEmailIdentityResult.Succeeded)
                {
                    return Ok();
                }

                return BadRequest(confirmEmailIdentityResult.Errors);
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetUserAvatar/{id}")]
        public async Task<ActionResult<GetUserAvatarResourceModel>> GetUserAvatar(Guid id)
        {
            var userAvatarModel = await usersAvatarService.GetUserAvatar(id);

            if (userAvatarModel == null)
            {
                return NotFound();
            }

            return userAvatarModel;
        }

        [HttpGet]
        [Authorize]
        [Route("GetCurrentUserAvatar")]
        public async Task<ActionResult<GetUserAvatarResourceModel>> GetUserAvatar()
        {
            var userAvatarModel = await usersAvatarService.GetUserAvatar(GetUserId(User));

            if (userAvatarModel == null)
            {
                return NotFound();
            }

            return userAvatarModel;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(LoginUserInputModel inputModel)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            try
            {
                var registerUserIdentityResult = await userService.LoginUser(
                    inputModel.Username,
                    inputModel.Password,
                    inputModel.RememberMe);

                if (registerUserIdentityResult.Succeeded)
                {
                    return Ok();
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register(RegisterUserInputModel inputModel)
        {
            try
            {
                var registerUserIdentityResult = await userService.RegisterUser(
                    inputModel.Username,
                    inputModel.Email,
                    inputModel.Password);

                if (registerUserIdentityResult.Succeeded)
                {
                    return Ok();
                }

                return BadRequest(registerUserIdentityResult.Errors);
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize]
        [Route("SendConfirmEmail")]
        public async Task<ActionResult> SendConfirmEmail()
        {
            try
            {
                await userService.SendConfirmEmail(GetUserId(User));

                return Ok();
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize]
        [Route("SendChangeEmailEmail")]
        public async Task<ActionResult> SendChangeEmailEmail(SendChangeEmailEmailInputModel inputModel)
        {
            try
            {
                await userService.SendChangeEmailEmail(inputModel.NewEmail, GetUserId(User));

                return Ok();
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize]
        [Route("SendChangePasswordEmail")]
        public async Task<ActionResult> SendChangePasswordEmail()
        {
            try
            {
                await userService.SendChangePasswordEmail(GetUserId(User));

                return Ok();
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("SendResetPasswordEmail")]
        public async Task<ActionResult> SendResetPasswordEmail(SendResetPasswordEmailInputModel inputModel)
        {
            try
            {
                await userService.SendResetPasswordEmail(inputModel.Email);

                return Ok();
            }
            catch (NullReferenceException ex)
            {
                loggerService.LogException(ex);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }
    }
}
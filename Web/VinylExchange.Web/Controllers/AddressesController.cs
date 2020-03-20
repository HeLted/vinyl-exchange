namespace VinylExchange.Web.Controllers
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using VinylExchange.Services.Data.MainServices.Addresses;
    using VinylExchange.Services.Logging;
    using VinylExchange.Web.Models.InputModels.Addresses;
    using VinylExchange.Web.Models.ResourceModels.Addresses;
    using VinylExchange.Web.Models.Utility;

    #endregion

    [Authorize]
    public class AddressesController : ApiController
    {
        private readonly IAddressesService addressesService;

        private readonly ILoggerService loggerService;

        public AddressesController(IAddressesService addressesService, ILoggerService loggerService)
        {
            this.addressesService = addressesService;
            this.loggerService = loggerService;
        }

        [HttpPost]
        public async Task<ActionResult<CreateAddressResourceModel>> Create(CreateAddressInputModel inputModel)
        {
            try
            {
                return this.Created(
                    await this.addressesService.CreateAddress<CreateAddressResourceModel>(
                        inputModel,
                        this.GetUserId(this.User)));
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpGet]
        [Route("GetUserAddresses")]
        public async Task<ActionResult<IEnumerable<GetUserAddressesResourceModel>>> GetUserAddresses()
        {
            try
            {
                return await this.addressesService.GetUserAddresses<GetUserAddressesResourceModel>(
                           this.GetUserId(this.User));
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<RemoveAddressResourceModel>> Remove(Guid id)
        {
            try
            {
                var addressInfoModel = await this.addressesService.GetAddressInfo<GetAddressInfoUtilityModel>(id);

                if (addressInfoModel == null) return this.NotFound();

                if (addressInfoModel.UserId != this.GetUserId(this.User)) return this.Forbid();

                return await this.addressesService.RemoveAddress<RemoveAddressResourceModel>(addressInfoModel.Id);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }
    }
}
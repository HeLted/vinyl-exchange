namespace VinylExchange.Web.Controllers
{
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
                CreateAddressResourceModel addressModel = await this.addressesService.AddAddress<CreateAddressResourceModel>(inputModel, this.GetUserId(this.User));

                return this.Created(addressModel);
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
                List<GetUserAddressesResourceModel> addresses =
                    await this.addressesService.GetUserAddresses<GetUserAddressesResourceModel>(this.GetUserId(this.User));

                return addresses;
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
                GetAddressInfoUtilityModel addressInfoModel = await this.addressesService.GetAddressInfo<GetAddressInfoUtilityModel>(id);

                if (addressInfoModel == null)
                {
                    return this.NotFound();
                }

                if (addressInfoModel.UserId != this.GetUserId(this.User))
                {
                    return this.Unauthorized();
                }

                RemoveAddressResourceModel removedAddressResourceModel =
                    await this.addressesService.RemoveAddress<RemoveAddressResourceModel>(addressInfoModel.Id);

                return removedAddressResourceModel;
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }
    }
}
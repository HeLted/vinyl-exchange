namespace VinylExchange.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.InputModels.Addresses;
    using Models.ResourceModels.Addresses;
    using Models.Utility.Addresses;
    using Services.Data.MainServices.Addresses.Contracts;
    using Services.Logging;

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
                var resourceModel = await this.addressesService.CreateAddress<CreateAddressResourceModel>(
                    inputModel.Country,
                    inputModel.Town,
                    inputModel.PostalCode,
                    inputModel.FullAddress, this.GetUserId(this.User));

                return this.Created(resourceModel);
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
                var addressInfoModel = await this.addressesService.GetAddress<GetAddressInfoUtilityModel>(id);

                if (addressInfoModel == null)
                {
                    return this.NotFound();
                }

                if (addressInfoModel.UserId != this.GetUserId(this.User))
                {
                    return this.Forbid();
                }

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
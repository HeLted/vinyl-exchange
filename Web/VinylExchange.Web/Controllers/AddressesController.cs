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
                var resourceModel = await addressesService.CreateAddress<CreateAddressResourceModel>(
                    inputModel.Country,
                    inputModel.Town,
                    inputModel.PostalCode,
                    inputModel.FullAddress,
                    GetUserId(User));

                return Created(resourceModel);
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetUserAddresses")]
        public async Task<ActionResult<IEnumerable<GetUserAddressesResourceModel>>> GetUserAddresses()
        {
            try
            {
                return await addressesService.GetUserAddresses<GetUserAddressesResourceModel>(
                    GetUserId(User));
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<RemoveAddressResourceModel>> Remove(Guid id)
        {
            try
            {
                var addressInfoModel = await addressesService.GetAddress<GetAddressInfoUtilityModel>(id);

                if (addressInfoModel == null)
                {
                    return NotFound();
                }

                if (addressInfoModel.UserId != GetUserId(User))
                {
                    return Forbid();
                }

                return await addressesService.RemoveAddress<RemoveAddressResourceModel>(addressInfoModel.Id);
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }
    }
}
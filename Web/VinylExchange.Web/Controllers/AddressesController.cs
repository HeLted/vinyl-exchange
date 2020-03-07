namespace VinylExchange.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using VinylExchange.Data.Models;
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
        public async Task<IActionResult> Add(AddAdressInputModel inputModel)
        {
            try
            {
                Address address = await this.addressesService.AddAddress(inputModel, this.GetUserId(this.User));

                return this.StatusCode(HttpStatusCode.Created, address.Id);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpGet]
        [Route("GetUserAddresses")]
        public async Task<IActionResult> GetUserAddresses()
        {
            try
            {
                IEnumerable<GetUserAddressesResourceModel> addresses =
                    await this.addressesService.GetUserAddresses(this.GetUserId(this.User));

                return this.Ok(addresses);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            try
            {
                GetAddressInfoUtilityModel addressInfoModel = await this.addressesService.GetAddressInfo(id);

                if (addressInfoModel == null)
                {
                    return this.NotFound();
                }

                if (addressInfoModel.UserId != this.GetUserId(this.User))
                {
                    return this.Unauthorized();
                }

                RemoveAddressResourceModel removedAddressResourceModel =
                    await this.addressesService.RemoveAddress(addressInfoModel.Id);

                return this.Ok(removedAddressResourceModel);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }
    }
}
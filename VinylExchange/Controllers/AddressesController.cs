using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VinylExchange.Models.InputModels.Addresses;
using VinylExchange.Services.Data.MainServices.Addresses;
using VinylExchange.Services.Logging;

namespace VinylExchange.Controllers
{
    [Authorize]
    public class AddressesController : ApiController
    {
        private readonly IAddressesService addressesService;
        private readonly ILoggerService loggerService;

        public AddressesController(IAddressesService addressesService,ILoggerService loggerService)
        {
            this.addressesService = addressesService;
            this.loggerService = loggerService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(AddAdressInputModel inputModel)
        {

            try
            {
                var address = await this.addressesService.AddAddress(inputModel, this.GetUserId(this.User));

                return CreatedAtRoute("Default", new { id = address.Id });
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
            
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            try
            {
                var addressInfoModel = await this.addressesService.GetAddressInfo(id);

                if (addressInfoModel == null)
                {
                    return NotFound();
                }

                if (addressInfoModel.UserId != this.GetUserId(this.User))
                {
                    return Unauthorized();
                }

                var addressRemovedModel = await this.addressesService.RemoveAddress(addressInfoModel.Id);

                return Ok(addressRemovedModel);
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }


        }

        [HttpGet]
        [Route("GetUserAddresses")]
        public async Task<IActionResult> GetUserAddresses()
        {
            try
            {
                var addresses = await this.addressesService.GetUserAddresses(this.GetUserId(this.User));

                return Ok(addresses);
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
            
        }
               
    }
}

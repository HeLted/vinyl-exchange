using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VinylExchange.Data.Common.Enumerations;
using VinylExchange.Models.InputModels.Sales;
using VinylExchange.Services.Data.MainServices.Sales;
using VinylExchange.Services.Logging;

namespace VinylExchange.Controllers
{
    [Authorize]
    public class SalesController : ApiController
    {
        private readonly ISalesService salesService;
        private readonly ILoggerService loggerService;

        public SalesController(ISalesService salesService,ILoggerService loggerService)
        {
            this.salesService = salesService;
            this.loggerService = loggerService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
           
            try
            {
                var sale = await this.salesService.GetSale(id);
                               
                if (sale == null)
                {
                    return NotFound();
                }

                
                var currentUserId = this.GetUserId(this.User);

                if((sale.BuyerId != currentUserId && sale.SellerId != currentUserId) 
                    && sale.Status != Status.Open)
                {
                    return Unauthorized();
                }

                return Ok(sale);
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }

        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSaleInputModel inputModel)
        {
            try
            {                
                var sale = await salesService.CreateSale(inputModel,this.GetUserId(this.User));

                return CreatedAtRoute("Default", new { id = sale.Id });
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
            
        }

        [HttpGet]
        [Route("GetAllSalesForRelease/{id}")]
        public async Task<IActionResult> GetAllSalesForRelease(Guid id)
        {
            try
            {
                var sales = await this.salesService.GetAllSalesForRelease(id);

                return Ok(sales);
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }

    }
}

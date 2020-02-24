using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylExchange.Services.Data.MainServices.Sales;
using VinylExchange.Services.Logging;

namespace VinylExchange.Controllers
{
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

                return Ok(sale);
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }

        }

    }
}

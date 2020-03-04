using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VinylExchange.Data.Common.Enumerations;
using VinylExchange.Models.InputModels.Sales;
using VinylExchange.Models.ResourceModels.Sales;
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

        [HttpPut]
        [Route("PlaceOrder")]
        public async Task<IActionResult> PlaceOrder(PlaceOrderInputModel inputModel)
        {
            try
            {
                var saleModel = await this.salesService.GetSaleInfo(inputModel.SaleId);

                if (saleModel == null)
                {
                    return BadRequest();
                }

                var currentUserId = this.GetUserId(this.User);

                if(saleModel.SellerId == currentUserId 
                    || saleModel.BuyerId == currentUserId 
                    || saleModel.Status != Status.Open)
                {
                    return Unauthorized();
                }

                await this.salesService.PlaceOrder(inputModel,currentUserId);
                
                return NoContent();
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }

        }

        [HttpPut]
        [Route("SetShippingPrice")]
        public async Task<IActionResult> SetShippingPrice(SetShippingPriceInputModel inputModel)
        {
            try
            {
                var saleModel = await this.salesService.GetSaleInfo(inputModel.SaleId);

                if (saleModel== null)
                {
                    return BadRequest();
                }

                var currentUserId = this.GetUserId(this.User);

                if (saleModel.SellerId == currentUserId  && saleModel.Status == Status.ShippingNegotiation)
                {
                    await this.salesService.SetShippingPrice(inputModel);

                    return NoContent();
                }
                else
                {
                    return Unauthorized();
                }

           
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }

        }

        [HttpPut]
        [Route("CompletePayment")]
        public async Task<IActionResult> CompletePayment(CompletePaymentInputModel inputModel)
        {
            try
            {
                var saleModel = await this.salesService.GetSaleInfo(inputModel.SaleId);

                if (saleModel == null)
                {
                    return BadRequest();
                }

                var currentUserId = this.GetUserId(this.User);

                if (saleModel.BuyerId == currentUserId && saleModel.Status == Status.PaymentPending)
                {
                    await this.salesService.CompletePayment(inputModel);

                    return NoContent();
                }
                else
                {
                    return Unauthorized();
                }


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

        [HttpGet]
        [Route("GetUserPurchases")]
        public async Task<IActionResult> GetUserPurchases()
        {
            try
            {
                var purchases = await this.salesService.GetUserPurchases(this.GetUserId(this.User));

                return Ok(purchases);
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }

        }

        [HttpGet]
        [Route("GetUserSales")]
        public async Task<IActionResult> GetUserSales()
        {
            try
            {
                var sales = await this.salesService.GetUserSales(this.GetUserId(this.User));

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

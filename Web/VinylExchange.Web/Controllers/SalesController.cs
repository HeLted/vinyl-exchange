namespace VinylExchange.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using VinylExchange.Common.Enumerations;
    using VinylExchange.Data.Common.Enumerations;
    using VinylExchange.Services.Data.HelperServices.Sales.SaleLogs;
    using VinylExchange.Services.Data.MainServices.Sales;
    using VinylExchange.Services.Logging;
    using VinylExchange.Web.Hubs.SaleLog;
    using VinylExchange.Web.Models.InputModels.Sales;
    using VinylExchange.Web.Models.ResourceModels.Sales;
    using VinylExchange.Web.Models.Utility;
    using static VinylExchange.Common.Constants.RolesConstants;

    [Authorize(Roles= AdminUser)]
    public class SalesController : ApiController
    {
        private readonly ILoggerService loggerService;

        private readonly IHubContext<SaleLogsHub, ISaleLogsClient> saleLogHubContext;

        private readonly ISaleLogsService saleLogsService;

        private readonly ISalesService salesService;               

        public SalesController(
            ISalesService salesService,
            ILoggerService loggerService,
            IHubContext<SaleLogsHub, ISaleLogsClient> saleLogHubContext,
            ISaleLogsService saleLogsService)
        {
            this.salesService = salesService;
            this.loggerService = loggerService;
            this.saleLogHubContext = saleLogHubContext;           
            this.saleLogsService = saleLogsService;
           
        }

        [HttpPut]
        [Route("CompletePayment")]
        public async Task<ActionResult<SaleStatusResourceModel>> CompletePayment(CompletePaymentInputModel inputModel)
        {
            try
            {
                GetSaleInfoUtilityModel saleModel = await this.GetSaleInfo(inputModel.SaleId);

                if (saleModel == null)
                {
                    return this.NotFound();
                }

                Guid currentUserId = this.GetUserId(this.User);

                if (saleModel.BuyerId == currentUserId && saleModel.Status == Status.PaymentPending)
                {
                    var saleStatusModel = await this.salesService.CompletePayment<SaleStatusResourceModel>(inputModel);

                    var addedLogModel = await this.saleLogsService.AddLogToSale(saleModel.Id, SaleLogs.Paid);

                    await this.saleLogHubContext.Clients.Group(saleModel.Id.ToString())
                        .RecieveLogNotification(addedLogModel.Content);

                    return saleStatusModel;
                }
                else
                {
                    return this.Forbid();
                }
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpPut]
        [Route("ConfirmItemRecieved")]
        public async Task<ActionResult<SaleStatusResourceModel>> ConfirmItemRecieved(ConfirmItemRecievedInputModel inputModel)
        {
            try
            {
                GetSaleInfoUtilityModel saleModel = await this.GetSaleInfo(inputModel.SaleId);

                if (saleModel == null)
                {
                    return this.NotFound();
                }

                Guid currentUserId = this.GetUserId(this.User);

                if (saleModel.BuyerId == currentUserId && saleModel.Status == Status.Sent)
                {
                    var saleStatusModel = await this.salesService.ConfirmItemRecieved<SaleStatusResourceModel>(inputModel);

                    var addedLogModel = await this.saleLogsService.AddLogToSale(saleModel.Id, SaleLogs.ItemRecieved);

                    await this.saleLogHubContext.Clients.Group(saleModel.Id.ToString())
                        .RecieveLogNotification(addedLogModel.Content);

                    return saleStatusModel;
                }
                else
                {
                    return this.Forbid();
                }
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpPut]
        [Route("ConfirmItemSent")]
        public async Task<ActionResult<SaleStatusResourceModel>> ConfirmItemSent(ConfirmItemSentInputModel inputModel)
        {
            try
            {
                GetSaleInfoUtilityModel saleModel = await this.GetSaleInfo(inputModel.SaleId);

                if (saleModel == null)
                {
                    return this.NotFound();
                }

                Guid currentUserId = this.GetUserId(this.User);

                if (saleModel.SellerId == currentUserId && saleModel.Status == Status.Paid)
                {
                    var saleStatusModel = await this.salesService.ConfirmItemSent<SaleStatusResourceModel>(inputModel);

                    var addedLogModel = await this.saleLogsService.AddLogToSale(saleModel.Id, SaleLogs.ItemSent);

                    await this.saleLogHubContext.Clients.Group(saleModel.Id.ToString())
                        .RecieveLogNotification(addedLogModel.Content);

                    return saleStatusModel;
                }
                else
                {
                    return this.Forbid();
                }
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<CreateSaleResourceModel>> Create(CreateSaleInputModel inputModel)
        {
            try
            {           
                return this.Created(await this.salesService
                    .CreateSale<CreateSaleResourceModel>(inputModel, this.GetUserId(this.User)));
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpPut]
        public async Task<ActionResult<EditSaleResourceModel>> Update(EditSaleInputModel inputModel)
        {
            try
            {
                GetSaleInfoUtilityModel saleInfoModel = await this.GetSaleInfo(inputModel.SaleId);

                if (saleInfoModel == null)
                {
                    return this.NotFound();
                }

                Guid currentUserId = this.GetUserId(this.User);

                if (saleInfoModel.SellerId == currentUserId && saleInfoModel.Status == Status.Open)
                {

                    EditSaleResourceModel saleModel = await this.salesService.EditSale<EditSaleResourceModel>(inputModel);
                    
                    var addedLogModel = await this.saleLogsService.AddLogToSale(saleModel.Id, SaleLogs.SaleEdit);

                    await this.saleLogHubContext.Clients.Group(saleModel.Id.ToString())
                        .RecieveLogNotification(addedLogModel.Content);

                    return saleModel;
                }
                else
                {
                    return this.Forbid();
                }


            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetSaleResourceModel>> Get(Guid id)
        {
            try
            {
                GetSaleResourceModel saleInfoModel = await this.salesService.GetSale<GetSaleResourceModel>(id);

                if (saleInfoModel == null)
                {
                    return this.NotFound();
                }

                Guid currentUserId = this.GetUserId(this.User);

                if ((saleInfoModel.BuyerId != currentUserId && saleInfoModel.SellerId != currentUserId)
                    && saleInfoModel.Status != Status.Open)
                {
                    return this.Forbid();
                }

                return saleInfoModel;
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpGet]
        [Route("GetAllSalesForRelease/{id}")]
        public async Task<ActionResult<IEnumerable<GetAllSalesForReleaseResouceModel>>> GetAllSalesForRelease(Guid id)
        {
            try
            {                               
                return await this.salesService.GetAllSalesForRelease<GetAllSalesForReleaseResouceModel>(id);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpGet]
        [Route("GetUserPurchases")]
        public async Task<ActionResult<IEnumerable<GetUserPurchasesResourceModel>>> GetUserPurchases()
        {
            try
            {
                return await this.salesService
                    .GetUserPurchases<GetUserPurchasesResourceModel>(this.GetUserId(this.User));
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpGet]
        [Route("GetUserSales")]
        public async Task<ActionResult<IEnumerable<GetUserSalesResourceModel>>> GetUserSales()
        {
            try
            {
                return await this.salesService.GetUserSales<GetUserSalesResourceModel>(this.GetUserId(this.User));
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpPut]
        [Route("PlaceOrder")]
        public async Task<ActionResult<GetSaleInfoUtilityModel>> PlaceOrder(PlaceOrderInputModel inputModel)
        {
            try
            {
                GetSaleInfoUtilityModel saleInfoModel = await this.GetSaleInfo(inputModel.SaleId);

                if (saleInfoModel == null)
                {
                    return this.NotFound();
                }

                Guid currentUserId = this.GetUserId(this.User);

                if (saleInfoModel.SellerId == currentUserId || saleInfoModel.BuyerId == currentUserId
                                                        || saleInfoModel.Status != Status.Open)
                {
                    return this.Forbid();
                }

                await this.salesService.PlaceOrder<SaleStatusResourceModel>(inputModel, currentUserId);

                var addedLogModel = await this.saleLogsService.AddLogToSale(saleInfoModel.Id, SaleLogs.PlacedOrder);

                await this.saleLogHubContext.Clients.Group(saleInfoModel.Id.ToString())
                    .RecieveLogNotification(addedLogModel.Content);

                return saleInfoModel;
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpPut]
        [Route("SetShippingPrice")]
        public async Task<ActionResult<SaleStatusResourceModel>> SetShippingPrice(SetShippingPriceInputModel inputModel)
        {
            try
            {
                GetSaleInfoUtilityModel saleInfoModel = await this.GetSaleInfo(inputModel.SaleId);

                if (saleInfoModel == null)
                {
                    return this.BadRequest();
                }

                Guid currentUserId = this.GetUserId(this.User);

                if (saleInfoModel.SellerId == currentUserId && saleInfoModel.Status == Status.ShippingNegotiation)
                {
                    var saleStatusModel = await this.salesService.SetShippingPrice<SaleStatusResourceModel>(inputModel);

                    var addedLogModel = await this.saleLogsService.AddLogToSale(
                                             saleInfoModel.Id,
                                            SaleLogs.SettedShippingPrice);

                    await this.saleLogHubContext.Clients.Group(saleInfoModel.Id.ToString())
                        .RecieveLogNotification(addedLogModel.Content);

                    return saleStatusModel;
                }
                else
                {
                    return this.Forbid();
                }
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        private async Task<GetSaleInfoUtilityModel> GetSaleInfo(Guid? saleId) =>
            await this.salesService.GetSaleInfo< GetSaleInfoUtilityModel> (saleId);
    }
}
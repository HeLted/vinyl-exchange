namespace VinylExchange.Web.Controllers
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;

    using VinylExchange.Common.Enumerations;
    using VinylExchange.Data.Common.Enumerations;
    using VinylExchange.Services.Data.HelperServices.Sales.SaleLogs;
    using VinylExchange.Services.Data.HelperServices.Sales.SaleMessages;
    using VinylExchange.Services.Data.MainServices.Sales;
    using VinylExchange.Services.Logging;
    using VinylExchange.Web.Infrastructure.Hubs.SaleLog;
    using VinylExchange.Web.Models.InputModels.Sales;
    using VinylExchange.Web.Models.ResourceModels.SaleLogs;
    using VinylExchange.Web.Models.ResourceModels.Sales;
    using VinylExchange.Web.Models.Utility.Sales;

    #endregion

    [Authorize]
    public class SalesController : ApiController
    {
        private readonly ILoggerService loggerService;

        private readonly IHubContext<SaleLogsHub, ISaleLogsClient> saleLogHubContext;

        private readonly ISaleLogsService saleLogsService;
        private readonly ISaleMessagesService saleChatService;
        private readonly ISalesService salesService;

        public SalesController(
            ISalesService salesService,
            ILoggerService loggerService,
            IHubContext<SaleLogsHub, ISaleLogsClient> saleLogHubContext,
            ISaleLogsService saleLogsService,
            ISaleMessagesService saleChatService)
        {
            this.salesService = salesService;
            this.loggerService = loggerService;
            this.saleLogHubContext = saleLogHubContext;
            this.saleLogsService = saleLogsService;
            this.saleChatService = saleChatService;
        }

        [HttpPost]
        public async Task<ActionResult<CreateSaleResourceModel>> Create(CreateSaleInputModel inputModel)
        {
            try
            {
                return this.Created(
                    await this.salesService.CreateSale<CreateSaleResourceModel>(inputModel, this.GetUserId(this.User)));
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
                var saleInfoModel = await this.GetSaleInfo(inputModel.SaleId);

                if (saleInfoModel == null)
                {
                    return this.NotFound();
                }

                var currentUserId = this.GetUserId(this.User);

                if (saleInfoModel.SellerId == currentUserId
                    && saleInfoModel.Status == Status.Open)
                {
                    var saleModel = await this.salesService.EditSale<EditSaleResourceModel>(inputModel);

                    await this.saleLogsService.ClearSaleLogs(saleInfoModel.Id);

                    await this.saleLogHubContext.Clients.Group(saleModel.Id.ToString())
                        .RecieveLogNotification("Sale was edited by seller!");

                    return saleModel;
                }

                return this.Forbid();
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<RemoveSaleResourceModel>> Remove(Guid id)
        {
            try
            {
                var saleInfoModel = await this.salesService.GetSale<GetSaleInfoUtilityModel>(id);

                if (saleInfoModel == null)
                {
                    return this.NotFound();
                }

                if (saleInfoModel.SellerId == this.GetUserId(this.User))
                {
                    return await this.salesService.RemoveSale<RemoveSaleResourceModel>(saleInfoModel.Id);
                }

                return this.Forbid();
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
                var saleInfoModel = await this.salesService.GetSale<GetSaleResourceModel>(id);

                if (saleInfoModel == null)
                {
                    return this.NotFound();
                }

                var currentUserId = this.GetUserId(this.User);

                if (saleInfoModel.BuyerId != currentUserId
                    && saleInfoModel.SellerId != currentUserId
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
        [AllowAnonymous]
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
                return await this.salesService.GetUserPurchases<GetUserPurchasesResourceModel>(
                           this.GetUserId(this.User));
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
                var saleInfoModel = await this.GetSaleInfo(inputModel.SaleId);

                if (saleInfoModel == null)
                {
                    return this.NotFound();
                }

                var currentUserId = this.GetUserId(this.User);

                if (saleInfoModel.SellerId == currentUserId
                    || saleInfoModel.BuyerId == currentUserId
                    )
                {
                    return this.Forbid();
                }

                await this.salesService.PlaceOrder<SaleStatusResourceModel>(inputModel.SaleId,inputModel.AddressId, currentUserId);

                var addedLogModel = await this.saleLogsService.AddLogToSale<AddLogToSaleResourceModel>(saleInfoModel.Id, SaleLogs.PlacedOrder);

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
        [Route("CancelOrder")]
        public async Task<ActionResult<GetSaleInfoUtilityModel>> CancelOrder(CancelOrderInputModel inputModel)
        {
            try
            {
                var saleInfoModel = await this.GetSaleInfo(inputModel.SaleId);

                if (saleInfoModel == null)
                {
                    return this.NotFound();
                }

                var currentUserId = this.GetUserId(this.User);

                if (saleInfoModel.BuyerId != currentUserId)
                {
                    return this.Forbid();
                }

                await this.salesService.CancelOrder<SaleStatusResourceModel>(inputModel.SaleId, currentUserId);

                await this.saleLogsService.ClearSaleLogs(saleInfoModel.Id);

                await this.saleLogHubContext.Clients.Group(saleInfoModel.Id.ToString())
                    .RecieveLogNotification("Order was canceled by buyer!");

                await this.saleChatService.ClearSaleMessages(saleInfoModel.Id);

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
                var saleInfoModel = await this.GetSaleInfo(inputModel.SaleId);

                if (saleInfoModel == null)
                {
                    return this.NotFound();
                }

                var currentUserId = this.GetUserId(this.User);

                if (saleInfoModel.SellerId == currentUserId)
                {
                    var saleStatusModel = await this.salesService.SetShippingPrice<SaleStatusResourceModel>(inputModel.SaleId,inputModel.ShippingPrice);

                    var addedLogModel = await this.saleLogsService.AddLogToSale<AddLogToSaleResourceModel>(
                                            saleInfoModel.Id,
                                            SaleLogs.SettedShippingPrice);

                    await this.saleLogHubContext.Clients.Group(saleInfoModel.Id.ToString())
                        .RecieveLogNotification(addedLogModel.Content);

                    return saleStatusModel;
                }

                return this.Forbid();
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpPut]
        [Route("CompletePayment")]
        public async Task<ActionResult<SaleStatusResourceModel>> CompletePayment(CompletePaymentInputModel inputModel)
        {
            try
            {
                var saleModel = await this.GetSaleInfo(inputModel.SaleId);

                if (saleModel == null)
                {
                    return this.NotFound();
                }

                var currentUserId = this.GetUserId(this.User);

                if (saleModel.BuyerId == currentUserId)
                {
                    var saleStatusModel = await this.salesService.CompletePayment<SaleStatusResourceModel>(inputModel.SaleId,inputModel.OrderId);

                    var addedLogModel = await this.saleLogsService.AddLogToSale<AddLogToSaleResourceModel>(saleModel.Id, SaleLogs.Paid);

                    await this.saleLogHubContext.Clients.Group(saleModel.Id.ToString())
                        .RecieveLogNotification(addedLogModel.Content);

                    return saleStatusModel;
                }

                return this.Forbid();
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
                var saleModel = await this.GetSaleInfo(inputModel.SaleId);

                if (saleModel == null)
                {
                    return this.NotFound();
                }

                var currentUserId = this.GetUserId(this.User);

                if (saleModel.SellerId == currentUserId)
                {
                    var saleStatusModel = await this.salesService.ConfirmItemSent<SaleStatusResourceModel>(inputModel.SaleId);

                    var addedLogModel = await this.saleLogsService.AddLogToSale<AddLogToSaleResourceModel>(saleModel.Id, SaleLogs.ItemSent);

                    await this.saleLogHubContext.Clients.Group(saleModel.Id.ToString())
                        .RecieveLogNotification(addedLogModel.Content);

                    return saleStatusModel;
                }

                return this.Forbid();
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpPut]
        [Route("ConfirmItemRecieved")]
        public async Task<ActionResult<SaleStatusResourceModel>> ConfirmItemRecieved(
          ConfirmItemRecievedInputModel inputModel)
        {
            try
            {
                var saleModel = await this.GetSaleInfo(inputModel.SaleId);

                if (saleModel == null)
                {
                    return this.NotFound();
                }

                var currentUserId = this.GetUserId(this.User);

                if (saleModel.BuyerId == currentUserId)
                {
                    var saleStatusModel =
                        await this.salesService.ConfirmItemRecieved<SaleStatusResourceModel>(inputModel.SaleId);

                    var addedLogModel = await this.saleLogsService.AddLogToSale<AddLogToSaleResourceModel>(saleModel.Id, SaleLogs.ItemRecieved);

                    await this.saleLogHubContext.Clients.Group(saleModel.Id.ToString())
                        .RecieveLogNotification(addedLogModel.Content);

                    return saleStatusModel;
                }

                return this.Forbid();
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        private async Task<GetSaleInfoUtilityModel> GetSaleInfo(Guid? saleId)
        {
            return await this.salesService.GetSale<GetSaleInfoUtilityModel>(saleId);
        }
    }
}
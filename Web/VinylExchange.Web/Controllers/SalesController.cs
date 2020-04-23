namespace VinylExchange.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Common.Enumerations;
    using Data.Common.Enumerations;
    using Infrastructure.Hubs.SaleLog;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;
    using Models.InputModels.Sales;
    using Models.ResourceModels.SaleLogs;
    using Models.ResourceModels.Sales;
    using Models.Utility.Sales;
    using Services.Data.HelperServices.Sales.SaleLogs;
    using Services.Data.HelperServices.Sales.SaleMessages;
    using Services.Data.MainServices.Sales.Contracts;
    using Services.Logging;

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
                var resourceModel = await salesService.CreateSale<CreateSaleResourceModel>(
                    inputModel.VinylGrade,
                    inputModel.SleeveGrade,
                    inputModel.Description,
                    inputModel.Price,
                    inputModel.ShipsFromAddressId,
                    inputModel.ReleaseId,
                    GetUserId(User));

                return Created(resourceModel);
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<ActionResult<EditSaleResourceModel>> Update(EditSaleInputModel inputModel)
        {
            try
            {
                var saleInfoModel = await GetSaleInfo(inputModel.SaleId);

                if (saleInfoModel == null)
                {
                    return NotFound();
                }

                var currentUserId = GetUserId(User);

                if (saleInfoModel.SellerId == currentUserId &&
                    saleInfoModel.Status == Status.Open)
                {
                    var saleModel = await salesService.EditSale<EditSaleResourceModel>(inputModel);

                    await saleLogsService.ClearSaleLogs(saleInfoModel.Id);

                    await saleLogHubContext.Clients.Group(saleModel.Id.ToString())
                        .RecieveLogNotification("Sale was edited by seller!");

                    return saleModel;
                }

                return Forbid();
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<RemoveSaleResourceModel>> Remove(Guid id)
        {
            try
            {
                var saleInfoModel = await salesService.GetSale<GetSaleInfoUtilityModel>(id);

                if (saleInfoModel == null)
                {
                    return NotFound();
                }

                if (saleInfoModel.SellerId == GetUserId(User))
                {
                    return await salesService.RemoveSale<RemoveSaleResourceModel>(saleInfoModel.Id);
                }

                return Forbid();
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetSaleResourceModel>> Get(Guid id)
        {
            try
            {
                var saleInfoModel = await salesService.GetSale<GetSaleResourceModel>(id);

                if (saleInfoModel == null)
                {
                    return NotFound();
                }

                var currentUserId = GetUserId(User);

                if (saleInfoModel.BuyerId != currentUserId &&
                    saleInfoModel.SellerId != currentUserId &&
                    saleInfoModel.Status != Status.Open)
                {
                    return Forbid();
                }

                return saleInfoModel;
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetAllSalesForRelease/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<GetAllSalesForReleaseResouceModel>>> GetAllSalesForRelease(Guid id)
        {
            try
            {
                return await salesService.GetAllSalesForRelease<GetAllSalesForReleaseResouceModel>(id);
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetUserPurchases")]
        public async Task<ActionResult<IEnumerable<GetUserPurchasesResourceModel>>> GetUserPurchases()
        {
            try
            {
                return await salesService.GetUserPurchases<GetUserPurchasesResourceModel>(
                    GetUserId(User));
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetUserSales")]
        public async Task<ActionResult<IEnumerable<GetUserSalesResourceModel>>> GetUserSales()
        {
            try
            {
                return await salesService.GetUserSales<GetUserSalesResourceModel>(GetUserId(User));
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("PlaceOrder")]
        public async Task<ActionResult<GetSaleInfoUtilityModel>> PlaceOrder(PlaceOrderInputModel inputModel)
        {
            try
            {
                var saleInfoModel = await GetSaleInfo(inputModel.SaleId);

                if (saleInfoModel == null)
                {
                    return NotFound();
                }

                var currentUserId = GetUserId(User);

                if (saleInfoModel.SellerId == currentUserId ||
                    saleInfoModel.BuyerId == currentUserId)
                {
                    return Forbid();
                }

                await salesService.PlaceOrder<SaleStatusResourceModel>(
                    inputModel.SaleId,
                    inputModel.AddressId,
                    currentUserId);

                var addedLogModel =
                    await saleLogsService.AddLogToSale<AddLogToSaleResourceModel>(
                        saleInfoModel.Id,
                        SaleLogs.PlacedOrder);

                await saleLogHubContext.Clients.Group(saleInfoModel.Id.ToString())
                    .RecieveLogNotification(addedLogModel.Content);

                return saleInfoModel;
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("CancelOrder")]
        public async Task<ActionResult<GetSaleInfoUtilityModel>> CancelOrder(CancelOrderInputModel inputModel)
        {
            try
            {
                var saleInfoModel = await GetSaleInfo(inputModel.SaleId);

                if (saleInfoModel == null)
                {
                    return NotFound();
                }

                var currentUserId = GetUserId(User);

                if (saleInfoModel.BuyerId != currentUserId)
                {
                    return Forbid();
                }

                await salesService.CancelOrder<SaleStatusResourceModel>(inputModel.SaleId, currentUserId);

                await saleLogsService.ClearSaleLogs(saleInfoModel.Id);

                await saleLogHubContext.Clients.Group(saleInfoModel.Id.ToString())
                    .RecieveLogNotification("Order was canceled by buyer!");

                await saleChatService.ClearSaleMessages(saleInfoModel.Id);

                return saleInfoModel;
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("SetShippingPrice")]
        public async Task<ActionResult<SaleStatusResourceModel>> SetShippingPrice(SetShippingPriceInputModel inputModel)
        {
            try
            {
                var saleInfoModel = await GetSaleInfo(inputModel.SaleId);

                if (saleInfoModel == null)
                {
                    return NotFound();
                }

                var currentUserId = GetUserId(User);

                if (saleInfoModel.SellerId == currentUserId)
                {
                    var saleStatusModel = await salesService.SetShippingPrice<SaleStatusResourceModel>(
                        inputModel.SaleId,
                        inputModel.ShippingPrice);

                    var addedLogModel = await saleLogsService.AddLogToSale<AddLogToSaleResourceModel>(
                        saleInfoModel.Id,
                        SaleLogs.SettedShippingPrice);

                    await saleLogHubContext.Clients.Group(saleInfoModel.Id.ToString())
                        .RecieveLogNotification(addedLogModel.Content);

                    return saleStatusModel;
                }

                return Forbid();
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("CompletePayment")]
        public async Task<ActionResult<SaleStatusResourceModel>> CompletePayment(CompletePaymentInputModel inputModel)
        {
            try
            {
                var saleModel = await GetSaleInfo(inputModel.SaleId);

                if (saleModel == null)
                {
                    return NotFound();
                }

                var currentUserId = GetUserId(User);

                if (saleModel.BuyerId == currentUserId)
                {
                    var saleStatusModel =
                        await salesService.CompletePayment<SaleStatusResourceModel>(
                            inputModel.SaleId,
                            inputModel.OrderId);

                    var addedLogModel =
                        await saleLogsService.AddLogToSale<AddLogToSaleResourceModel>(saleModel.Id, SaleLogs.Paid);

                    await saleLogHubContext.Clients.Group(saleModel.Id.ToString())
                        .RecieveLogNotification(addedLogModel.Content);

                    return saleStatusModel;
                }

                return Forbid();
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("ConfirmItemSent")]
        public async Task<ActionResult<SaleStatusResourceModel>> ConfirmItemSent(ConfirmItemSentInputModel inputModel)
        {
            try
            {
                var saleModel = await GetSaleInfo(inputModel.SaleId);

                if (saleModel == null)
                {
                    return NotFound();
                }

                var currentUserId = GetUserId(User);

                if (saleModel.SellerId == currentUserId)
                {
                    var saleStatusModel =
                        await salesService.ConfirmItemSent<SaleStatusResourceModel>(inputModel.SaleId);

                    var addedLogModel =
                        await saleLogsService.AddLogToSale<AddLogToSaleResourceModel>(
                            saleModel.Id,
                            SaleLogs.ItemSent);

                    await saleLogHubContext.Clients.Group(saleModel.Id.ToString())
                        .RecieveLogNotification(addedLogModel.Content);

                    return saleStatusModel;
                }

                return Forbid();
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("ConfirmItemRecieved")]
        public async Task<ActionResult<SaleStatusResourceModel>> ConfirmItemRecieved(
            ConfirmItemRecievedInputModel inputModel)
        {
            try
            {
                var saleModel = await GetSaleInfo(inputModel.SaleId);

                if (saleModel == null)
                {
                    return NotFound();
                }

                var currentUserId = GetUserId(User);

                if (saleModel.BuyerId == currentUserId)
                {
                    var saleStatusModel =
                        await salesService.ConfirmItemRecieved<SaleStatusResourceModel>(inputModel.SaleId);

                    var addedLogModel =
                        await saleLogsService.AddLogToSale<AddLogToSaleResourceModel>(
                            saleModel.Id,
                            SaleLogs.ItemRecieved);

                    await saleLogHubContext.Clients.Group(saleModel.Id.ToString())
                        .RecieveLogNotification(addedLogModel.Content);

                    return saleStatusModel;
                }

                return Forbid();
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }

        private async Task<GetSaleInfoUtilityModel> GetSaleInfo(Guid? saleId)
        {
            return await salesService.GetSale<GetSaleInfoUtilityModel>(saleId);
        }
    }
}
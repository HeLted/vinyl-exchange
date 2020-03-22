namespace VinylExchange.Services.Data.Tests
{
    #region

    using VinylExchange.Data;
    using VinylExchange.Services.Data.MainServices.Sales;
    using VinylExchange.Services.Data.Tests.TestFactories;
    using VinylExchange.Web.Models.InputModels.Sales;

    #endregion

    public class SalesServiceTests
    {
        private readonly VinylExchangeDbContext dbContext;

        private readonly ISalesService releasesService;

        private readonly CreateSaleInputModel testCreateSaleInputModel = new CreateSaleInputModel();

        public SalesServiceTests()
        {
            this.dbContext = DbFactory.CreateVinylExchangeDbContext();
        }
    }
}
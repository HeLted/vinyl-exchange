using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VinylExchange.Data.Models;
using VinylExchange.Models.InputModels.Sales;
using VinylExchange.Models.ResourceModels.Sales;

namespace VinylExchange.Services.Data.MainServices.Sales
{
    public interface ISalesService
    {
        Task<GetSaleResourceModel> GetSale(Guid saleId);
        Task<Sale> CreateSale(CreateSaleInputModel inputModel,Guid sellerId);
    }
}

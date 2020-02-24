using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VinylExchange.Models.ResourceModels.Sales;

namespace VinylExchange.Services.Data.MainServices.Sales
{
    public interface ISalesService
    {
        Task<GetSaleResourceModel> GetSale(Guid saleId);
    }
}

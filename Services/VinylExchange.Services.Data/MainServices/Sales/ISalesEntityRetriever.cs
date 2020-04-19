using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VinylExchange.Data.Models;

namespace VinylExchange.Services.Data.MainServices.Sales
{
    public interface ISalesEntityRetriever
    {        
        Task<Sale> GetSale(Guid? saleId);        
    }
}

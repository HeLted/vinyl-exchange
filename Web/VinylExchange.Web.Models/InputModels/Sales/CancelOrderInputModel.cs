using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VinylExchange.Web.Models.InputModels.Sales
{
    public class CancelOrderInputModel
    {
        [Required]
        public Guid? SaleId { get; set; }
    }
}

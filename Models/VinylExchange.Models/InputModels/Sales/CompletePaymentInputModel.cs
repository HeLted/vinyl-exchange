using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VinylExchange.Models.InputModels.Sales
{
   public  class CompletePaymentInputModel
    {

        [Required]
        public Nullable<Guid> SaleId { get; set; }


        [Required]
        public string OrderId { get; set; }

    }
}

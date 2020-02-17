using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VinylExchange.Data.Models.Enums;

namespace VinylExchange.Models.InputModels.Collections
{
    public class AddToCollectionInputModel
    {
        [Required]
        public Condition VinylGrade { get; set; }
        [Required]
        public Condition SleeveGrade { get; set; }
        [Required]
        public string Description { get; set; }

           
    }
}

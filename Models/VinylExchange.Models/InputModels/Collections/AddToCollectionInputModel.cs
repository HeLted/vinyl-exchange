using System.ComponentModel.DataAnnotations;
using VinylExchange.Data.Common.Enumerations;

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

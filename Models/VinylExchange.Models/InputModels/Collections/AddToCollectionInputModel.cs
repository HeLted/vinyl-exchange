using System.ComponentModel.DataAnnotations;
using VinylExchange.Data.Common.Enumerations;
using VinylExchange.Data.Models;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Models.InputModels.Collections
{
    public class AddToCollectionInputModel :IMapTo<CollectionItem>
    {
        [Required]
        public Condition VinylGrade { get; set; }
        [Required]
        public Condition SleeveGrade { get; set; }
        [Required]
        public string Description { get; set; }
           
    }
}

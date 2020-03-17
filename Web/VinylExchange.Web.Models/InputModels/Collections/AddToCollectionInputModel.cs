namespace VinylExchange.Web.Models.InputModels.Collections
{
    #region

    using System.ComponentModel.DataAnnotations;

    using VinylExchange.Data.Common.Enumerations;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    #endregion

    public class AddToCollectionInputModel : IMapTo<CollectionItem>
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public Condition SleeveGrade { get; set; }

        [Required]
        public Condition VinylGrade { get; set; }
    }
}
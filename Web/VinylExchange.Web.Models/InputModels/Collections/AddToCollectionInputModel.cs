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

        public string Description { get; set; }

        [Required]
        [Range((int)Condition.NotSelected,(int)Condition.Mint,ErrorMessage ="Please select correct option for field")]
        public Condition SleeveGrade { get; set; }

        [Required]
        [Range((int)Condition.NotSelected,(int)Condition.Mint,ErrorMessage ="Please select correct option for field")]
        public Condition VinylGrade { get; set; }
    }
}
namespace VinylExchange.Web.Models.InputModels.Collections
{
    #region

    #region

    using System;
    using System.ComponentModel.DataAnnotations;

    using VinylExchange.Data.Common.Enumerations;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    #endregion

    #endregion

    public class AddToCollectionInputModel : IMapTo<CollectionItem>
    {
        [Required]
        [Range(
            (int)Condition.NotSelected,
            (int)Condition.Mint,
            ErrorMessage = "Please select correct option for field")]
        public Condition VinylGrade { get; set; }

        [Required]
        [Range(
            (int)Condition.NotSelected,
            (int)Condition.Mint,
            ErrorMessage = "Please select correct option for field")]
        public Condition SleeveGrade { get; set; }
        
        public string Description { get; set; }

        [Required]
        public Guid? ReleaseId { get; set; }
    }
}
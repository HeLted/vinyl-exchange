namespace VinylExchange.Web.Models.InputModels.Collections
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Data.Common.Enumerations;
    using Data.Models;
    using Services.Mapping;

    public class AddToCollectionInputModel : IMapTo<CollectionItem>
    {
        [Required]
        [Range(
            (int) Condition.NotSelected,
            (int) Condition.Mint,
            ErrorMessage = "Please select correct option for field")]
        public Condition VinylGrade { get; set; }

        [Required]
        [Range(
            (int) Condition.NotSelected,
            (int) Condition.Mint,
            ErrorMessage = "Please select correct option for field")]
        public Condition SleeveGrade { get; set; }

        public string Description { get; set; }

        [Required]
        public Guid? ReleaseId { get; set; }
    }
}
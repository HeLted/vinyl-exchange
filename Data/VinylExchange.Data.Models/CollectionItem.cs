namespace VinylExchange.Data.Models
{
    #region

    using System;
    using System.ComponentModel.DataAnnotations;

    using VinylExchange.Data.Common.Enumerations;
    using VinylExchange.Data.Common.Models;

    #endregion

    public class CollectionItem : BaseModel
    {
     
        public string Description { get; set; }

        public Release Release { get; set; }

        [Required]
        public Guid ReleaseId { get; set; }

        [Required]
        public Condition SleeveGrade { get; set; }

        public VinylExchangeUser User { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Condition VinylGrade { get; set; }
    }
}
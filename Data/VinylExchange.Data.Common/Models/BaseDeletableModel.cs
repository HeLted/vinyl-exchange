namespace VinylExchange.Data.Common.Models
{
    using System;

    public abstract class BaseDeletableModel : BaseAuditModel, IDeletableEntity
    {
        public DateTime? DeletedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}
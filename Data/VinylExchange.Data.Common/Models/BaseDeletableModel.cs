namespace VinylExchange.Data.Common.Models
{
    #region

    using System;

    #endregion

    public abstract class BaseDeletableModel : BaseAuditModel, IDeletableEntity
    {
        public DateTime? DeletedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}
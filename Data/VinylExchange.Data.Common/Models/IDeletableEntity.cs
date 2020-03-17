namespace VinylExchange.Data.Common.Models
{
    #region

    using System;

    #endregion

    public interface IDeletableEntity
    {
        DateTime? DeletedOn { get; set; }

        bool IsDeleted { get; set; }
    }
}
namespace VinylExchange.Data.Common.Models
{
    using System;

    public interface IDeletableEntity
    {
        DateTime? DeletedOn { get; set; }

        bool IsDeleted { get; set; }
    }
}
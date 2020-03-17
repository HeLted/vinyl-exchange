namespace VinylExchange.Data.Common.Models
{
    #region

    using System;

    #endregion

    public interface IAuditInfo
    {
        DateTime CreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}
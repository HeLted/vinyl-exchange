namespace VinylExchange.Data.Common.Models
{
    #region

    using System;
    using System.ComponentModel.DataAnnotations;

    #endregion

    public abstract class BaseAuditModel : IAuditInfo
    {
        public BaseAuditModel()
        {
            this.Id = Guid.NewGuid();
            this.CreatedOn = DateTime.UtcNow;
        }

        public DateTime CreatedOn { get; set; }

        [Key]
        public Guid Id { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
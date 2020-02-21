using System;
using System.ComponentModel.DataAnnotations;

namespace VinylExchange.Data.Common.Models
{
    public abstract class BaseModel<TKey> : IAuditInfo
    {

        public BaseModel()
        {
            this.CreatedOn = DateTime.UtcNow;
        }
        [Key]
        public TKey Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}

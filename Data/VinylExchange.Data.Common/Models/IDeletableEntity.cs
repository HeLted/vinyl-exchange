using System;
using System.Collections.Generic;
using System.Text;

namespace VinylExchange.Data.Common.Models
{
    public interface IDeletableEntity
    {
        bool IsDeleted { get; set; }
        DateTime? DeletedOn { get; set; }
    }
}

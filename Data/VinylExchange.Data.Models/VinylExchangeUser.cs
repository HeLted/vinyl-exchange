using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace VinylExchange.Data.Models
{
    public class VinylExchangeUser : IdentityUser
    {
      public   ICollection<CollectionItem> Collection { get; set; } = new HashSet<CollectionItem>();
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace VinylExchange.Data.Models
{
    public class VinylExchangeUser : IdentityUser
    {
      public   ICollection<CollectionItem> ReleaseCollection { get; set; } = new HashSet<CollectionItem>();
    }
}

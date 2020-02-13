using System;
using System.Collections.Generic;
using System.Text;

namespace VinylExchange.Data.Models
{
    public class CollectionItem
    {

        public CollectionItem()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
                
        public Guid ReleaseId { get; set; }

        public Release Release { get; set; }

        public string UserId { get; set; }

        public VinylExchangeUser User { get; set; }



    }
}

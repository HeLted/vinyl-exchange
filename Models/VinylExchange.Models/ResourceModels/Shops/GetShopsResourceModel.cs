using System;
using System.Collections.Generic;
using System.Text;
using VinylExchange.Models.ResourceModels.ShopFiles;

namespace VinylExchange.Models.ResourceModels.Shops
{
    public class GetShopsResourceModel
    {

        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public string Country { get; set; }
        public string Town { get; set; }

        public ShopFileResourceModel MainPhoto { get; set; }

    }
}

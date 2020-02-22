using System;
using System.Collections.Generic;
using System.Text;
using VinylExchange.Data.Models;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Models.ResourceModels.ShopFiles
{
    public class ShopFileResourceModel  : IMapFrom<ShopFile>
    {
        public Guid Id { get; set; }

        public string Path { get; set; }

        public string FileName { get; set; }

    }
}

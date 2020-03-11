using System;
using System.Collections.Generic;
using System.Text;
using VinylExchange.Data.Models;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Web.Models.ResourceModels.Collections
{
    public class AddToCollectionResourceModel : IMapFrom<CollectionItem>
    {
        public Guid Id { get; set; }
    }
}

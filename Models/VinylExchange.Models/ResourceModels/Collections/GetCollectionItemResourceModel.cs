using System;
using VinylExchange.Data.Common.Enumerations;
using VinylExchange.Data.Models;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Models.ResourceModels.Collections
{
    public class GetCollectionItemResourceModel :IMapFrom<CollectionItem>
    {
        public Guid Id { get; set; }
  
        public Condition VinylGrade { get; set; }
      
        public Condition SleeveGrade { get; set; }
      
        public string Description { get; set; }
    }
}

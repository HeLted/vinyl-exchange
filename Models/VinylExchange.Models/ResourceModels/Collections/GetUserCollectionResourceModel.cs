using AutoMapper;
using System;
using VinylExchange.Data.Models;
using VinylExchange.Models.ResourceModels.ReleaseFiles;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Models.ResourceModels.Collections
{
    public class GetUserCollectionResourceModel :IMapFrom<CollectionItem>, IHaveCustomMappings
    {
        #region CollectionItem
        public Guid Id { get; set; }

        public Guid ReleaseId { get; set; }
  
        public string VinylGrade { get; set; }
       
        public string SleeveGrade { get; set; }
      
        public string Description { get; set; }

        #endregion

        #region Release
        public string Artist { get; set; }
        public string Title { get; set; }
        public ReleaseFileResourceModel CoverArt { get; set; }
        #endregion


        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CollectionItem, GetUserCollectionResourceModel>()
                 .ForMember(m => m.VinylGrade, ci => ci.MapFrom(x => x.VinylGrade.ToString()))
                 .ForMember(m => m.SleeveGrade, ci => ci.MapFrom(x => x.SleeveGrade.ToString()));                
        }
    }
}

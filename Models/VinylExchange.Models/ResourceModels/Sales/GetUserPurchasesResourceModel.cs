using AutoMapper;
using System;
using VinylExchange.Data.Common.Enumerations;
using VinylExchange.Data.Models;
using VinylExchange.Models.ResourceModels.ReleaseFiles;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Models.ResourceModels.Sales
{
    public class GetUserPurchasesResourceModel: IMapFrom<Sale>,IHaveCustomMappings
    {
        #region Sale
        public Guid Id { get; set; }
        
        public Condition VinylGrade { get; set; }

        public Guid ReleaseId { get; set; }


        public Condition SleeveGrade { get; set; }
        
        public Status Status { get; set; }

        #endregion


        #region Release
        public string Artist { get; set; }
        public string Title { get; set; }
        public ReleaseFileResourceModel CoverArt { get; set; }
        #endregion




        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Sale, GetUserPurchasesResourceModel>()
                 .ForMember(m => m.Artist, ci => ci.MapFrom(x => x.Release.Artist))
                 .ForMember(m => m.Title, ci => ci.MapFrom(x => x.Release.Title));
        }

    }
}

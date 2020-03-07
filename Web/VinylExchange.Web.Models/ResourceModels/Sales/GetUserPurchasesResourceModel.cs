namespace VinylExchange.Web.Models.ResourceModels.Sales
{
    using System;

    using AutoMapper;

    using VinylExchange.Data.Common.Enumerations;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;
    using VinylExchange.Web.Models.ResourceModels.ReleaseFiles;

    public class GetUserPurchasesResourceModel : IMapFrom<Sale>, IHaveCustomMappings
    {
        public string Artist { get; set; }

        public ReleaseFileResourceModel CoverArt { get; set; }

        public Guid Id { get; set; }

        public Guid ReleaseId { get; set; }

        public Condition SleeveGrade { get; set; }

        public Status Status { get; set; }

        public string Title { get; set; }

        public Condition VinylGrade { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Sale, GetUserPurchasesResourceModel>()
                .ForMember(m => m.Artist, ci => ci.MapFrom(x => x.Release.Artist)).ForMember(
                    m => m.Title,
                    ci => ci.MapFrom(x => x.Release.Title));
        }
    }
}
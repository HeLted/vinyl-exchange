namespace VinylExchange.Web.Models.ResourceModels.Collections
{
    #region

    using System;
    using System.Linq;

    using AutoMapper;

    using VinylExchange.Common.Enumerations;
    using VinylExchange.Data.Common.Enumerations;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;
    using VinylExchange.Web.Models.ResourceModels.ReleaseFiles;

    #endregion

    public class GetUserCollectionResourceModel : IMapFrom<CollectionItem>, IHaveCustomMappings
    {
        public string Artist { get; set; }

        public ReleaseFileResourceModel CoverArt { get; set; }

        public string Description { get; set; }

        public Guid Id { get; set; }

        public Guid ReleaseId { get; set; }

        public Condition SleeveGrade { get; set; }

        public string Title { get; set; }

        public Condition VinylGrade { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CollectionItem, GetUserCollectionResourceModel>()
                .ForMember(m => m.Artist, ci => ci.MapFrom(x => x.Release.Artist))
                .ForMember(m => m.Title, ci => ci.MapFrom(x => x.Release.Title)).ForMember(
                    m => m.CoverArt,
                    ci => ci.MapFrom(
                        x => x.Release.ReleaseFiles.FirstOrDefault(
                            rf => rf.FileType == FileType.Image && rf.IsPreview)));
        }
    }
}
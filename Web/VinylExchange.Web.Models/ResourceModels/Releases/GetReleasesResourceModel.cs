namespace VinylExchange.Web.Models.ResourceModels.Releases
{
    using AutoMapper;
    using System;
    using System.Linq;
    using VinylExchange.Common.Enumerations;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;
    using VinylExchange.Web.Models.ResourceModels.ReleaseFiles;

    public class GetReleasesResourceModel : IMapFrom<Release>,IHaveCustomMappings
    {
        public string Artist { get; set; }

        public ReleaseFileResourceModel CoverArt { get; set; }

        public string Format { get; set; }

        public Guid Id { get; set; }

        public string Label { get; set; }

        public string Title { get; set; }

        public string Year { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Release, GetReleasesResourceModel>().ForMember(
                 m => m.CoverArt,
                 ci => ci.MapFrom(x =>
                     x.ReleaseFiles.FirstOrDefault(rf => rf.FileType == FileType.Image && rf.IsPreview)));

        }
    }
}
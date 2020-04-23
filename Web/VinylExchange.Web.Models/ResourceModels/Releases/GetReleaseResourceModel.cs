namespace VinylExchange.Web.Models.ResourceModels.Releases
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Common.Enumerations;
    using Data.Models;
    using ReleaseFiles;
    using Services.Mapping;

    public class GetReleaseResourceModel : IMapFrom<Release>, IHaveCustomMappings
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
            configuration.CreateMap<Release, GetReleaseResourceModel>().ForMember(
                m => m.CoverArt,
                ci => ci.MapFrom(
                    x => x.ReleaseFiles.FirstOrDefault(rf => rf.FileType == FileType.Image && rf.IsPreview)));
        }
    }
}
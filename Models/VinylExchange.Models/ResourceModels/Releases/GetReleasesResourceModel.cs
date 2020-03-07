namespace VinylExchange.Models.ResourceModels.Releases
{
    using System;

    using VinylExchange.Data.Models;
    using VinylExchange.Models.ResourceModels.ReleaseFiles;
    using VinylExchange.Services.Mapping;

    public class GetReleasesResourceModel : IMapFrom<Release>
    {
        public string Artist { get; set; }

        public ReleaseFileResourceModel CoverArt { get; set; }

        public string Format { get; set; }

        public Guid Id { get; set; }

        public string Label { get; set; }

        public string Title { get; set; }

        public string Year { get; set; }
    }
}
namespace VinylExchange.Web.Models.ResourceModels.Releases
{
    using System;

    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;
    using VinylExchange.Web.Models.ResourceModels.ReleaseFiles;

    public class GetReleaseResourceModel : IMapFrom<Release>
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
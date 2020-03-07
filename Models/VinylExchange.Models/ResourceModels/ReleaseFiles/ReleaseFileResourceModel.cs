namespace VinylExchange.Models.ResourceModels.ReleaseFiles
{
    using System;

    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    public class ReleaseFileResourceModel : IMapFrom<ReleaseFile>
    {
        public string FileName { get; set; }

        public Guid Id { get; set; }

        public string Path { get; set; }
    }
}
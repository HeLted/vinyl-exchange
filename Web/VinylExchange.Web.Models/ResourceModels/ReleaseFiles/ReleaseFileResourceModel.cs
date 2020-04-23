namespace VinylExchange.Web.Models.ResourceModels.ReleaseFiles
{
    using System;
    using Data.Models;
    using Services.Mapping;

    public class ReleaseFileResourceModel : IMapFrom<ReleaseFile>
    {
        public string FileName { get; set; }

        public Guid Id { get; set; }

        public string Path { get; set; }
    }
}
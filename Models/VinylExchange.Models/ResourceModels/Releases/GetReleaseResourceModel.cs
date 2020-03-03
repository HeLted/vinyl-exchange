using System;
using System.Collections.Generic;
using System.Text;
using VinylExchange.Data.Models;
using VinylExchange.Models.ResourceModels.ReleaseFiles;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Models.ResourceModels.Releases
{
    public class GetReleaseResourceModel : IMapFrom<Release>
    {
        public Guid Id { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Format { get; set; }
        public string Year { get; set; }
        public string Label { get; set; }
        public ReleaseFileResourceModel CoverArt { get; set; }
    }
}

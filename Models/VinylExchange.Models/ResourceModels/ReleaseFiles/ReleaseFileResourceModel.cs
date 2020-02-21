using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VinylExchange.Data.Models;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Models.ResourceModels.ReleaseFiles
{
    public class ReleaseFileResourceModel : IMapFrom<ReleaseFile>
    {

        public Guid Id { get; set; }

        public string Path { get; set; }

        public string FileName { get; set; }

        
    }
}

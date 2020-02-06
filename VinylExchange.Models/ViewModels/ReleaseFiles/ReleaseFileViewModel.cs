using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VinylExchange.Data.Models;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Models.ViewModels.ReleaseFiles
{
    public class ReleaseFileViewModel : IMapFrom<ReleaseFile>
    {
        public string Path { get; set; }

        public string FileName { get; set; }

        
    }
}

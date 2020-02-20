﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VinylExchange.Data.Models;
using VinylExchange.Data.Models.Enums;
using VinylExchange.Models.ResourceModels.ReleaseFiles;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Models.ResourceModels.Collections
{
    public class GetUserCollectionResourceModel : IHaveCustomMappings
    {
        #region CollectionItem
        public Guid Id { get; set; }

        public Guid ReleaseId { get; set; }
  
        public string VinylGrade { get; set; }
       
        public string SleeveGrade { get; set; }
      
        public string Description { get; set; }

        #endregion

        #region Release
        public string Artist { get; set; }
        public string Title { get; set; }
        public ReleaseFileResourceModel CoverArt { get; set; }
        #endregion


        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CollectionItem, GetUserCollectionResourceModel>()
                 .ForMember(m=> m.VinylGrade,ci=> ci.MapFrom(x=> x.VinylGrade.ToString()))
                 .ForMember(m => m.SleeveGrade, ci => ci.MapFrom(x => x.SleeveGrade.ToString()))
                 .ForMember(m => m.Artist, r => r.MapFrom(x => x.Release.Artist))
                 .ForMember(m => m.Title, r => r.MapFrom(x => x.Release.Title));
        }
    }
}
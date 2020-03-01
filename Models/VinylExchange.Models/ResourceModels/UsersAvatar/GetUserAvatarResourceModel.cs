using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VinylExchange.Data.Models;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Models.ResourceModels.UsersAvatar
{
    public class GetUserAvatarResourceModel : IHaveCustomMappings
    {
        public string Avatar { get; set; } //base64Encoded
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<VinylExchangeUser, GetUserAvatarResourceModel>()
               .ForMember(m => m.Avatar, ci => ci.MapFrom(x => Convert.ToBase64String(x.Avatar)));
        }
    }
}

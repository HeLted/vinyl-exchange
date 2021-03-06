﻿namespace VinylExchange.Web.Models.ResourceModels.UsersAvatar
{
    using System;
    using AutoMapper;
    using Data.Models;
    using Services.Mapping;

    public class GetUserAvatarResourceModel : IHaveCustomMappings
    {
        public string Avatar { get; set; } // base64Encoded

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<VinylExchangeUser, GetUserAvatarResourceModel>().ForMember(
                m => m.Avatar,
                ci => ci.MapFrom(x => Convert.ToBase64String(x.Avatar)));
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VinylExchange.Data.Models;
using VinylExchange.Models.InputModels.Releases;
using VinylExchange.Models.ViewModels.Releases;

namespace VinylExchange.Services.MainServices
{
    public interface IReleasesService
    {
        Task<IEnumerable<GetAllReleasesViewModel>> GetAllReleases();
        Task<IEnumerable<GetAllReleasesViewModel>> SearchReleases(string searchTerm);
        Task<Release> AddRelease(AddReleaseInputModel inputModel);

    }
}
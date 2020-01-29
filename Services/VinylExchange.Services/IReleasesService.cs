using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VinylExchange.Data.Models;
using VinylExchange.Models.InputModels.Releases;
using VinylExchange.Models.ViewModels.Releases;

namespace VinylExchange.Services
{
    public interface IReleasesService
    {
        Task<IEnumerable<GetAllReleasesViewModel>> GetAllReleases();
        
        Release AddRelease(AddReleaseInputModel inputModel);

    }
}

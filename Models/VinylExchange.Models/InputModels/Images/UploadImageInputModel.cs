using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace VinylExchange.Models.InputModels.Images
{
    public class UploadImageInputModel
    {
        IFormFile File { get; set; }

        string FormSessionId { get; set; }

    }
}

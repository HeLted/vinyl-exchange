namespace VinylExchange.Web.Models.InputModels.Images
{
    using Microsoft.AspNetCore.Http;

    public class UploadImageInputModel
    {
        private IFormFile File { get; set; }

        private string FormSessionId { get; set; }
    }
}
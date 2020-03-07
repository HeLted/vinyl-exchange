namespace VinylExchange.Models.InputModels.Users
{
    using System;

    public class ConfirmEmailInputModel
    {
        public string EmailConfirmToken { get; set; }

        public Guid UserId { get; set; }
    }
}
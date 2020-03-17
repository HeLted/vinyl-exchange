namespace VinylExchange.Models.InputModels.Users
{
    #region

    using System;

    #endregion

    public class ConfirmEmailInputModel
    {
        public string EmailConfirmToken { get; set; }

        public Guid UserId { get; set; }
    }
}
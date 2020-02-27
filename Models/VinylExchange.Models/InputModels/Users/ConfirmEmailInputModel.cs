using System;
using System.Collections.Generic;
using System.Text;

namespace VinylExchange.Models.InputModels.Users
{
    public class ConfirmEmailInputModel
    {
        public string EmailConfirmToken { get; set; }

        public Guid UserId { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using VinylExchange.Data.Models;
using VinylExchange.Models.Utility;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Models.InputModels.Users
{
   public class RegisterUserInputModel : IMapTo<VinylExchangeUser>
    {

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
        public string ConfirmPassword { get; set; }

    }
}

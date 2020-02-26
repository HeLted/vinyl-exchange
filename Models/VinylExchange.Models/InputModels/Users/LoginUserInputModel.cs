using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VinylExchange.Models.InputModels.Users
{
   public  class LoginUserInputModel
    {
        [Required]

        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
             
        public bool RememberMe { get; set; }
    }
}

﻿using System.ComponentModel.DataAnnotations;

namespace FGP_API.Authentification
{
    public class RegisterModel
    {
        
        [Required (ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; } 

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } 

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace recepTour.Models
{
    public partial class UserRegistration
    {
        public int Id { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required.")]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "Required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Required.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

    }
}

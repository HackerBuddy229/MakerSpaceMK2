using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MakerSpaceMK2.Models
{
    public class NewUserModel
    {


        [Required]
        [StringLength(25, ErrorMessage = "Username can't be longer than 25 characters.")]
        public string Username { get; set; }
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Password must be between 5 and 30 characters.")]
        [Required]
        public string Password { get; set; }
        public string AdminKey { get; set; }
        public string Name { get; set; }

    }
}
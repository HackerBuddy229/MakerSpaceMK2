using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MakerSpaceMK2.Models
{
    public class LoginRequestModel
    {
        [StringLength(25, ErrorMessage = "Username can't be longer than 25 characters.")]
        [Required]
        public String Username { get; set; }
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Password must be between 5 and 30 characters.")]
        [Required]
        public String Password { get; set; }

        public DateTime RequestTime { get; set; }
    }
}
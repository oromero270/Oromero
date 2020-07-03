using System.ComponentModel.DataAnnotations;
using System;
namespace Registration.Models
{
    public class goUser
    {
        [Required(ErrorMessage="Enter your email now please!")]
        [EmailAddress]
        public string LoginEmail {get; set;}

        [Required(ErrorMessage="need to put a password sherlock")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage="Needs to be at least 8 letters")]
        public string LoginPassword { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Registration.Models
{
    public class Reg
    {
        [Key]
        public int UserId{get; set;}

        [Required]
        [MinLength(2, ErrorMessage="Needs to be at least 2 letters")]
        [Display(Name = "UserName")]
        public string fName {get; set;}

        [Required]
        [MinLength(2, ErrorMessage="Needs to be at least 2 letters")]
        public string lName {get; set;}

        [Required]
        [EmailAddress]
        public string Email {get; set;}

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage="Needs to be at least 8 letters")]
        public string Password { get; set; }

        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime UpdatedAt{get; set;}= DateTime.Now;
        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string Confirm {get;set;}
    }
}
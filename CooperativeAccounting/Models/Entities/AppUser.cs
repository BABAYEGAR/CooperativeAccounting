using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CooperativeAccounting.Models.Entities
{
    public class AppUser : Transport
    {
        public long AppUserId { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "This field is does not support more than 100 characters")]
        [RegularExpression("[a-zA-Z ]*$")]
        public string Name { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "This field is does not support more than 100 characters")]
        [RegularExpression("[a-zA-Z ]*$")]
        public string Surname { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "This field is does not support more than 100 characters")]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(100, ErrorMessage = "This field is does not support more than 100 characters")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "This field must contain only digits")]
        public string Mobile { get; set; }

        public string MobileExtension { get; set; }
        public string Address { get; set; }

        [Required] public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public long? RoleId { get; set; }
        [ForeignKey("RoleId")]

        public Role Role { get; set; }
        public string Status { get; set; }

        [DisplayName("Profile Picture")] public string ProfilePicture { get; set; }

        public string BackgroundPicture { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Nationality { get; set; }
        [DisplayName("Marital Status")]
        [Required]
        public string MaritalStatus { get; set; }
        [DisplayName("Name of Spouce")]
        public string Spouce { get; set; }
        [DisplayName("Next Of Kin")]
        [Required]
        public string NextOfKin { get; set; }
        [DisplayName("Next Of Kin Address")]
        [Required]
        public string NextOfKinAddress { get; set; }
        [DisplayName("Monthly Contribution")]
        [Required]
        public double MonthlyContribution { get; set; }
        [DisplayName("State")]
        [Required]
        public int StateId { get; set; }

        [ForeignKey("StateId")]
        [Required]
        public State State { get; set; }
        [DisplayName("LGA")]
        [Required]
        public int LgaId { get; set; }

        [ForeignKey("LgaId")]
        public Lga Lga { get; set; }

    }
}

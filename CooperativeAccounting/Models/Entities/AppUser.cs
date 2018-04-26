﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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

        [Required] public long? RoleId { get; set; }

        public Role Role { get; set; }
        public string Status { get; set; }

        [DisplayName("Profile Picture")] public string ProfilePicture { get; set; }

        public string BackgroundPicture { get; set; }
        public string Website { get; set; }

        [Display(Name = "Date Of Birth")] public DateTime? DateOfBirth { get; set; }
    }
}

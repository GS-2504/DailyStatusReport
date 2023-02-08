using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DailyReportWeb_Api.Model
{
    public class UserSignUp
    {
        /// <summary>
        /// Property that holds the name of the user. This property is required and cannot be empty.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Property that holds the email of the user. This property is required, must be in email format and cannot be empty.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Property that holds the password of the user. This property is required, must be at least 6 characters long and at most 100 characters long, and must be in password format.
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Property that holds the confirmed password of the user. This property must match with the password property, and must be in password format.
        /// </summary>
      
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Property that holds the Organization Id of the user.This property is Required.
        /// </summary>
        [Required]
        public int OrganizationId { get; set; }

    }
}

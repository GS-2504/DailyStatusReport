using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DailyReportWeb_Api.Model
{
    public class ResetPassword
    {
        /// <summary>
        /// The Id of the User.
        /// </summary>
        [Required]
        public string UserId { get; set; }

        /// <summary>
        /// The token used to reset password.
        /// </summary>
        [Required]
        public string Token { get; set; }

        /// <summary>
        /// The new password of the user.
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// The password confirmation of the user.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}

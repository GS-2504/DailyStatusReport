using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DailyReportWeb_Api.Model
{
    public class ConfirmEmail
    {
        /// <summary>
        /// Property to hold the user identifier.
        /// </summary>
        [Required]
        public string UserId { get; set; }
        /// <summary>
        /// Property to hold the confirmation code.
        /// </summary>
        [Required]
        public string Code { get; set; }

    }
}

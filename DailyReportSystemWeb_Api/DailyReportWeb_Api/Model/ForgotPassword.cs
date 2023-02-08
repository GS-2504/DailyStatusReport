using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DailyReportWeb_Api.Model
{
    public class ForgotPassword
    {
        /// <summary>
        /// The email address of the user.
        /// </summary>
        [Required]
        public string Email { get; set; }
       
    }
}

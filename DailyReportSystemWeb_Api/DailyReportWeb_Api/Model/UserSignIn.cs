using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DailyReportWeb_Api.Model
{
    public class UserSignIn
    {   
        /// <summary>
        /// This property holds the Email of the user.This property is Required.
        /// </summary>
       
        [Required]
        [EmailAddress]
        public string Email { get; set; }
      
        /// <summary>
       /// This property holds the password of the user.This property is Required.
       /// </summary>
        [Required]
        public string Password { get; set; }
    }
}

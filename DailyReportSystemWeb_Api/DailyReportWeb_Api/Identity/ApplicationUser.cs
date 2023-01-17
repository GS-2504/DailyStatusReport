using DailyReportWeb_Api.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DailyReportWeb_Api.Identity
{
    public class ApplicationUser: IdentityUser
    {  
        [NotMapped]
        public string Token { get; set; }
        [NotMapped]
        public string Role { get; set; }
        public virtual ICollection<UserTask> Tasks { get; set; }
    }
}

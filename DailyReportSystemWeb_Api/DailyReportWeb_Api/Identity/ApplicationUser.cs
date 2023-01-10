using DailyReportWeb_Api.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyReportWeb_Api.Identity
{
    public class ApplicationUser: IdentityUser
    {
        public virtual ICollection<UserTask> Tasks { get; set; }
    }
}

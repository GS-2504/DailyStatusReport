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
        /// <summary>
        /// Represents the role of the user.
        /// </summary>
        [NotMapped]
        public string Role { get; set; }

        /// <summary>
        /// Represents the foreign key for the Organization.
        /// </summary>
        public int OrganizationId { get; set; }

        /// <summary>
        /// Represents the navigation property for the Organization.
        /// </summary>
        [ForeignKey("OrganizationId")]
        public Organization Organization { get; set; }

        /// <summary>
        /// Represents the collection of tasks assigned to the user.
        /// </summary>
        public virtual ICollection<UserTask> Tasks { get; set; }
    }
}

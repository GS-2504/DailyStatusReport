using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DailyReportWeb_Api.Model
{
    public class Organization
    {
        /// <summary>
        /// The unique identifier of the organization.
        /// </summary> 
        [Required]
        public int OrganizationId { get; set; }

        /// <summary>
        /// The name of the organization.
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}

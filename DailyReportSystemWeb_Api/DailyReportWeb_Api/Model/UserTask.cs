using DailyReportWeb_Api.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DailyReportWeb_Api.Model
{
    public class UserTask
    {       
            [Key]
            public int Id { get; set; }
            [Required]
            public string TaskName { get; set; }
            [Required]
            public DateTime TaskDate { get; set; }
            [Required]
            public decimal TaskHours { get; set; }
            [Required]
            public string Success { get; set; }
            [Required]
            public string Obstacle { get; set; }
            [Required]
            public string NextDayPlan { get; set; }
            public bool IsDeleted { get; set; } 
            = false;
            public string UserId { get; set; }
            [ForeignKey("UserId")]
            public virtual ApplicationUser ApplicationUser { get; set; }
            public enum Status { Pending, Approved, Disapproved }
            public Status UserStatus { get; set; } = Status.Pending;







    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DailyReportWeb_Api.Model.DTOs
{
    public class UserTaskDto
    {
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
        [Required]
        public string UserId { get; set; }
        //public enum Status { Pending, Approved, Disapproved }
        //public Status UserStatus { get; set; } = Status.Pending;

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyReportWeb_Api.Model.DTOs
{
    public class UserTaskDetailsDto
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public DateTime TaskDate { get; set; }
        public decimal TaskHours { get; set; }
        public string Success { get; set; }
        public string Obstacle { get; set; }
        public string NextDayPlan { get; set; }
        //public enum Status { Pending, Approved, Disapproved }
        public  int UserStatus { get; set; }
    }
}

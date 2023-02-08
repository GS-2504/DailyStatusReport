using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyReportWeb_Api.Model.DTOs
{
    public class UserTaskDetailsDto
    {
        /// <summary>
        /// ID of the task of the user
        /// </summary>
        public int Id { get; set; }
       
        /// <summary>
        /// get or sets the task name
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// get or set the task date
        /// </summary>
        public DateTime TaskDate { get; set; }

        /// <summary>
        /// get or set the taskhours
        /// </summary>
        public decimal TaskHours { get; set; }

        /// <summary>
        /// get or set the successes of  the task
        /// </summary>
        public string Success { get; set; }

        /// <summary>
        /// get or set the obstacle in the task
        /// </summary>
        public string Obstacle { get; set; }

        /// <summary>
        /// get or set the nextdayplan
        /// </summary>
        public string NextDayPlan { get; set; }

        /// <summary>
        /// get or set the userstatus pending,approved or disapproved
        /// </summary>
        public  int UserStatus { get; set; }
    }
}

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
        /// <summary>
        /// Represents a model for a user task.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the task.
        /// </summary>
        [Required]
        public string TaskName { get; set; }

        /// <summary>
        /// Gets or sets the date of the task.
        /// </summary>
        [Required]
        public DateTime TaskDate { get; set; }

        /// <summary>
        /// Gets or sets the hours spent on the task.
        /// </summary>
        [Required]
        public decimal TaskHours { get; set; }

        /// <summary>
        /// Gets or sets a value indicating what was successful about the task.
        /// </summary>
        [Required]
        public string Success { get; set; }

        /// <summary>
        /// Gets or sets a value indicating any obstacle encountered while completing the task.
        /// </summary>
        [Required]
        public string Obstacle { get; set; }

        /// <summary>
        /// Gets or sets the plan for the following day related to the task.
        /// </summary>
        [Required]
        public string NextDayPlan { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the task has been deleted.
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// Gets or sets the user identifier for the user who created the task.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the application user who created the task.
        /// </summary>
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        /// <summary>
        /// Represents the status of the task.
        /// </summary>
        public enum Status { Pending, Approved, Disapproved }

        /// <summary>
        /// Gets or sets the status of the task for the user.
        /// </summary>
        public Status UserStatus { get; set; } = Status.Pending;
    }
}

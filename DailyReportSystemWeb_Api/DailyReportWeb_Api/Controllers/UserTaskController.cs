using DailyReportWeb_Api.Identity;
using DailyReportWeb_Api.Model;
using DailyReportWeb_Api.Model.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyReportWeb_Api.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserTaskController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UserTaskController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost("UserTask")]
        public async Task<IActionResult> UserTask(UserTaskDto[] userTask)
        {
            if (userTask == null && !ModelState.IsValid) return NotFound();
            foreach (var task in userTask)
            {
                var addtask = new UserTask
                {
                    UserId = task.UserId,
                    TaskName = task.TaskName,
                    TaskDate = task.TaskDate,
                    TaskHours = task.TaskHours,
                    NextDayPlan = task.NextDayPlan,
                    Success = task.Success,
                    Obstacle = task.Obstacle,
                    UserStatus = Model.UserTask.Status.Pending
                };
              await  _context.UserTask.AddAsync(addtask);
            }
              await  _context.SaveChangesAsync();
                   return Ok();
        }
        //[HttpGet]
        //public async Task<IActionResult> GetAllUserTask()
        //{
        //    var result = from user in _context.UserTask join
        //                 applicationuser in _context.Users on
        //                 user.UserId equals applicationuser.Id select
        //                 new
        //                 {
        //                     name = applicationuser.NormalizedUserName,
        //                     UserTask = user.TaskName
        //                 };
        //    return Ok(result);
        //}
    }
}

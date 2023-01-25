using DailyReportWeb_Api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyReportWeb_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTaskController : ControllerBase
    {   
        [HttpPost]
        public Task<IActionResult> UserTask(UserTask[] userTask)
        {

            return null;
        }
    }
}

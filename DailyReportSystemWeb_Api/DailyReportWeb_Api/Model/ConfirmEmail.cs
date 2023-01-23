using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyReportWeb_Api.Model
{
    public class ConfirmEmail
    {
        public string UserId { get; set; }
        public string Code { get; set; }
    }
}

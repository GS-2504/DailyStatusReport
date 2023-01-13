using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyReportWeb_Api.Utility
{
    public class EmailSettings
    {
        public String PrimaryDomain { get; set; }
        public int PrimaryPort { get; set; }
        public String UserNameEmail { get; set; }
        public String UserNamePassword { get; set; }
        public String FromEmail { get; set; }
        public String ToEmail { get; set; }
        public String CCEmail { get; set; }
    }
}

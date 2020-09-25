using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class TimesheetActivityDto
    {
        public int IdTimesheetActivity { get; set; }

        
        public int IdTimesheet { get; set; }

       
        public int IdProject { get; set; }

        
        public string Comments { get; set; }

        
        public TimeSpan WorkedHours { get; set; }
    }
}

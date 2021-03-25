using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class TimesheetUpdate
    {
        public int IdTimesheet { get; set; }

        public DateTime Date { get; set; }

        public int IdLocation { get; set; }

        public int IdUser { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public TimeSpan BreakTime { get; set; }

        public List<TimesheetActivity> TimesheetActivities { get; set; }
    }
}

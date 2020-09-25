using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class TimesheetActivityViewDto
    {

        public int IdLocation { get; set; }

        public int IdUser { get; set; }

        public int IdProject { get; set; }

        public string Comments { get; set; }

        public DateTime Date { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public TimeSpan BreakTime { get; set; }
    }
}

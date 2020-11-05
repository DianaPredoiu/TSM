using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class TimesheetObj
    {
        public int IdUser { get; set; }

        public int IdProject { get; set; }

        public int IdTeam { get; set; }

        public int IdManager { get; set; }

        public string Date { get; set; }
    }
}

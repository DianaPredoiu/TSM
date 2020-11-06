using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class TimesheetObjDto
    {

        public int IdProject { get; set; }

        public int IdUser { get; set; }

        public int IdManager { get; set; }

        public int IdTeam { get; set; }

        public string Date { get; set; }
    }
}

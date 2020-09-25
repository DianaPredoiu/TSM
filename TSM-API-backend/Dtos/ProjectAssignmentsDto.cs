using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class ProjectAssignmentsDto
    {
       
        public int IdProject { get; set; }

       
        public int IdUser { get; set; }

        
        public DateTime StartDate { get; set; }

      
        public DateTime EndDate { get; set; }

       
        public bool IsActive { get; set; }
    }
}

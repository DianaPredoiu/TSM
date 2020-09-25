using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Helpers;

namespace WebApi.Dtos
{
    public class ProjectDto
    {
        
        public int IdProject { get; set; }

        
        public string ProjectName { get; set; }

        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime StartDate { get; set; }

        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime EndDate { get; set; }

        
        public bool IsActive { get; set; }
    }
}

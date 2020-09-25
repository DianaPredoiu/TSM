using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Helpers;

namespace WebApi.Dtos
{
    public class TimesheetViewDto
    {
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime Date { get; set; }

        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime StartTime { get; set; }

        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime EndTime { get; set; }

        public TimeSpan BreakTime { get; set; }

        public string Location { get; set; }

        public string Project { get; set; }

        public TimeSpan WorkedHours { get; set; }

        public string Comments { get; set; }

        public int IdUser { get; set; }

        public string Username { get; set; }
    }
}

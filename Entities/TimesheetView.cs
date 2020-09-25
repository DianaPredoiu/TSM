using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using System.Threading.Tasks;
using WebApi.Helpers;

namespace WebApi.Entities
{
    public class TimesheetView
    {
        //[DataType(DataType.Date)]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime Date { get; set; }

        //[DataType(DataType.Date)]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime StartTime { get; set; }

        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime EndTime { get; set; }

        //[JsonConverter(typeof(JsonDateConverter))]
        public TimeSpan BreakTime { get; set; }

        public string Location { get; set; }

        public string Project { get; set; }

        public TimeSpan WorkedHours { get; set; }

        public string Comments { get; set; }

        public int IdUser { get; set; }

        public string Username {get; set;}

    }
}

/*********************************************************************************
 * \file 
 * 
 * TimesheetViewDto.cs file contains the TimesheetViewDto class, which is included 
 * in Dtos namespace.
 * 
 ********************************************************************************/


//list of namespaces used in TimesheetViewDto class
using Newtonsoft.Json;
using System;
using WebApi.Helpers;
//list of namespaces

namespace WebApi.Dtos
{
    /*******************************************************
     * 
     * \class
     * 
     * TimesheetViewDto class is a model that has the structure of the 
     * TimesheetView class.
     * 
     * The properties of the class have the same decription 
     * as the ones in the TimesheetView class.
     * 
     ******************************************************/
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

    }//CLASS TimesheetViewDto

}//NAMESPACE WebApi.Dtos

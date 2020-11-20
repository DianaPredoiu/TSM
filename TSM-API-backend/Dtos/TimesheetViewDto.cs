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
using System.Collections.Generic;
using WebApi.Entities;
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
        public int IdTimesheet { get; set; } //!< The Timesheet id

        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime Date { get; set; }  //!< The date of the timesheet

        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime StartTime { get; set; }  //!< The time a user started working of the specified date

        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime EndTime { get; set; }  //!< The time a user stopped working of the specified date

        public TimeSpan BreakTime { get; set; }   //!< The time span of the break in the work day

        public string Location { get; set; }  //!< The location where the user is employed

        public int IdLocation { get; set; } //!< The location id

        public int IdUser { get; set; }  //!< The user's id

        public string Username { get; set; }  //!< The username

        public List<TimesheetActivityViewDto> TimesheetActivities { get; set; } //!<List of TimesheetActivities

    }//CLASS TimesheetViewDto

}//NAMESPACE WebApi.Dtos

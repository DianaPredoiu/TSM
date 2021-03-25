/*********************************************************************************
 * \file 
 * 
 * TimesheetView.cs file contains the TimsheetView class, which is included in 
 * Entities namespace.
 * 
 ********************************************************************************/


//namespaces used in TimesheetView class
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApi.Helpers;
//list of namespaces

namespace WebApi.Entities
{
    
    /*******************************************************
     * 
     * \class
     * 
     * TimesheetView class is used to represent some 
     * information collected from multiple tables as the 
     * result of queries. 
     * 
     ******************************************************/
    public class TimesheetView
    {
        [Key]
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

        public List<TimesheetActivityView> TimesheetActivities { get; set; } //!<List of TimesheetActivities

    }//CLASS TimesheetView

}//NAMESPACE WebApi.Entities

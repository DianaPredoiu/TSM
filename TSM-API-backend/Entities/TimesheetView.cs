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
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime Date { get; set; }  //!< The date of the timesheet

        //[JsonConverter(typeof(JsonDateConverter))]
        //public DateTime StartTime { get; set; }  //!< The time a user started working of the specified date

        //[JsonConverter(typeof(JsonDateConverter))]
        //public DateTime EndTime { get; set; }  //!< The time a user stopped working of the specified date

        //public TimeSpan BreakTime { get; set; }   //!< The time span of the break in the work day

        //public string Location { get; set; }  //!< The location the user is employed

        public string Project { get; set; }   //!< The project the user is working on

        public TimeSpan WorkedHours { get; set; }   //!< The time span a user worked on the specified project in the specified date

        public string Comments { get; set; }   //!< The comments a user had on the work from specified date,specified project

        public int IdUser { get; set; }  //!< The user's id

        public string Username { get; set; }  //!< The username

        public int IdTeam { get; set; } //!< The id of the user's team

        public int IdManager { get; set; } //!< The id of the user's manager

        public int IdProject { get; set; } //!< The id of the specified project

        [System.ComponentModel.DataAnnotations.Key]
        public int IdTimesheetView { get; set; }

    }//CLASS TimesheetView

}//NAMESPACE WebApi.Entities

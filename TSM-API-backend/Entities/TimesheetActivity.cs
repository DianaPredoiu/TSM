/*********************************************************************************
 * \file 
 * 
 * TimesheetActivity.cs file contains the TimsheetActivity class, which is included 
 * in Entities namespace.
 * 
 ********************************************************************************/

//list of namespaces usd in TimesheetActivity class
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//list of namespaces

namespace WebApi.Entities
{
    [Table("TimesheetActivity")]

    /*******************************************************
     * 
     * \class
     * 
     * TimesheetActivity class is used to represent the data
     * from the Timesheet Activity table from database.
     * 
     ******************************************************/
    public class TimesheetActivity
    {
        [Required]
        [Key]
        public int IdTimesheetActivity { get; set; }  //!< The id of the timesheet activity

        [Required]
        [ForeignKey("Timesheets")]
        [Column(Order = 1)]
        public int IdTimesheet { get; set; }  //!< The timesheet a user worked on (id of the timesheet)

        [Required]
        [ForeignKey("Projects")]
        [Column(Order = 4)]
        public int IdProject { get; set; }  //!< The project a user worked on (id of the project)

        [StringLength(100)]
        public string Comments { get; set; }  //!< The comments a user had on the work from specified date,specified project

        [Required]
        public TimeSpan WorkedHours { get; set; } //!< The number of hours a user spent to work on a project on a specified date

    }//CLASS TimesheetActivity

}//NAMESPACE WebApi.Entities

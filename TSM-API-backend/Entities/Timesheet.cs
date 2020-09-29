/*********************************************************************************
 * \file 
 * 
 * Timesheet.cs file contains the Timsheet class, which is included 
 * in Entities namespace.
 * 
 ********************************************************************************/


//list of namespaces used in Timesheet class
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//list of namespaces

namespace WebApi.Entities
{
    [Table("Timesheet")]

    /*******************************************************
     * 
     * \class
     * 
     * TimesheetActivity class is used to represent the data
     * from the Timesheet Activity table from database.
     * 
     ******************************************************/
    public class Timesheet
    {
        [Required]
        [Key]
        [Column(Order = 1)]
        public int IdTimesheet { get; set; }  //!< The id of the timesheet

        [Required]
        [ForeignKey("Locations")]
        [Column(Order = 5)]
        public int IdLocation { get; set; }  //!< The users's location (id of the location)

        [Required]
        [ForeignKey("Users")]
        [Column(Order = 0)]
        public int IdUser { get; set; }  //!< The user's id

        [Required]
        public DateTime Date { get; set; } //!< The date of the work day

        [Required]
        public DateTime StartTime { get; set; }  //!< The staring time of the work day

        [Required]
        public DateTime EndTime { get; set; } //!< The enting time of the work day

        [Required]
        public TimeSpan BreakTime { get; set; } //!< The timespan of the break in the work day

    }//CLASS Timesheet

}//NAMESPACE WebApi.Entities

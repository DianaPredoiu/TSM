using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    [Table("TimesheetActivity")]
    public class TimesheetActivity
    {
        [Required]
        [Key]
        public int IdTimesheetActivity { get; set; }

        [Required]
        [ForeignKey("Timesheets")]
        [Column(Order = 1)]
        public int IdTimesheet { get; set; }

        [Required]
        [ForeignKey("Projects")]
        [Column(Order = 4)]
        public int IdProject { get; set; }

        [StringLength(100)]
        public string Comments { get; set; }

        [Required]
        public TimeSpan WorkedHours { get; set; }

    }//CLASS TiemsheetActivity

}//NAMESPACE WebApi.Entities

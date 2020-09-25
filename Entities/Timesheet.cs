using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebApi.Entities
{
    [Table("Timesheet")]
    public class Timesheet
    {
        [Required]
        [Key]
        [Column(Order = 1)]
        public int IdTimesheet { get; set; }

        [Required]
        [ForeignKey("Locations")]
        [Column(Order = 5)]
        public int IdLocation { get; set; }

        [Required]
        [ForeignKey("Users")]
        [Column(Order = 0)]
        public int IdUser { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public TimeSpan BreakTime { get; set; }

    }//CLASS Timesheet

}//NAMESPACE WebApi.Entities

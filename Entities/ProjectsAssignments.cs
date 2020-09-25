using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    [Table("ProjectAssignments")]
    public class ProjectAssignments
    {
        [Required]
        [Key]
        [Column(Order = 4)]
        public int IdProject { get; set; }

        [Required]
        [ForeignKey("Users")]
        [Column(Order = 0)]
        public int IdUser { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}

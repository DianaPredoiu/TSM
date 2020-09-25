using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebApi.Entities
{
    [Table("Projects")]
    public class Project
    {
        [Required]
        [Key]
        [Column(Order = 4)]
        public int IdProject { get; set; }

        [Required]
        [StringLength(50)]
        public string ProjectName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public bool IsActive { get; set; }

    }//CLASS Project
    
}//NAMESPACE WebApi.Entities

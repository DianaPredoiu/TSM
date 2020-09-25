using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebApi.Entities
{
    [Table("ProjectManagers")]
    public class ProjectManager
    {
        [Key]
        [Required]
        public int IdProjectManager { get; set; }

        [Required]
        [ForeignKey("Projects")]
        [Column(Order = 4)]
        public int IdProject { get; set; }

        [Required]
        [ForeignKey("Users")]
        [Column(Order = 0)]
        public int IdUser { get; set; }

    }//CLASS ProjectManager

}//NAMESPACE WebApi.Entities

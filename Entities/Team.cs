using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    [Table("Teams")]
    public class Team
    {
        [Required]
        [Key]
        [Column(Order = 2)]
        public int IdTeam { get; set; }

        [StringLength(25)]
        [Required]
        public string TeamName { get; set; }

    }//CLASS Team

}//NAMESPACE WebApi.Entities

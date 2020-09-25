using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    [Table("Roles")]
    public class Role
    {
        [Required]
        [Key]
        [Column(Order = 3)]
        public int IdRole { get; set; }

        [Required]
        [StringLength(25)]
        public string RoleName { get; set; }

    }//CLASS Role

}//NAMESPACE WebApi.Entities

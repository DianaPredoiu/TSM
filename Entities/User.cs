using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        public int IdUser { get; set; }

        [Required]
        [ForeignKey("Roles")]
        [Column(Order = 3)]
        public int IdRole { get; set; }

        [Required]
        [ForeignKey("Teams")]
        [Column(Order = 2)]
        public int IdTeam {get;set;}

        [Required]
        [StringLength(25)]
        public string Username { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public bool IsAdmin { get; set; }

    }//CLASS User

}//NAMESPACE WebApi.Entities



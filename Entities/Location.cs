using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebApi.Entities
{
    [Table("Locations")]
    public class Location
    {
        [Required]
        [Key]
        [Column(Order = 5)]
        public int IdLocation { get; set; }

        [Required]
        [StringLength(25)]
        public string LocationName { get; set; }

    }//CLASS Location

}//NAMESPACE WebApi.Entities

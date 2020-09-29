/*********************************************************************************
 * \file 
 * 
 * Project.cs file contains the Project class, which is included 
 * in Entities namespace.
 * 
 ********************************************************************************/

//list of namespaces used in Location class
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//list of namespaces

namespace WebApi.Entities
{
    [Table("Locations")]
    /*******************************************************
     * 
     * \class
     * 
     * Location class is used to represent the data
     * from the Locations table from database.
     * 
     ******************************************************/
    public class Location
    {
        [Required]
        [Key]
        [Column(Order = 5)]
        public int IdLocation { get; set; }  //!< The id of the location

        [Required]
        [StringLength(25)]
        public string LocationName { get; set; }  //!< The name of the location

    }//CLASS Location

}//NAMESPACE WebApi.Entities

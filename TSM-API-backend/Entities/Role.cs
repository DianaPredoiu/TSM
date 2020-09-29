/*********************************************************************************
 * \file 
 * 
 * Role.cs file contains the Role class, which is included 
 * in Entities namespace.
 * 
 ********************************************************************************/

//list of the namespace used in Role class
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//list of namespaces

namespace WebApi.Entities
{
    [Table("Roles")]

    /*******************************************************
     * 
     * \class
     * 
     * Role class is used to represent the data
     * from the Roles table from database.
     * 
     ******************************************************/
    public class Role
    {
        [Required]
        [Key]
        [Column(Order = 3)]
        public int IdRole { get; set; }  //!< The id of the role

        [Required]
        [StringLength(25)]
        public string RoleName { get; set; }  //!< The id name of the role

    }//CLASS Role

}//NAMESPACE WebApi.Entities

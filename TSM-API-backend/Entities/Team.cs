/*********************************************************************************
 * \file 
 * 
 * Team.cs file contains the Team class, which is included 
 * in Entities namespace.
 * 
 ********************************************************************************/

//list of namespaces used in Team class
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//list of namespaces

namespace WebApi.Entities
{
    [Table("Teams")]

    /*******************************************************
     * 
     * \class
     * 
     * Team class is used to represent the data
     * from the Teams table from database.
     * 
     ******************************************************/
    public class Team
    {
        [Required]
        [Key]
        [Column(Order = 2)]
        public int IdTeam { get; set; }  //!< The id of the team

        [StringLength(25)]
        [Required]
        public string TeamName { get; set; }  //!< The team name

    }//CLASS Team

}//NAMESPACE WebApi.Entities

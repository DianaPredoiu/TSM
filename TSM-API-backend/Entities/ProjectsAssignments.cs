/*********************************************************************************
 * \file 
 * 
 * Role.cs file contains the Role class, which is included 
 * in Entities namespace.
 * 
 ********************************************************************************/

//list of namespaces included in ProjectAssignments class
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//list of namespaces

namespace WebApi.Entities
{
    [Table("ProjectAssignments")]
    /*******************************************************
     * 
     * \class
     * 
     * ProjectsAssignments class is used to represent the data
     * from the ProjectAssignments table from database.
     * 
     ******************************************************/
    public class ProjectAssignments
    {
        [Required]
        [Key]
        [Column(Order = 4)]
        public int IdProject { get; set; }  //!< The id of the project

        [Required]
        [ForeignKey("Users")]
        [Column(Order = 0)]
        public int IdUser { get; set; } //!< The id of the user

        [Required]
        public DateTime StartDate { get; set; } //!< The starting date of the project

        [Required]
        public DateTime EndDate { get; set; }  //!< The ending date of the project

        [Required]
        public bool IsActive { get; set; } //!< Specifies if the project is active or not
    }
}

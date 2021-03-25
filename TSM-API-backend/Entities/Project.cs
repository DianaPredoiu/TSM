/*********************************************************************************
 * \file 
 * 
 * Project.cs file contains the Project class, which is included 
 * in Entities namespace.
 * 
 ********************************************************************************/

//list of namespaces used in ProjectClass
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//list of namespaces

namespace WebApi.Entities
{
    [Table("Projects")]
    /*******************************************************
     * 
     * \class
     * 
     * Projects class is used to represent the data
     *  from the ProjectM table from database.
     * 
     ******************************************************/
    public class Project
    {
        [Required]
        [Key]
        [Column(Order = 4)]
        public int IdProject { get; set; } //!< The id of the project

        [Required]
        [StringLength(50)]
        public string ProjectName { get; set; } //!< The name of the project

        [Required]
        public DateTime StartDate { get; set; }  //!< The starting date of the project

        [Required]
        public DateTime EndDate { get; set; } //!< The ending date of the project

        [Required]
        public bool IsActive { get; set; } //!< Specifies if the project is active or not

    }//CLASS Project
    
}//NAMESPACE WebApi.Entities

/*********************************************************************************
 * \file 
 * 
 * ProjectManager.cs file contains the ProjectManager class, which is included 
 * in Entities namespace.
 * 
 ********************************************************************************/

//list of namespaces used in ProjectManager class
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//list of namespaces

namespace WebApi.Entities
{
    [Table("ProjectManagers")]
    /*******************************************************
     * 
     * \class
     * 
     * ProjectManager class is used to represent the data
     * from the ProjectManagers table from database.
     * 
     ******************************************************/
    public class ProjectManager
    {
        [Key]
        [Required]
        public int IdProjectManager { get; set; } //!< The id of the project manager

        [Required]
        [ForeignKey("Projects")]
        [Column(Order = 4)]
        public int IdProject { get; set; } //!< The id of the project he is project manager on

        [Required]
        [ForeignKey("Users")]
        [Column(Order = 0)]
        public int IdUser { get; set; } //!< The id of the user which is project manager

    }//CLASS ProjectManager

}//NAMESPACE WebApi.Entities

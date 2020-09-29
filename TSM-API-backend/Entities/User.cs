/*********************************************************************************
 * \file 
 * 
 * User.cs file contains the User class, which is included in Entities namespace
 * 
 ********************************************************************************/

//namespaces used in UserClass
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//list of namespaces


namespace WebApi
{
    /***********************************************************
     * 
     * \namespace
     * 
     * Entities namespace is included in WebApi namespace and
     * contains all the classes thet reprezent a model for Sql
     * database tables.
     * 
     **********************************************************/
    namespace Entities
    {
        //using annotations
        [Table("Users")]

        /*******************************************************
         * 
         * \class
         * 
         * User class is a model that represents Users table
         * from TSM Sql database
         * 
         ******************************************************/
        public class User
        {
            [Key]
            [Column(Order = 0)]
            [Required]
            public int IdUser { get; set; } //!< The user's id

            [Required]
            [ForeignKey("Roles")]
            [Column(Order = 3)]
            public int IdRole { get; set; } //!< The user's role (id of the role)

            [Required]
            [ForeignKey("Teams")]
            [Column(Order = 2)]
            public int IdTeam { get; set; } //!< The user's team (id of the team)

            [Required]
            [StringLength(25)]
            public string Username { get; set; } //!< The username

            [Required]
            public byte[] PasswordHash { get; set; } //!< The the password hash (password is encrypted in the database)

            [Required]
            public byte[] PasswordSalt { get; set; } //!< The the password salt (password is encrypted in the database)

            [Required]
            [StringLength(50)]
            public string Email { get; set; } //!< The the user's email

            [Required]
            public bool IsActive { get; set; } //!< Specifies if the user is active or not

            [Required]
            public bool IsAdmin { get; set; } //!< Specifies if the user is admin or not

        }//CLASS User

    }//NAMESPACE Entities  

}//NAMESPACE WebApi



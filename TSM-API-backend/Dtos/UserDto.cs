/*********************************************************************************
 * \file 
 * 
 * UserDto.cs file contains the UserDto class, which is included in Dtos namespace
 * 
 ********************************************************************************/


namespace WebApi
{
    /***********************************************************
      * 
      * \namespace
      * 
      * Dtos namespace is included in WebApi namespace and
      * contains all the classes thet reprezent a data transfer 
      * object which will be transfered to the front-end app,or 
      * from the front-end to the API.
      * 
      **********************************************************/
    namespace Dtos
    {
        /*******************************************************
         * 
         * \class
         * 
         * UserDto class is a model that has the structure of the 
         * User class, except the Password which is not encrypted 
         * anymore, it is a simple string.
         * 
         * The properties of the class have the same decription 
         * as the ones in the User class.
         * 
         ******************************************************/
        public class UserDto
        {

            public int IdUser { get; set; }


            public int IdRole { get; set; }


            public int IdTeam { get; set; }


            public string Username { get; set; }


            public string Password { get; set; }


            public string Email { get; set; }


            public bool IsActive { get; set; }

            public bool IsAdmin { get; set; }

        }//CLASS UserDto

    }//NAMESPACE Dtos

}//NAMESPACE WebApi
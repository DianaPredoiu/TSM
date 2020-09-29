/*********************************************************************************
 * \file 
 * 
 * RoleDto.cs file contains the RoleDto class, which is 
 * included in Dtos namespace.
 * 
 ********************************************************************************/

namespace WebApi.Dtos
{
    /*******************************************************
     * 
     * \class
     * 
     * RoleDto class is a model that has the 
     * structure of the Role class.
     * 
     * The properties of the class have the same decription 
     * as the ones in the Role class.
     * 
     ******************************************************/
    public class RoleDto
    {
        public int IdRole { get; set; }

        public string RoleName { get; set; }

    }//CLASS RoleDto

}//NAMESPACE WebApi.Dtos

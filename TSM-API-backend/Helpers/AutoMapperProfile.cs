/*********************************************************************************
 * \file 
 * 
 * AutoMapperProfile.cs file contains the AutoMapperProfile class, which is 
 * included in Helpers namespace.
 * 
 ********************************************************************************/

//namespaces used in AutoMapperProfile class
using AutoMapper;
using WebApi.Dtos;
using WebApi.Entities;
//list of namespaces

namespace WebApi.Helpers
{
    /**********************************************************
     * 
     * \class
     * 
     * AutoMapperProfile class inherits Profile class that 
     * provides a named configuration for the mappinf process
     * 
     *********************************************************/
    public class AutoMapperProfile : Profile
    {
        /**********************************************************
         * 
         * AutoMapperProfile constructor creates maps for 
         * UserDto-User, and User-UserDto.
         * 
         *********************************************************/
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            

        }//CONSTRUCTOR

    }//CLASS AutoMapperprofile

}//NAMESPACE WebApi.Helpers
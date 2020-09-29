/*********************************************************************************
 * \file 
 * 
 * LocationDto.cs file contains the LocationDto class, which 
 * is included in Dtos namespace.
 * 
 ********************************************************************************/

namespace WebApi.Dtos
{
    /*******************************************************
     * 
     * \class
     * 
     * LocationDto class is a model that has the 
     * structure of the Location class.
     * 
     * The properties of the class have the same decription 
     * as the ones in the Location class.
     * 
     ******************************************************/
    public class LocationDto
    {
        public int IdLocation { get; set; }
        public string LocationName { get; set; }

    }//CLASS Location

}//NAMESPACE WebApi.Dtos

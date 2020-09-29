/*********************************************************************************
 * \file 
 * 
 * TeamDto.cs file contains the TeamDto class, which is 
 * included in Dtos namespace.
 * 
 ********************************************************************************/

namespace WebApi.Dtos
{
    /*******************************************************
     * 
     * \class
     * 
     * TeamDto class is a model that has the 
     * structure of the Team class.
     * 
     * The properties of the class have the same decription 
     * as the ones in the Team class.
     * 
     ******************************************************/
    public class TeamDto
    {
        public int IdTeam { get; set; }

        public string TeamName { get; set; }

    }//CLASS TeamDto

}//NAMESPACE WebApi.Dtos

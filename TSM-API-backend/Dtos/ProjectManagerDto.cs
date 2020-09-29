/*********************************************************************************
 * \file 
 * 
 * ProjectManagerDto.cs file contains the ProjectMangerDto class, which is 
 * included in Dtos namespace.
 * 
 ********************************************************************************/ 

namespace WebApi.Dtos
{
    /*******************************************************
     * 
     * \class
     * 
     * ProjectManagerDto class is a model that has the 
     * structure of the ProjectManager class.
     * 
     * The properties of the class have the same decription 
     * as the ones in the ProjectManager class.
     * 
     ******************************************************/
    public class ProjectManagerDto
    {
       
        public int IdProjectManager { get; set; }

      
        public int IdProject { get; set; }

      
        public int IdUser { get; set; }

    }//CLASS ProjectManagerDto

}//NAMESPACE WebApi.Dto

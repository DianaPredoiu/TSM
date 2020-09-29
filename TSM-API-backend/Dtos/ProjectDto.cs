/*********************************************************************************
 * \file 
 * 
 * ProjectDto.cs file contains the ProjectDto class, which is 
 * included in Dtos namespace.
 * 
 ********************************************************************************/

//list of namespaces used in ProjectDto class
using Newtonsoft.Json;
using System;
using WebApi.Helpers;
//list of namespaces

namespace WebApi.Dtos
{
    /*******************************************************
     * 
     * \class
     * 
     * ProjectDto class is a model that has the 
     * structure of the Project class.
     * 
     * The properties of the class have the same decription 
     * as the ones in the Project class.
     * 
     ******************************************************/
    public class ProjectDto
    {
        
        public int IdProject { get; set; }
       
        public string ProjectName { get; set; }

        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime StartDate { get; set; }

        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime EndDate { get; set; }
      
        public bool IsActive { get; set; }

    }//CLASS ProjectDto

}//NAMESPACE WebApi.Dtos

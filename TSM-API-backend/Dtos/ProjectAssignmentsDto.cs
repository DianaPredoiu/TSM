/*********************************************************************************
 * \file 
 * 
 * ProjectAssignmentsDto.cs file contains the ProjectAssignmentsDto class, which 
 * is included in Dtos namespace.
 * 
 ********************************************************************************/

using System;

namespace WebApi.Dtos
{
    /*******************************************************
     * 
     * \class
     * 
     * ProjectAssignmentsDto class is a model that has the 
     * structure of the ProjectAssignments class.
     * 
     * The properties of the class have the same decription 
     * as the ones in the ProjectAssignments class.
     * 
     ******************************************************/
    public class ProjectAssignmentsDto
    {
       
        public int IdProject { get; set; }

       
        public int IdUser { get; set; }

        
        public DateTime StartDate { get; set; }

      
        public DateTime EndDate { get; set; }

       
        public bool IsActive { get; set; }

    }//CLASS ProjectAssignmentsDto

}//NAMESPACE WebApi.Dtos

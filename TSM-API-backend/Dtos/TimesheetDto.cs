/*********************************************************************************
 * \file 
 * 
 * TimesheetDto.cs file contains the TimesheetDto class, which is included in Dtos 
 * namespace.
 * 
 ********************************************************************************/

using System;

namespace WebApi.Dtos
{
    /*******************************************************
     * 
     * \class
     * 
     * TimesheetDto class is a model that has the structure of 
     * the Timesheet class.
     * 
     * The properties of the class have the same decription 
     * as the ones in the Timesheet class.
     * 
     ******************************************************/
    public class TimesheetDto
    {
       
        public int IdTimesheet { get; set; }

        
        public int IdLocation { get; set; }

        public int IdUser { get; set; }

        
        public DateTime Date { get; set; }

      
        public DateTime StartTime { get; set; }

       
        public DateTime EndTime { get; set; }

        
        public TimeSpan BreakTime { get; set; }

    }//CLASS TimesheetDto

}//NAMESPACE WebApi.Dtos

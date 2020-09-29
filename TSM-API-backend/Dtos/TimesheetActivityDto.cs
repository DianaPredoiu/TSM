/*********************************************************************************
 * \file 
 * 
 * TimesheetActivityDto.cs file contains the TimesheetActivityDto class, which is 
 * included in Dtos namespace.
 * 
 ********************************************************************************/

using System;


namespace WebApi.Dtos
{
    /*******************************************************
     * 
     * \class
     * 
     * TimesheetActivityDto class is a model that has the 
     * structure of the TimesheetActivity class.
     * 
     * The properties of the class have the same decription 
     * as the ones in the TimesheetActivity class.
     * 
     ******************************************************/
    public class TimesheetActivityDto
    {
        public int IdTimesheetActivity { get; set; }

        
        public int IdTimesheet { get; set; }

       
        public int IdProject { get; set; }

        
        public string Comments { get; set; }

        
        public TimeSpan WorkedHours { get; set; }

    }//CLASS TimesheetActivityDto

}//NAMESPACE WebApi.Dtos

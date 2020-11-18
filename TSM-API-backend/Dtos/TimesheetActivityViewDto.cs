using System;

namespace WebApi.Dtos
{
    public class TimesheetActivityViewDto
    {
        public int IdTimesheetActivity { get; set; }  //!< The id of the timesheet activity

        public int IdTimesheet { get; set; }  //!< The timesheet a user worked on (id of the timesheet)

        public int IdProject { get; set; }  //!< The project a user worked on (id of the project)

        public string ProjectName { get; set; } //!< The project's name

        public string Comments { get; set; }  //!< The comments a user had on the work from specified date,specified project

        public TimeSpan WorkedHours { get; set; } //!< The number of hours a user spent to work on a project on a specified date
    }
}

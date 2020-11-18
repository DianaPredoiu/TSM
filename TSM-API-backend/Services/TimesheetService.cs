using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi
{
    public interface ITimesheetService
    {
        IEnumerable<Timesheet> GetAll();

        int Create(Timesheet timesheet);

        TimesheetUpdate GetTimesheetByDate(DateTime date, int IdUser);

    }//INTERFACE
    public class TimesheetService: ITimesheetService
    {
        private DataContext _context;

        //CONSTRUCTOR
        public TimesheetService(DataContext context)
        {
            _context = context;
        }     

        public IEnumerable<Timesheet> GetAll()
        {
            return _context.Timesheets;
        }

        public int Create(Timesheet timesheet)
        {
            _context.Timesheets.Add(timesheet);
            _context.SaveChanges();

            return timesheet.IdTimesheet;
        }

        public TimesheetUpdate GetTimesheetByDate(DateTime date,int IdUser)
        {
            var timesheets = _context.Timesheets;
            var timesheetActivities = _context.TimesheetActivities;
            var projects = _context.Projects;
            var locations = _context.Locations;
            var users = _context.Users;

            var timesheetUpdate=from ta in timesheetActivities
                                join t in timesheets on ta.IdTimesheet equals t.IdTimesheet
                                join l in locations on t.IdLocation equals l.IdLocation
                                join p in projects on ta.IdProject equals p.IdProject
                                join u in users on t.IdUser equals u.IdUser
                                where t.Date == date && u.IdUser == IdUser
                                select new
                                {
                                    Location = l.LocationName,
                                    Project = p.ProjectName,
                                    IdUser = t.IdUser,
                                    Date = t.Date,
                                    StartTime = t.StartTime,
                                    EndTime = t.EndTime,
                                    BreakTime = t.BreakTime,
                                    WorkedHours = ta.WorkedHours,
                                    Comments = ta.Comments,
                                    Username = u.Username,
                                    IdTimesheet=t.IdTimesheet,
                                    IdLocation=t.IdLocation,
                                    IdProject=ta.IdProject
                                };

            var tUpdate = timesheetUpdate.ToList();

            TimesheetUpdate update = new TimesheetUpdate();
            update.IdTimesheet = tUpdate[0].IdTimesheet;
            update.IdUser = tUpdate[0].IdUser;
            update.IdLocation = tUpdate[0].IdLocation;
            update.Date = tUpdate[0].Date;
            update.StartTime = tUpdate[0].StartTime;
            update.EndTime = tUpdate[0].EndTime;
            update.BreakTime = tUpdate[0].BreakTime;

            List<TimesheetActivity> timesheetActivity = new List<TimesheetActivity>();

            foreach (var t in timesheetUpdate)
            {
                TimesheetActivity activity = new TimesheetActivity();

                activity.IdProject = t.IdProject;
                activity.WorkedHours = t.WorkedHours;
                activity.Comments = t.Comments;

                timesheetActivity.Add(activity);
            }

            update.TimesheetActivities = timesheetActivity;

            return update;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi
{
    public interface ITimesheetService
    {

        IList<TimesheetView> GetFilteredTimesheet(TimesheetObj timsheetObj);

        IEnumerable<Timesheet> GetAll();

        int Create(Timesheet timesheet);

    }//INTERFACE
    public class TimesheetService: ITimesheetService
    {
        private DataContext _context;

        //CONSTRUCTOR
        public TimesheetService(DataContext context)
        {
            _context = context;
        }

        public IList<TimesheetView> GetFilteredTimesheet(TimesheetObj timsheetObj)
        {
            var timesheets = _context.Timesheets;
            var timesheetActivities = _context.TimesheetActivities;
            var projects = _context.Projects;
            var users = _context.Users;
            var projectManagers = _context.ProjectManagers;

            //var join1 = timesheetActivities.Join(timesheets,
            //                                     ta => ta.IdTimesheet,
            //                                     t => t.IdTimesheet,
            //                                     (ta, t) => new
            //                                     {
            //                                         ta.IdProject,
            //                                         ta.Comments,
            //                                         ta.WorkedHours,
            //                                         t.Date,
            //                                         t.IdUser

            //                                     }).Where(t => timsheetObj.Date != "nullDate" ? t.Date == DateTime.Parse( timsheetObj.Date ) :  t.Date >= DateTime.Now.AddDays(-7) );

            //var join2 = join1.Join(projects,
            //                       j => j.IdProject,
            //                       p => p.IdProject,
            //                       (j, p) => new
            //                       {
            //                           j.Date,
            //                           j.IdUser,
            //                           j.WorkedHours,
            //                           j.Comments,
            //                           p.ProjectName,
            //                           p.IdProject

            //                       }).Where(p => timsheetObj.IdProject != -1 ? p.IdProject == timsheetObj.IdProject : true);

            //var join3 = join2.Join(users,
            //                     j => j.IdUser,
            //                     u => u.IdUser,
            //                     (j, u) => new TimesheetView
            //                     {
            //                         Date = j.Date,
            //                         IdUser = j.IdUser,
            //                         WorkedHours = j.WorkedHours,
            //                         Comments = j.Comments,
            //                         Project = j.ProjectName,
            //                         IdProject = j.IdProject,
            //                         Username = u.Username,
            //                         IdTeam = u.IdTeam

            //                     }).Where(u => timsheetObj.IdUser != -1 ? u.IdUser == timsheetObj.IdUser : true);

            //if(timsheetObj.IdTeam != -1)
            //{
            //    return join3.Where(u => u.IdTeam == timsheetObj.IdUser).AsNoTracking().ToList();
            //}

            //if(timsheetObj.IdManager != -1)
            //{
            //    return join3.Join(projectManagers,
            //                      j => j.IdProject,
            //                      p => p.IdProject,
            //                      (j, p) => new TimesheetView
            //                      {
            //                          Date = j.Date,
            //                          IdUser = j.IdUser,
            //                          WorkedHours = j.WorkedHours,
            //                          Comments = j.Comments,
            //                          Project = j.Project,
            //                          IdProject = j.IdProject,
            //                          Username = j.Username,
            //                          IdTeam = j.IdTeam,
            //                          IdManager = p.IdUser

            //                      }).Where(m => m.IdManager == timsheetObj.IdManager).AsNoTracking().ToList();
            //}

            //else
            //{
            //    return join3.AsNoTracking().ToList();
            //}


            var timesheet = from t in timesheetActivities
                            join ts in timesheets on t.IdTimesheet equals ts.IdTimesheet
                            join p in projects on t.IdProject equals p.IdProject
                            join u in users on ts.IdUser equals u.IdUser
                            join pm in projectManagers on t.IdProject equals pm.IdProject
                            select new TimesheetView
                            {
                                Project = p.ProjectName,
                                IdUser = ts.IdUser,
                                Date = ts.Date,
                                WorkedHours = t.WorkedHours,
                                Comments = t.Comments,
                                Username = u.Username,
                                IdTeam = u.IdTeam,
                                IdManager = pm.IdUser,
                                IdProject = p.IdProject,
                            };


            return timesheet.AsQueryable().Where(GenerateFilter.GenerateTimesheetFilter(timsheetObj)).AsNoTracking().ToList();

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
    }
}

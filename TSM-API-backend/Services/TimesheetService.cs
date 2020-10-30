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

        ////filter by project and date for project manager 1
        //IEnumerable<TimesheetView> GetTimesheetByProjectDate_Manager(int IdProject,DateTime date);

        ////filter by project for project manager 2
        //IEnumerable<TimesheetView> GetTimesheetByProject_Manager(int IdProject);

        ////filter by date for project manager 3
        //IEnumerable<TimesheetView> GetTimesheetByDate_Manager(DateTime date,int IdManager);

        ////filter by user for project manager 4
        //IEnumerable<TimesheetView> GetTimesheetByUser_Manager(int IdUser, int IdManager);

        ////filter by user and date for project manager 5
        //IEnumerable<TimesheetView> GetTimesheetByUserDate_Manager(int IdUser,int IdManager,DateTime date);

        ////filter by project for team leader 1
        //IEnumerable<TimesheetView> GetTimesheetByProjectTeamLeader(int IdTeam, int IdProj);

        ////filter by project and date for team leader 2
        //IEnumerable<TimesheetView> GetTimesheetByProjectDateTeamLeader(int IdProject, int IdTeam, DateTime date);

        ////filter by date for team leader 3
        //IEnumerable<TimesheetView> GetTimesheetByDateTeamLeader(DateTime date,int IdTeam);

        ////filter by user for team leader 4
        //IEnumerable<TimesheetView> GetTimesheetByUserTeamLeader(int IdUser);

        ////filetr by user and date for team leader 5
        //IEnumerable<TimesheetView> GetTimesheetByUserDateTeamLeader(int IdUser,DateTime date);

        ////filter by project date and user for team leader 7
        //IEnumerable<TimesheetView> GetTimesheetByProjectUserDateTeamLeader(int IdProject, int IdUser,DateTime date);

        ////filter by project and user for team leader and project manager 6
        //IEnumerable<TimesheetView> GetTimesheetByProjectUserTeamLeader(int IdProject, int IdUser);

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

        //public IEnumerable<TimesheetView> GetTimesheetByProjectUserDateTeamLeader(int IdProject, int IdUser, DateTime date)
        //{
        //    var timesheets = _context.Timesheets;
        //    var timesheetActivities = _context.TimesheetActivities;
        //    var projects = _context.Projects;
        //    var locations = _context.Locations;
        //    var users = _context.Users;

        //    var timesheet = from t in timesheetActivities
        //                    join ts in timesheets on t.IdTimesheet equals ts.IdTimesheet
        //                    join l in locations on ts.IdLocation equals l.IdLocation
        //                    join p in projects on t.IdProject equals p.IdProject
        //                    join u in users on ts.IdUser equals u.IdUser
        //                    where u.IdUser == IdUser && p.IdProject == IdProject && ts.Date==date
        //                    select new TimesheetView
        //                    {
        //                        Location = l.LocationName,
        //                        Project = p.ProjectName,
        //                        IdUser = ts.IdUser,
        //                        Date = ts.Date,
        //                        StartTime = ts.StartTime,
        //                        EndTime = ts.EndTime,
        //                        BreakTime = ts.BreakTime,
        //                        WorkedHours = t.WorkedHours,
        //                        Comments = t.Comments,
        //                        Username = u.Username
        //                    };

        //    return timesheet;
        //}

        //public IEnumerable<TimesheetView> GetTimesheetByUserDate_Manager(int IdUser, int IdManager, DateTime date)
        //{
        //    var timesheets = _context.Timesheets;
        //    var timesheetActivities = _context.TimesheetActivities;
        //    var projects = _context.Projects;
        //    var locations = _context.Locations;
        //    var users = _context.Users;
        //    var projectManagers = _context.ProjectManagers;

        //    var timesheet = from t in timesheetActivities
        //                    join p in projects on t.IdProject equals p.IdProject
        //                    join pm in projectManagers on t.IdProject equals pm.IdProject
        //                    join ts in timesheets on t.IdTimesheet equals ts.IdTimesheet
        //                    join l in locations on ts.IdLocation equals l.IdLocation
        //                    join u in users on ts.IdUser equals u.IdUser
        //                    where ts.IdUser == IdUser && pm.IdUser == IdManager && ts.Date == date
        //                    select new TimesheetView
        //                    {
        //                        Location = l.LocationName,
        //                        Project = p.ProjectName,
        //                        IdUser = ts.IdUser,
        //                        Date = ts.Date,
        //                        StartTime = ts.StartTime,
        //                        EndTime = ts.EndTime,
        //                        BreakTime = ts.BreakTime,
        //                        WorkedHours = t.WorkedHours,
        //                        Comments = t.Comments,
        //                        Username = u.Username
        //                    };

        //    return timesheet;
        //}

        //public IEnumerable<TimesheetView> GetTimesheetByUser_Manager(int IdUser, int IdManager)
        //{
        //    var timesheets = _context.Timesheets;
        //    var timesheetActivities = _context.TimesheetActivities;
        //    var projects = _context.Projects;
        //    var locations = _context.Locations;
        //    var users = _context.Users;
        //    var projectManagers = _context.ProjectManagers;

        //    var timesheet = from t in timesheetActivities
        //                    join p in projects on t.IdProject equals p.IdProject
        //                    join pm in projectManagers on t.IdProject equals pm.IdProject
        //                    join ts in timesheets on t.IdTimesheet equals ts.IdTimesheet
        //                    join l in locations on ts.IdLocation equals l.IdLocation
        //                    join u in users on ts.IdUser equals u.IdUser
        //                    where ts.IdUser == IdUser && pm.IdUser == IdManager && ts.Date >= DateTime.Now.AddDays(-7)
        //                    select new TimesheetView
        //                    {
        //                        Location = l.LocationName,
        //                        Project = p.ProjectName,
        //                        IdUser = ts.IdUser,
        //                        Date = ts.Date,
        //                        StartTime = ts.StartTime,
        //                        EndTime = ts.EndTime,
        //                        BreakTime = ts.BreakTime,
        //                        WorkedHours = t.WorkedHours,
        //                        Comments = t.Comments,
        //                        Username = u.Username
        //                    };

        //    return timesheet;
        //}
        //public IEnumerable<TimesheetView> GetTimesheetByDate_Manager(DateTime date,int IdManager)
        //{
        //    var timesheets = _context.Timesheets;
        //    var timesheetActivities = _context.TimesheetActivities;
        //    var projects = _context.Projects;
        //    var locations = _context.Locations;
        //    var users = _context.Users;
        //    var projectManagers = _context.ProjectManagers;

        //    var timesheet = from t in timesheetActivities
        //                    join p in projects on t.IdProject equals p.IdProject
        //                    join pm in projectManagers on t.IdProject equals pm.IdProject
        //                    join ts in timesheets on t.IdTimesheet equals ts.IdTimesheet
        //                    join l in locations on ts.IdLocation equals l.IdLocation
        //                    join u in users on ts.IdUser equals u.IdUser
        //                    where ts.Date==date && pm.IdUser == IdManager
        //                    select new TimesheetView
        //                    {
        //                        Location = l.LocationName,
        //                        Project = p.ProjectName,
        //                        IdUser = ts.IdUser,
        //                        Date = ts.Date,
        //                        StartTime = ts.StartTime,
        //                        EndTime = ts.EndTime,
        //                        BreakTime = ts.BreakTime,
        //                        WorkedHours = t.WorkedHours,
        //                        Comments = t.Comments,
        //                        Username = u.Username
        //                    };

        //    return timesheet;
        //}

        //public IEnumerable<TimesheetView> GetTimesheetByProjectDate_Manager(int IdProject, DateTime date)
        //{
        //    var timesheets = _context.Timesheets;
        //    var timesheetActivities = _context.TimesheetActivities;
        //    var projects = _context.Projects;
        //    var locations = _context.Locations;
        //    var users = _context.Users;

        //    var timesheet = from t in timesheetActivities
        //                    join ts in timesheets on t.IdTimesheet equals ts.IdTimesheet
        //                    join l in locations on ts.IdLocation equals l.IdLocation
        //                    join p in projects on t.IdProject equals p.IdProject
        //                    join u in users on ts.IdUser equals u.IdUser
        //                    where p.IdProject == IdProject && ts.Date==date
        //                    select new TimesheetView
        //                    {
        //                        Location = l.LocationName,
        //                        Project = p.ProjectName,
        //                        IdUser = ts.IdUser,
        //                        Date = ts.Date,
        //                        StartTime = ts.StartTime,
        //                        EndTime = ts.EndTime,
        //                        BreakTime = ts.BreakTime,
        //                        WorkedHours = t.WorkedHours,
        //                        Comments = t.Comments,
        //                        Username = u.Username
        //                    };

        //    return timesheet;
        //}

        //public IEnumerable<TimesheetView> GetTimesheetByProject_Manager(int IdProject)
        //{
        //    var timesheets = _context.Timesheets;
        //    var timesheetActivities = _context.TimesheetActivities;
        //    var projects = _context.Projects;
        //    var locations = _context.Locations;
        //    var users = _context.Users;

        //    var timesheet = from t in timesheetActivities
        //                    join ts in timesheets on t.IdTimesheet equals ts.IdTimesheet
        //                    join l in locations on ts.IdLocation equals l.IdLocation
        //                    join p in projects on t.IdProject equals p.IdProject
        //                    join u in users on ts.IdUser equals u.IdUser
        //                    where p.IdProject == IdProject && ts.Date >= DateTime.Now.AddDays(-7)
        //                    select new TimesheetView
        //                    {
        //                        Location = l.LocationName,
        //                        Project = p.ProjectName,
        //                        IdUser = ts.IdUser,
        //                        Date = ts.Date,
        //                        StartTime = ts.StartTime,
        //                        EndTime = ts.EndTime,
        //                        BreakTime = ts.BreakTime,
        //                        WorkedHours = t.WorkedHours,
        //                        Comments = t.Comments,
        //                        Username = u.Username
        //                    };

        //    return timesheet;
        //}

        //public IEnumerable<TimesheetView> GetTimesheetByProjectUserTeamLeader(int IdProject, int IdUser)
        //{
        //    var timesheets = _context.Timesheets;
        //    var timesheetActivities = _context.TimesheetActivities;
        //    var projects = _context.Projects;
        //    var locations = _context.Locations;
        //    var users = _context.Users;

        //    var timesheet = from t in timesheetActivities
        //                    join ts in timesheets on t.IdTimesheet equals ts.IdTimesheet
        //                    join l in locations on ts.IdLocation equals l.IdLocation
        //                    join p in projects on t.IdProject equals p.IdProject
        //                    join u in users on ts.IdUser equals u.IdUser
        //                    where u.IdUser==IdUser && p.IdProject==IdProject
        //                    select new TimesheetView
        //                    {
        //                        Location = l.LocationName,
        //                        Project = p.ProjectName,
        //                        IdUser = ts.IdUser,
        //                        Date = ts.Date,
        //                        StartTime = ts.StartTime,
        //                        EndTime = ts.EndTime,
        //                        BreakTime = ts.BreakTime,
        //                        WorkedHours = t.WorkedHours,
        //                        Comments = t.Comments,
        //                        Username = u.Username
        //                    };

        //    return timesheet;
        //}

        //public IEnumerable<TimesheetView> GetTimesheetByProjectDateTeamLeader(int IdProject, int IdTeam, DateTime date)
        //{
        //    var timesheets = _context.Timesheets;
        //    var timesheetActivities = _context.TimesheetActivities;
        //    var projects = _context.Projects;
        //    var locations = _context.Locations;
        //    var users = _context.Users;

        //    var timesheet = from t in timesheetActivities
        //                    join ts in timesheets on t.IdTimesheet equals ts.IdTimesheet
        //                    join l in locations on ts.IdLocation equals l.IdLocation
        //                    join p in projects on t.IdProject equals p.IdProject
        //                    join u in users on ts.IdUser equals u.IdUser
        //                    where p.IdProject==IdProject && ts.Date == date && u.IdTeam==IdTeam
        //                    select new TimesheetView
        //                    {
        //                        Location = l.LocationName,
        //                        Project = p.ProjectName,
        //                        IdUser = ts.IdUser,
        //                        Date = ts.Date,
        //                        StartTime = ts.StartTime,
        //                        EndTime = ts.EndTime,
        //                        BreakTime = ts.BreakTime,
        //                        WorkedHours = t.WorkedHours,
        //                        Comments = t.Comments,
        //                        Username = u.Username
        //                    };

        //    return timesheet;
        //}

        //public IEnumerable<TimesheetView> GetTimesheetByUserDateTeamLeader(int IdUser, DateTime date)
        //{
        //    var timesheets = _context.Timesheets;
        //    var timesheetActivities = _context.TimesheetActivities;
        //    var projects = _context.Projects;
        //    var locations = _context.Locations;
        //    var users = _context.Users;

        //    var timesheet = from t in timesheetActivities
        //                    join ts in timesheets on t.IdTimesheet equals ts.IdTimesheet
        //                    join l in locations on ts.IdLocation equals l.IdLocation
        //                    join p in projects on t.IdProject equals p.IdProject
        //                    join u in users on ts.IdUser equals u.IdUser
        //                    where ts.IdUser == IdUser && ts.Date==date
        //                    select new TimesheetView
        //                    {
        //                        Location = l.LocationName,
        //                        Project = p.ProjectName,
        //                        IdUser = ts.IdUser,
        //                        Date = ts.Date,
        //                        StartTime = ts.StartTime,
        //                        EndTime = ts.EndTime,
        //                        BreakTime = ts.BreakTime,
        //                        WorkedHours = t.WorkedHours,
        //                        Comments = t.Comments,
        //                        Username = u.Username
        //                    };

        //    return timesheet;
        //}

        //public IEnumerable<TimesheetView> GetTimesheetByUserTeamLeader(int IdUser)
        //{
        //    var timesheets = _context.Timesheets;
        //    var timesheetActivities = _context.TimesheetActivities;
        //    var projects = _context.Projects;
        //    var locations = _context.Locations;
        //    var users = _context.Users;

        //    var timesheet = from t in timesheetActivities
        //                    join ts in timesheets on t.IdTimesheet equals ts.IdTimesheet
        //                    join l in locations on ts.IdLocation equals l.IdLocation
        //                    join p in projects on t.IdProject equals p.IdProject
        //                    join u in users on ts.IdUser equals u.IdUser
        //                    where ts.IdUser == IdUser && ts.Date >= DateTime.Now.AddDays(-7)
        //                    select new TimesheetView
        //                    {
        //                        Location = l.LocationName,
        //                        Project = p.ProjectName,
        //                        IdUser = ts.IdUser,
        //                        Date = ts.Date,
        //                        StartTime = ts.StartTime,
        //                        EndTime = ts.EndTime,
        //                        BreakTime = ts.BreakTime,
        //                        WorkedHours = t.WorkedHours,
        //                        Comments = t.Comments,
        //                        Username = u.Username
        //                    };

        //    return timesheet;
        //}

        //public IEnumerable<TimesheetView> GetTimesheetByProjectTeamLeader(int IdTeam, int IdProj)
        //{
        //    var timesheets = _context.Timesheets;
        //    var timesheetActivities = _context.TimesheetActivities;
        //    var projects = _context.Projects;
        //    var locations = _context.Locations;
        //    var users = _context.Users;

        //    var timesheet = from t in timesheetActivities
        //                    join ts in timesheets on t.IdTimesheet equals ts.IdTimesheet
        //                    join l in locations on ts.IdLocation equals l.IdLocation
        //                    join p in projects on t.IdProject equals p.IdProject
        //                    join u in users on ts.IdUser equals u.IdUser
        //                    where p.IdProject == IdProj && u.IdTeam == IdTeam && ts.Date >= DateTime.Now.AddDays(-7)
        //                    select new TimesheetView
        //                    {
        //                        Location = l.LocationName,
        //                        Project = p.ProjectName,
        //                        IdUser = ts.IdUser,
        //                        Date = ts.Date,
        //                        StartTime = ts.StartTime,
        //                        EndTime = ts.EndTime,
        //                        BreakTime = ts.BreakTime,
        //                        WorkedHours = t.WorkedHours,
        //                        Comments = t.Comments,
        //                        Username = u.Username
        //                    };

        //    return timesheet;
        //}

        //public IEnumerable<TimesheetView> GetTimesheetByDateTeamLeader(DateTime date,int IdTeam)
        //{
        //    var timesheets = _context.Timesheets;
        //    var timesheetActivities = _context.TimesheetActivities;
        //    var projects = _context.Projects;
        //    var locations = _context.Locations;
        //    var users = _context.Users;

        //    var timesheet = from t in timesheetActivities
        //                    join ts in timesheets on t.IdTimesheet equals ts.IdTimesheet
        //                    join l in locations on ts.IdLocation equals l.IdLocation
        //                    join p in projects on t.IdProject equals p.IdProject
        //                    join u in users on ts.IdUser equals u.IdUser
        //                    where ts.Date==date && u.IdTeam==IdTeam
        //                    select new TimesheetView
        //                    {
        //                        Location = l.LocationName,
        //                        Project = p.ProjectName,
        //                        IdUser = ts.IdUser,
        //                        Date = ts.Date,
        //                        StartTime = ts.StartTime,
        //                        EndTime = ts.EndTime,
        //                        BreakTime = ts.BreakTime,
        //                        WorkedHours = t.WorkedHours,
        //                        Comments = t.Comments,
        //                        Username=u.Username
        //                    };

        //    return timesheet;
        //}

        public IList<TimesheetView> GetFilteredTimesheet(TimesheetObj timsheetObj)
        {

            var  timesheets = _context.Timesheets;
            var timesheetActivities = _context.TimesheetActivities;
            var projects = _context.Projects;
            var users = _context.Users;
            var projectManagers = _context.ProjectManagers;

            var join1 = timesheetActivities.Join(timesheets,
                                                 ta => ta.IdTimesheet,
                                                 t => t.IdTimesheet,
                                                 (ta, t) => new
                                                 {
                                                     ta.IdProject,
                                                     ta.Comments,
                                                     ta.WorkedHours,
                                                     t.Date,
                                                     t.IdUser

                                                 }).Where(t => timsheetObj.Date != "nullDate" ? t.Date == DateTime.Parse( timsheetObj.Date ) :  t.Date >= DateTime.Now.AddDays(-7) );

            var join2 = join1.Join(projects,
                                   j => j.IdProject,
                                   p => p.IdProject,
                                   (j, p) => new
                                   {
                                       j.Date,
                                       j.IdUser,
                                       j.WorkedHours,
                                       j.Comments,
                                       p.ProjectName,
                                       p.IdProject

                                   }).Where(p => timsheetObj.IdProject != -1 ? p.IdProject == timsheetObj.IdProject : true);

            var join3 = join2.Join(users,
                                 j => j.IdUser,
                                 u => u.IdUser,
                                 (j, u) => new TimesheetView
                                 {
                                     Date = j.Date,
                                     IdUser = j.IdUser,
                                     WorkedHours = j.WorkedHours,
                                     Comments = j.Comments,
                                     Project = j.ProjectName,
                                     IdProject = j.IdProject,
                                     Username = u.Username,
                                     IdTeam = u.IdTeam

                                 }).Where(u => timsheetObj.IdUser != -1 ? u.IdUser == timsheetObj.IdUser : true);

            if(timsheetObj.IdTeam != -1)
            {
                return join3.Where(u => u.IdTeam == timsheetObj.IdUser).AsNoTracking().ToList();
            }

            if(timsheetObj.IdManager != -1)
            {
                return join3.Join(projectManagers,
                                  j => j.IdProject,
                                  p => p.IdProject,
                                  (j, p) => new TimesheetView
                                  {
                                      Date = j.Date,
                                      IdUser = j.IdUser,
                                      WorkedHours = j.WorkedHours,
                                      Comments = j.Comments,
                                      Project = j.Project,
                                      IdProject = j.IdProject,
                                      Username = j.Username,
                                      IdTeam = j.IdTeam,
                                      IdManager = p.IdUser

                                  }).Where(m => m.IdManager == timsheetObj.IdManager).AsNoTracking().ToList();
            }

            else
            {
                return join3.AsNoTracking().ToList();
            }

            //var timesheet = from t in timesheetActivities
            //                join ts in timesheets on t.IdTimesheet equals ts.IdTimesheet
            //                join l in locations on ts.IdLocation equals l.IdLocation
            //                join p in projects on t.IdProject equals p.IdProject
            //                join u in users on ts.IdUser equals u.IdUser
            //                join pm in projectManagers on t.IdProject equals pm.IdProject
            //                select new TimesheetView
            //                {
            //                    Location = l.LocationName,
            //                    Project = p.ProjectName,
            //                    IdUser = ts.IdUser,
            //                    Date = ts.Date,
            //                    StartTime = ts.StartTime,
            //                    EndTime = ts.EndTime,
            //                    BreakTime = ts.BreakTime,
            //                    WorkedHours = t.WorkedHours,
            //                    Comments = t.Comments,
            //                    Username = u.Username,
            //                    IdTeam = u.IdTeam,
            //                    IdManager = pm.IdUser
            //                };


            //var timesheetFilter = GenerateFilter.GenerateTimesheetFilter(timsheetObj);

            //return timesheet.AsQueryable().Where(GenerateFilter.GenerateTimesheetFilter(timsheetObj)).ToList();          

            //var timesheetAct = _context.TimesheetActivities.Include(t => t.IdTimesheet);

            //var timesheetList = Generic<TimesheetActivity>.GetList(timesheetFilter, t => t.IdProject, t => t.IdTeam, t => t.IdUser, t => t.Date);

            //return timesheetList;
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

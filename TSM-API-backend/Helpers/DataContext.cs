using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.Helpers
{
    public class DataContext : DbContext
    {
        
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<TimesheetActivity> TimesheetActivities { get; set; }

        public DbSet<Timesheet> Timesheets { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<ProjectManager> ProjectManagers { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<ProjectAssignments> ProjectAssignments { get; set; }


    }//CLASS DataContext

}//NAMESPACE WebApi.Helpers
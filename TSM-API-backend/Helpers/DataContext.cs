/*********************************************************************************
 * \file 
 * 
 * DataContext.cs file contains the DataContext class, which is included in 
 * Helpers namespace.
 * 
 ********************************************************************************/

//namespaces used in DataContext Class
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
//list of namespaces

namespace WebApi
{
    /***************************************************************
     * 
     * \namespace
     * 
     * Helpers namespace includes classes that are used in models, 
     * services or controllers but do not fit into those categories
     * 
     ***************************************************************/
    namespace Helpers
    {
        /*******************************************************
         * 
         * \class
         * 
         * DataContext class inherits DbContext class and saves 
         * instances of the database tables.
         * 
         ******************************************************/
        public class DataContext : DbContext
        {
            /*******************************************************
             * 
             * Constructor of DataContext class, initializes new 
             *  instance of DbContext using options.
             * 
             ******************************************************/
            public DataContext(DbContextOptions<DataContext> options) : base(options) { }

            public DbSet<User> Users { get; set; } //!< Gets data from Users table

            public DbSet<TimesheetActivity> TimesheetActivities { get; set; }  //!< Gets data from TimesheetActivities table

            public DbSet<Timesheet> Timesheets { get; set; }  //!< Gets data from Timesheets table

            public DbSet<Team> Teams { get; set; } //!< Gets data from Teams table

            public DbSet<Role> Roles { get; set; }  //!< Gets data from Roles table

            public DbSet<ProjectManager> ProjectManagers { get; set; }  //!< Gets data from ProjectManages table

            public DbSet<Project> Projects { get; set; }  //!< Gets data from Projects table

            public DbSet<Location> Locations { get; set; }  //!< Gets data from Location table

            public DbSet<ProjectAssignments> ProjectAssignments { get; set; }  //!< Gets data from ProjectAssignments table


        }//CLASS DataContext

    }//NAMEPSACE Helpers

}//NAMESPACE WebApi.Helpers
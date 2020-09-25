using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{

    public interface ITimesheetActivityService
    {
        TimesheetActivity Create(TimesheetActivity timesheetActivity);

    }//INTERFACE IUserService
    public class TimesheetActivityService: ITimesheetActivityService
    {

        private DataContext _context;

        //CONSTRUCTOR
        public TimesheetActivityService(DataContext context)
        {
            _context = context;
        }
        public TimesheetActivity Create(TimesheetActivity timesheetActivity)
        {
            _context.TimesheetActivities.Add(timesheetActivity);
            _context.SaveChanges();

            return timesheetActivity;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{

    public interface IProjectAssignmentsService
    {
      
        IEnumerable<Project> GetByUserId(int userId);
     

    }//INTERFACE IUserService
    public class ProjectAssignmentsService : IProjectAssignmentsService
    {
        private DataContext _context;

        //CONSTRUCTOR
        public ProjectAssignmentsService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Project> GetByUserId(int userId)
        {
            var assign = _context.ProjectAssignments;
            var projects = _context.Projects;

            var result = from a in assign
                         join p in projects on a.IdProject equals p.IdProject
                         where a.IdUser==userId
                         select new Project
                         {
                             IdProject = p.IdProject,
                             ProjectName = p.ProjectName,
                             StartDate = p.StartDate,
                             EndDate = p.EndDate,
                             IsActive = p.IsActive
                         };

            return result;
        }
    }
}

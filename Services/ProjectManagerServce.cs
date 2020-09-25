using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public interface IProjectManagerService
    {
        IEnumerable<Project> GetProjectsByManagerId(int id);//by user id

    }//INTERFACE IUserService
    public class ProjectManagerService : IProjectManagerService
    {
        private DataContext _context;

        //CONSTRUCTOR
        public ProjectManagerService(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<Project> GetProjectsByManagerId(int id)
        {
            var projectManagers = _context.ProjectManagers;
            var projects = _context.Projects;

            var projectsByManagerId = from pm in projectManagers
                                      join p in projects on pm.IdProject equals p.IdProject
                                      where pm.IdUser == id
                                      select new Project
                                      {
                                          IdProject=p.IdProject,
                                          ProjectName=p.ProjectName,
                                          StartDate = p.StartDate,
                                          EndDate = p.EndDate,
                                          IsActive = p.IsActive
                                      };

            return projectsByManagerId;
        }
    }
}

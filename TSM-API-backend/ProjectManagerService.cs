using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi
{
    public interface IProjectManagerService
    {
        IEnumerable<Project> GetProjects(int id);

    }//INTERFACE IUserService
    public class ProjectManagerService:IProjectManagerService
    {
        private DataContext _context;

        //CONSTRUCTOR
        public ProjectManagerService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Project> GetProjectsById(int id)
        {
            var projects = _context.Projects;
            var projectManager = _context.ProjectManagers;

            var projectList = from pm in projectManager
                              join p in projects on pm.IdProject equals p.IdProject
                              select new Project
                              {
                                  IdProject = p.IdProject,
                                  ProjectName = p.ProjectName,
                                  StartDate = p.StartDate,
                                  EndDate = p.EndDate,
                                  IsActive = p.IsActive
                              };

            return projectList;
        }
    }
}

/*********************************************************************************
 * \file 
 * 
 * UserService.cs file contains the UserService class, which is included in 
 * Services namespace.
 * 
 ********************************************************************************/

/*********************************************************************************
 * ProjectsController.cs file contains the ProjectsController class, which is 
 * included in Controllers namespace.
 * 
 ********************************************************************************/

//list of namespaces used in ProjectsController class
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using WebApi.Dtos;
using WebApi.Helpers;
//list of namespaces

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]//MAIN ROUTE

    /*******************************************************
     * 
     * \class
     * 
     * ProjectsController class contains all the methods
     * that contain http requests and dto mapping for Projects 
     * related data.
     * 
     ******************************************************/
    public class ProjectsController:ControllerBase
    {
        //ATTRIBUTES
        private IProjectService _projectService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        /*************************************************************
         * 
         * ProjectsController Constructor injects 
         * ProjectService,the Mapper class and AppSettings class.
         * 
         ************************************************************/
        public ProjectsController(
            IProjectService projectService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _projectService = projectService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        //GETALL PROJECTS
        [HttpGet]
        public IActionResult GetAll()
        {
            var projects = _projectService.GetAll();
            var projectDtos = _mapper.Map<IList<ProjectDto>>(projects);
            return Ok(projectDtos);
        }

        [AllowAnonymous]
        //GETALL PROJECTS
        [HttpGet("getByUserId/{id}")]

        /***********************************************************************************
        * 
        * GetAll method:
        *     + Return type: IActionResult.
        *     + @param id: first argument,type int-represents the UserId.
        *     + It is used to get all the projects that a user works on.
        *     + HttpGet request.
        * 
        ***********************************************************************************/
        public IActionResult GetAll(int id)
        {
            var projects = _projectService.GetById(id);
            var projectDtos = _mapper.Map<IList<ProjectDto>>(projects);
            return Ok(projectDtos);
        }
    }
}

/*********************************************************************************
 * \file 
 * 
 * UserService.cs file contains the UserService class, which is included in 
 * Services namespace.
 * 
 ********************************************************************************/

/*********************************************************************************
 * ProjectAssignmentsController.cs file contains the ProjectAssignmentsController
 * class, which is included in Controllers namespace.
 * 
 ********************************************************************************/

//list of namespaces used in ProjectAssignmentsController class
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using WebApi.Dtos;
using WebApi.Helpers;
using WebApi.Services;
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
     * ProjectAssignmentsController class contains all the methods
     * that contain http requests and dto mapping for 
     * ProjectAssignments related data.
     * 
     ******************************************************/
    public class ProjectAssignmentsController : ControllerBase
    {
        //ATTRIBUTES
        private IProjectAssignmentsService _projectAssignmentsService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        /*************************************************************
        * 
        * ProjectAssignmentsController Constructor injects 
        * ProjectAssignmentsService,the Mapper class and AppSettings class.
        * 
        ************************************************************/
        public ProjectAssignmentsController(
            IProjectAssignmentsService projectAssignmentsService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _projectAssignmentsService = projectAssignmentsService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        //GETALL PROJECTS
        [HttpGet("{id}")]
        /***********************************************************************************
         * 
         * GetAll method:
         *     + Return type: IActionResult.
         *     + @param id: first argument,type int.
         *     + It is used to get all the projectsAssignments that are registered in 
         *       the ProjectAssignments table.
         *     + HttpGet request.
         * 
         ***********************************************************************************/
        public IActionResult GetAll(int id)
        {
            var projects = _projectAssignmentsService.GetByUserId(id);
            var projectDtos = _mapper.Map<IList<ProjectDto>>(projects);
            return Ok(projectDtos);
        }

    }
}

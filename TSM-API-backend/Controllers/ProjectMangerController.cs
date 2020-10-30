/*********************************************************************************
 * \file 
 * 
 * LocationController.cs file contains the LocationController class, which is 
 * included in Controllers namespace.
 * 
 ********************************************************************************/


//list of namespaces used in LocationController class
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
     * LocationController class contains all the methods
     * that contain http requests and dto mapping.
     * 
     ******************************************************/
    public class ProjectManagerController : ControllerBase
    {
        private IProjectManagerService _projectManagerService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        /*************************************************************
         * 
         * ProjectManagerController Constructor injects 
         * ProjectManagerService,the Mapper class and AppSettings class.
         * 
         ************************************************************/
        public ProjectManagerController(
            IProjectManagerService projectManagerService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _projectManagerService = projectManagerService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpGet("getProjectsByProjectManagerId/{id}")]
        /***********************************************************************************
         * 
         * GetAllProjectsByManagerId method:
         *     + Return type: IActionResult.
         *     + @param id: first argument,type int-represents the UserId of the Manager.
         *     + It is used to get all the projects that register under a specified manager.
         *     + HttpGet request.
         * 
         ***********************************************************************************/
        public IActionResult GetAllProjectsByManagerId(int id)
        {
            var projects = _projectManagerService.GetProjectsByManagerId(id);
            var projectDtos = _mapper.Map<IList<ProjectDto>>(projects);
            return Ok(projectDtos);
        }
    }
}

/*********************************************************************************
 * \file 
 * 
 * UserService.cs file contains the UserService class, which is included in 
 * Services namespace.
 * 
 ********************************************************************************/

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Helpers;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]//MAIN ROUTE
    public class ProjectManagerController : ControllerBase
    {
        //ATTRIBUTES
        private IProjectManagerService _projectManagerService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        //CONSTRUCTOR
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
        //GETALL PROJECTS
        [HttpGet("getProjectsByProjectManagerId/{id}")]
        public IActionResult GetAllProjectsByManagerId(int id)
        {
            var projects = _projectManagerService.GetProjectsByManagerId(id);
            var projectDtos = _mapper.Map<IList<ProjectDto>>(projects);
            return Ok(projectDtos);
        }
    }
}

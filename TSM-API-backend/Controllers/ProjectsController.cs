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

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]//MAIN ROUTE
    public class ProjectsController:ControllerBase
    {
        //ATTRIBUTES
        private IProjectService _projectService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        //CONSTRUCTOR
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
        public IActionResult GetAll(int id)
        {
            var projects = _projectService.GetById(id);
            var projectDtos = _mapper.Map<IList<ProjectDto>>(projects);
            return Ok(projectDtos);
        }
    }
}

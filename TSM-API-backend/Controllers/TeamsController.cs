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
    public class TeamsController : ControllerBase
    {
        //ATTRIBUTES
        private ITeamService _teamService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        //CONSTRUCTOR
        public TeamsController(
            ITeamService teamService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _teamService = teamService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        //GETALL USERS
        [HttpGet]
        public IActionResult GetAll()
        {
            var teams = _teamService.GetAll();
            var teamsDtos = _mapper.Map<IList<TeamDto>>(teams);
            return Ok(teamsDtos);
        }

    }
}

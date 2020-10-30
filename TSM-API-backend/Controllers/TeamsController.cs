/*********************************************************************************
 * \file 
 * 
 * TeamsController.cs file contains the TeamsController class, which is 
 * included in Controllers namespace.
 * 
 ********************************************************************************/

//list of namespaces used in TeamsController class
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
     * TeamsController class contains all the methods
     * that contain http requests and dto mapping for Teams 
     * related data.
     * 
     ******************************************************/
    public class TeamsController : ControllerBase
    {
        private ITeamService _teamService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        /*************************************************************
         * 
         * TeamsController Constructor injects 
         * TeamsService,the Mapper class and AppSettings class.
         * 
         ************************************************************/
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
        [HttpGet]
       /***********************************************************************************
       * 
       * GetAll method:
       *     + Return type: IActionResult.
       *     + It is used to get all the teams that are registered in the Teams table.
       *     + HttpGet request.
       * 
       ***********************************************************************************/
        public IActionResult GetAll()
        {
            var teams = _teamService.GetAll();
            var teamsDtos = _mapper.Map<IList<TeamDto>>(teams);
            return Ok(teamsDtos);
        }

    }
}

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using WebApi.Dtos;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]//MAIN ROUTE
    public class TimesheetsController : ControllerBase
    {
        //ATTRIBUTES
        private ITimesheetService _timesheetService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        //CONSTRUCTOR
        public TimesheetsController(
            ITimesheetService timesheetService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _timesheetService = timesheetService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        //GET TIMESHEET BY DATE FOR TEAM LEADER
        [HttpGet("byDateTeamLeader/{idTeam}/{date}")]//ROUTE
        public IActionResult GetByDateTeamLeader(int idTeam ,DateTime date)
        {
            var timesheet = _timesheetService.GetTimesheetByDateTeamLeader(date, idTeam);
            
            var timesheetDto = _mapper.Map<IList<TimesheetViewDto>>(timesheet);
            return Ok(timesheetDto);   
        }

        [AllowAnonymous]
        //GET TIMESHEET BY PROJECT FOR TEAM LEADER
        [HttpGet("byProjectTeamLeader/{idTeam}/{IdProject}")]//ROUTE
        public IActionResult GetByProjectTeamLeader(int idTeam, int IdProject)
        {
            var timesheet = _timesheetService.GetTimesheetByProjectTeamLeader(idTeam, IdProject);

            var timesheetDto = _mapper.Map<IList<TimesheetViewDto>>(timesheet);
            return Ok(timesheetDto);
        }

        [AllowAnonymous]
        //GET TIMESHEET BY USER FOR TEAM LEADER
        [HttpGet("byUserTeamLeader/{IdUser}")]//ROUTE
        public IActionResult GetByUserTeamLeader(int IdUser)
        {
            var timesheet = _timesheetService.GetTimesheetByUserTeamLeader(IdUser);

            var timesheetDto = _mapper.Map<IList<TimesheetViewDto>>(timesheet);
            return Ok(timesheetDto);
        }

        [AllowAnonymous]
        //GET TIMESHEET BY USER AND DATE FOR TEAM LEADER
        [HttpGet("byUserDateTeamLeader/{IdUser}/{date}")]//ROUTE
        public IActionResult GetByUserDateTeamLeader(int IdUser,DateTime date)
        {
            var timesheet = _timesheetService.GetTimesheetByUserDateTeamLeader(IdUser,date);

            var timesheetDto = _mapper.Map<IList<TimesheetViewDto>>(timesheet);
            return Ok(timesheetDto);
        }

        [AllowAnonymous]
        //GET TIMESHEET BY PROJECT AND DATE FOR TEAMLEADER
        [HttpGet("byProjectDateTeamLeader/{IdProject}/{IdTeam}/{date}")]//ROUTE
        public IActionResult GetByUserDateTeamLeader(int IdProject,int IdTeam, DateTime date)
        {
            var timesheet = _timesheetService.GetTimesheetByProjectDateTeamLeader(IdProject,IdTeam,date);

            var timesheetDto = _mapper.Map<IList<TimesheetViewDto>>(timesheet);
            return Ok(timesheetDto);
        }

        [AllowAnonymous]
        //GET TIMESHEET BY USER AND PROJECT FOR TEAM LEADER AND PROJECT MANAGER
        [HttpGet("byProjectUserTeamLeader/{IdProject}/{IdUser}")]//ROUTE
        public IActionResult GetByUserDateTeamLeader(int IdProject, int IdUser)
        {
            var timesheet = _timesheetService.GetTimesheetByProjectUserTeamLeader(IdProject,IdUser);

            var timesheetDto = _mapper.Map<IList<TimesheetViewDto>>(timesheet);
            return Ok(timesheetDto);
        }

        [AllowAnonymous]
        //GET TIMESHEET BY USER DATE AND PROJECT FOR TEAM LEADER AND PROJECT MANAGER
        [HttpGet("byProjectUserDateTeamLeader/{IdProject}/{IdUser}/{date}")]//ROUTE
        public IActionResult GetByUserDateProjectTeamLeader(int IdProject, int IdUser, DateTime date)
        {
            var timesheet = _timesheetService.GetTimesheetByProjectUserDateTeamLeader(IdProject, IdUser,date);

            var timesheetDto = _mapper.Map<IList<TimesheetViewDto>>(timesheet);
            return Ok(timesheetDto);
        }

        [AllowAnonymous]
        //GET TIMESHEET BY PROJECT DATE FOR PROJECT MANAGER
        [HttpGet("byProjectDate_ProjectManager/{IdProject}/{date}")]//ROUTE
        public IActionResult GetByProjectDate_Manager(int IdProject, DateTime date)
        {
            var timesheet = _timesheetService.GetTimesheetByProjectDate_Manager(IdProject, date);

            var timesheetDto = _mapper.Map<IList<TimesheetViewDto>>(timesheet);
            return Ok(timesheetDto);
        }

        [AllowAnonymous]
        //GET TIMESHEET BY PROJECT FOR PROJECT MANAGER
        [HttpGet("byProject_ProjectManager/{IdProject}")]//ROUTE
        public IActionResult GetByProject_Manager(int IdProject)
        {
            var timesheet = _timesheetService.GetTimesheetByProject_Manager(IdProject);

            var timesheetDto = _mapper.Map<IList<TimesheetViewDto>>(timesheet);
            return Ok(timesheetDto);
        }

        [AllowAnonymous]
        //GET TIMESHEET BY DATE FOR PROJECT MANAGER
        [HttpGet("byDate_ProjectManager/{date}/{IdManager}")]//ROUTE
        public IActionResult GetByDate_Manager(DateTime date, int IdManager)
        {
            var timesheet = _timesheetService.GetTimesheetByDate_Manager(date,IdManager);

            var timesheetDto = _mapper.Map<IList<TimesheetViewDto>>(timesheet);
            return Ok(timesheetDto);
        }

        [AllowAnonymous]
        //GET TIMESHEET BY USER FOR PROJECT MANAGER
        [HttpGet("byUser_projectManager/{IdManager}/{IdUser}")]//ROUTE
        public IActionResult GetByUser_Manager(int IdManager, int IdUser)
        {
            var timesheet = _timesheetService.GetTimesheetByUser_Manager(IdUser, IdManager);

            var timesheetDto = _mapper.Map<IList<TimesheetViewDto>>(timesheet);
            return Ok(timesheetDto);
        }


        [AllowAnonymous]
        //GET TIMESHEET BY USER DATE FOR PROJECT MANAGER
        [HttpGet("byUserDate_ProjectManager/{IdManager}/{IdUser}/{date}")]//ROUTE
        public IActionResult GetByUserDate_Manager(int IdManager, int IdUser,DateTime date)
        {
            var timesheet = _timesheetService.GetTimesheetByUserDate_Manager(IdUser, IdManager, date);

            var timesheetDto = _mapper.Map<IList<TimesheetViewDto>>(timesheet);
            return Ok(timesheetDto);
        }

        [AllowAnonymous]
        //GETALL TIMESHEETS
        [HttpGet]
        public IActionResult GetAll()
        {
            
            var users = _timesheetService.GetAll();
            var userDtos = _mapper.Map<IList<TimesheetDto>>(users);
       
            return Ok(userDtos);
        }

        [AllowAnonymous]
        //GETALL USERS
        [HttpPost("create")]
        public IActionResult Create([FromBody]TimesheetDto timesheetDto)
        {
            // map dto to entity
            var timesheet = _mapper.Map<Timesheet>(timesheetDto);

            try
            {
                // save 
                _timesheetService.Create(timesheet);
                return Ok(timesheet.IdTimesheet);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

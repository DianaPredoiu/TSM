using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
        //GETALL TIMESHEETS
        [HttpPost("filter")]
        public IActionResult GetByFilter([FromBody]TimesheetObjDto timesheetObjDto)
        {
            var timesheetObj = _mapper.Map<TimesheetObj>(timesheetObjDto);
            var timesheet = TimesheetFilter.GetFilteredTimesheet(timesheetObj);
            var timesheetDtos = _mapper.Map<IList<TimesheetViewDto>>(timesheet);

            return Ok(timesheetDtos);
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
        //create timesheet
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

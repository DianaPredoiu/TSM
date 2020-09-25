using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]//MAIN ROUTE
    public class TimesheetActivityController : ControllerBase
    {
        //ATTRIBUTES
        private ITimesheetActivityService _timesheetActivityService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        //CONSTRUCTOR
        public TimesheetActivityController(
            ITimesheetActivityService timesheetActivityService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _timesheetActivityService = timesheetActivityService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        //GETALL USERS
        [HttpPost("create")]
        public IActionResult Create([FromBody]TimesheetActivityDto timesheetActivityDto)
        {
            // map dto to entity

            var timesheetActivity = _mapper.Map<TimesheetActivity>(timesheetActivityDto);


            try
            {
                // save 
                _timesheetActivityService.Create(timesheetActivity);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        
    }
}

/*********************************************************************************
 * \file 
 * 
 * UserService.cs file contains the UserService class, which is included in 
 * Services namespace.
 * 
 ********************************************************************************/

/*********************************************************************************
 * TimesheetActivityController.cs file contains the TimesheetActivityController
 * class, which is included in Controllers namespace.
 * 
 ********************************************************************************/

//list of namespaces used in TimesheetActivityController class
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Dtos;
using WebApi.Entities;
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
    * TimesheetActivityController class contains all the methods
    * that contain http requests and dto mapping for 
    * TimesheetActivity related data.
    * 
    ******************************************************/
    public class TimesheetActivityController : ControllerBase
    {
        //ATTRIBUTES
        private ITimesheetActivityService _timesheetActivityService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        /*************************************************************
        * 
        * TimesheetActivityController Constructor injects 
        * TimesheetActivityService,the Mapper class and AppSettings class.
        * 
        ************************************************************/
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
        /***********************************************************************************
        * 
        * Create method:
        *     + Return type: IActionResult.
        *     + @param timesheetActivityDto: first argument,type TimesheetActivityDto.
        *     + It is used to add a new timehseet activity to the table.
        *     + HttpGet HttpPost.
        * 
        ***********************************************************************************/
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

        
    }//CLASS 

}//NAMESPACE

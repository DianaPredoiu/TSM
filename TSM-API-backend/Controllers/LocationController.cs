/*********************************************************************************
 * \file 
 * 
 * UserService.cs file contains the UserService class, which is included in 
 * Services namespace.
 * 
 ********************************************************************************/

/*********************************************************************************
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
    //attributes
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
    public class LocationController : ControllerBase
    {
        
        private ILocationService _locationService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        /*************************************************************
         * 
         * LocationController Constructor injects LocationService,
         * the Mapper class and AppSettings class.
         * 
         ************************************************************/
        public LocationController(
            ILocationService locationService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _locationService = locationService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }


        /***********************************************************************************
         * 
         * GetAll method:
         *     + Return type: IActionResult.
         *     + It is used to get all the locations registered in the Location Table.
         *     + HttpGet request.
         * 
         ***********************************************************************************/
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            var locations = _locationService.GetAll();
            var locationDtos = _mapper.Map<IList<LocationDto>>(locations);
            return Ok(locationDtos);
        }
    }
}

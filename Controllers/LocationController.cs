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
    public class LocationController : ControllerBase
    {
        //ATTRIBUTES
        private ILocationService _locationService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        //CONSTRUCTOR
        public LocationController(
            ILocationService locationService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _locationService = locationService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        //GETALL PROJECTS
        [HttpGet]
        public IActionResult GetAll()
        {
            var locations = _locationService.GetAll();
            var locationDtos = _mapper.Map<IList<LocationDto>>(locations);
            return Ok(locationDtos);
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using WebApi.Dtos;
using WebApi.Helpers;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]//MAIN ROUTE
    public class UserHobbiesViewController : ControllerBase
    {
        //ATTRIBUTES
        private IQueryService _queryService;
        private IMapper _mapper;

        //CONSTRUCTOR
        public UserHobbiesViewController(
            IQueryService queryService,
            IMapper mapper)
        {
            _queryService = queryService;
            _mapper = mapper;
        }

        //GET ALL HOBBIES FOR SPECIFIED USER
        [AllowAnonymous]
        [HttpGet("listhobbies/{id}")]//ROUTE
        public IActionResult ListAllHobbies(int Id)
        {
            var _userHobbieslist = _queryService.getList(Id);         
            var userHobbiesListDto = _mapper.Map<IList<HobbyDto>>(_userHobbieslist);
            return Ok(userHobbiesListDto);
        }

        //GET A LIST OF HOBBIES THE USER CAN CHOOSE FROM
        [AllowAnonymous]
        [HttpGet("listoptions/{id}")]//ROUTE
        public IActionResult ListAllOptions(int Id)
        {
            var _userHobbieslist = _queryService.getOptions(Id);
            var userHobbiesListDto = _mapper.Map<IList<HobbyDto>>(_userHobbieslist);
            return Ok(userHobbiesListDto);
        }

        [AllowAnonymous]
        [HttpGet("listusers/{id}")]//ROUTE
        public IActionResult ListAllUsers(int Id)
        {
            var _userList = _queryService.getUsers(Id);
            var userListDto = _mapper.Map<IList<UserDto>>(_userList);
            return Ok(userListDto);
        }

    }//CLASS UserHobbiesView

}//NAMESPACE WebApi.Controllers
                                      
     
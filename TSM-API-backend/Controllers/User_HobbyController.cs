using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using WebApi.Dtos;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]//MAIN ROUTE
    [ApiController]

    public class User_HobbyController : ControllerBase
    {
        //ATTRIBUTES
        private IUser_HobbyService _userhobbyService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        //CONSTRUCTOR
        public User_HobbyController(IMapper mapper, IOptions<AppSettings> appSettings, IUser_HobbyService user_hobbyService)
        {
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _userhobbyService = user_hobbyService;
        }

        //GETALL REQUEST
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            var user_hobbies = _userhobbyService.GetAll();
            var user_hobbyDtos = _mapper.Map<IList<User_HobbyDto>>(user_hobbies);
            return Ok(user_hobbyDtos);
        }

        //GET USER HOBBY RQUEST
        [AllowAnonymous]
        [HttpGet("{id}")]//ROUTE
        public IActionResult GetUserHobby(int id)
        {

            var user_hobby = _userhobbyService.Get(id);
            var user_hobbyDto = _mapper.Map<User_HobbyDto>(user_hobby);
            return Ok(user_hobbyDto);

        }

        //ADD REQUEST
        [AllowAnonymous]
        [HttpPost("add")]//ROUTE
        public IActionResult Add([FromBody]User_HobbyDto user_hobbyDto)
        {
            // map dto to entity
            var user_hobby = _mapper.Map<User_Hobby>(user_hobbyDto);
            if (user_hobbyDto == null)
                return BadRequest();
            try
            {
                // save 
                _userhobbyService.Add(user_hobby);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        //UPDATE REQUEST
        [AllowAnonymous]
        [HttpPut("{id}")]//ROUTE
        public IActionResult Update(int id, [FromBody]User_HobbyDto user_hobbyDto)
        {
            // map dto to entity and set id
            var user_hobby = _mapper.Map<User_Hobby>(user_hobbyDto);
            user_hobby.Id = id;

            try
            {
                // save 
                _userhobbyService.Update(user_hobby);
                return Ok(user_hobby);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        //DELETE REQUEST
        [AllowAnonymous]
        [HttpDelete("{id}")]//ROUTE
        public IActionResult DeleteHobby(int id)
        {
            _userhobbyService.Delete(id);
            return Ok();
        }

    }//CLASS User_HobbyController

}//NAMESPACE WebApi.Controllers

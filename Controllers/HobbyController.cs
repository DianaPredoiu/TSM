using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

    public class HobbyController : ControllerBase
    {
        //ATTRIBUTES
        private IHobbyService _hobbyService;
        private IMapper _mapper;

        //CONSTRUCTOR
        public HobbyController(IMapper mapper, IHobbyService hobbyService)
        {
            _mapper = mapper;
            _hobbyService = hobbyService;
        }

        //GETALL REQUEST
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            var hobbies = _hobbyService.GetAll();
            var hobbyDtos = _mapper.Map<IList<HobbyDto>>(hobbies);
            return Ok(hobbyDtos);
        }

        //GET HOBBY REQUEST
        [AllowAnonymous]
        [HttpGet("{id}")]//ROUTE
        public IActionResult GetHobby(int id)
        {

            var hobby = _hobbyService.Get(id);
            var hobbyDto = _mapper.Map<HobbyDto>(hobby);
            return Ok(hobbyDto);

        }

        //ADD REQUEST
        [AllowAnonymous]
        [HttpPost("add")]//ROUTE
        public IActionResult Add([FromBody]HobbyDto hobbyDto)
        {
            // map dto to entity
            var hobby = _mapper.Map<Hobby>(hobbyDto);

            try
            {
                // save 
                _hobbyService.Add(hobby);
                return Ok(hobby);
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
        public IActionResult Update(int id, [FromBody]HobbyDto hobbyDto)
        {
            // map dto to entity and set id
            var hobby = _mapper.Map<Hobby>(hobbyDto);
            hobby.IdHobby = id;

            try
            {
                // save 
                _hobbyService.Update(hobby);
                return Ok();
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
            _hobbyService.Delete(id);
            return Ok();
        }

    }//CLASS HobbyController

}//NAMESPACE WebApi.Controllers

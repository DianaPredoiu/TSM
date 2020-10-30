/*********************************************************************************
 * \file 
 * 
 * UserService.cs file contains the UserService class, which is included in 
 * Services namespace.
 * 
 ********************************************************************************/

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using WebApi.Helpers;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using WebApi.Services;
using WebApi.Dtos;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]//MAIN ROUTE
    public class UsersController : ControllerBase
    {
        //ATTRIBUTES
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        //CONSTRUCTOR
        public UsersController(
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        //Authentication function
        [AllowAnonymous]
        [HttpPost("authenticate")]//ROUTE
        public IActionResult Authenticate([FromBody]UserDto userDto)
        {
            var user = _userService.Authenticate(userDto.Username, userDto.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            if(!user.IsActive)
                return BadRequest(new { message = "User is inactive" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.IdUser.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info (without password) and token to store client side
            return Ok(new {
                IdUser = user.IdUser,
                Username = user.Username,
                Email = user.Email,
                IdTeam = user.IdTeam,
                IdRole=user.IdRole,
                IsActive=user.IsActive,
                IsAdmin=user.IsAdmin,
                Token = tokenString
            });
        }

        //Register function
        [AllowAnonymous]
        [HttpPost("register")]//ROUTE
        public IActionResult Register([FromBody]UserDto userDto)
        {
            // map dto to entity
            var user = _mapper.Map<User>(userDto);

            try 
            {
                // save 
                _userService.Create(user, userDto.Password);
                return Ok();
            } 
            catch(AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        //GETALL USERS
        [HttpGet]
        public IActionResult GetAll()
        {
            var users =  _userService.GetAll();
            var userDtos = _mapper.Map<IList<UserDto>>(users);
            return Ok(userDtos);
        }

        [AllowAnonymous]
        //GET USER BY ID
        [HttpGet("{id}")]//ROUTE
        public IActionResult GetById(int id)
        {
            var user =  _userService.GetById(id);
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [AllowAnonymous]
        //UPDATE USER
        [HttpPut("{id}")]//ROUTE
        public IActionResult Update(int id, [FromBody]UserDto userDto)
        {
            // map dto to entity and set id
            var user = _mapper.Map<User>(userDto);
            user.IdUser = id;

            try 
            {
                // save 
                _userService.Update(user, userDto.Password);
                return Ok();
            } 
            catch(AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        //DELETE USER
        [HttpDelete("{id}")]//ROUTE
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("verifyPassword")]
        public IActionResult VerifyPassord([FromBody]UserDto userDto)
        {
            Console.WriteLine(userDto);
            if (_userService.VerifyPassword(userDto.Password, userDto.IdUser))
                return Ok();
            else
                return BadRequest(new { message = "Password is incorrect" });
        }

        [AllowAnonymous]
        //GET USER BY ID
        [HttpGet("getTeamMembers/{id}")]//ROUTE
        public IActionResult GetTeamMembersByTeamId(int id)
        {
            var users = _userService.GetAllByIdTeam(id);
            var userDto = _mapper.Map<IList<UserDto>>(users);
            return Ok(userDto);
        }

        [AllowAnonymous]
        //GET USER BY ID
        [HttpGet("getProjectMembers/{id}")]//ROUTE
        public IActionResult GetProjectMembersByProjectId(int id)
        {
            var users = _userService.GetAllByIdProject(id);
            var userDto = _mapper.Map<IList<UserDto>>(users);
            return Ok(userDto);
        }

    }//CLASS UsersController

}//NAMESPACE WebApi.Controllers

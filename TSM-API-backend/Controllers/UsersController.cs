/*********************************************************************************
 * \file 
 * 
 * UsersController.cs file contains the UsersController class, which is 
 * included in Controllers namespace.
 * 
 ********************************************************************************/

//list of namespaces used in UsersController class
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
//list of namespaces

/*******************************************************************************************************************************************************
 * 
 * \namespace
 * 
 * Controllers namespace is included in WebApi namespace and contains all the classes that use http requests and dto mapping
 * made for each specific model.The concept of dependency injection is used in every class of this namespace.
 * 
 * For more details go to: https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-aspnet-mvc3/cs/adding-a-controller
 * 
 *******************************************************************************************************************************************************/
namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]//MAIN ROUTE

    /*******************************************************
     * 
     * \class
     * 
     * UsersController class contains all the methods
     * that contain http requests and dto mapping for Users 
     * related data.
     * 
     ******************************************************/
    public class UsersController : ControllerBase
    {
        //ATTRIBUTES
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        /*************************************************************
         * 
         * UsersController Constructor injects 
         * UsersService,the Mapper class and AppSettings class.
         * 
         ************************************************************/
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
        /***********************************************************************************
         * 
         * Authenticate method:
         *     + Return type: IActionResult.
         *     + @param userDto: first argument,type UserDto.
         *     + It is used to validate if a user exists in the database and the password is 
         *       correct.
         *     + HttpPost request.
         * 
         ***********************************************************************************/
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

        /***********************************************************************************
         * 
         * Register method:
         *     + Return type: IActionResult.
         *     + @param userDto: first argument,type UserDto.
         *     + It is used to add a user to the database.
         *     + HttpPost request.
         * 
         ***********************************************************************************/
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
        [HttpGet]
        /***********************************************************************************
         * 
         * GetAll method:
         *     + Return type: IActionResult.
         *     + It is used to get all the users that are registered in the Users table.
         *     + HttpGet request.
         * 
         ***********************************************************************************/
        public IActionResult GetAll()
        {
            var users =  _userService.GetAll();
            var userDtos = _mapper.Map<IList<UserDto>>(users);
            return Ok(userDtos);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]//ROUTE
        /***********************************************************************************
         * 
         * GetById method:
         *     + Return type: IActionResult.
         *     + @param id: first argument,type int.
         *     + It is used to get all the users specified by an IdUser.
         *     + HttpGet request.
         * 
         ***********************************************************************************/
        public IActionResult GetById(int id)
        {
            var user =  _userService.GetById(id);
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [AllowAnonymous]
        //UPDATE USER
        [HttpPut("{id}")]//ROUTE
        /***********************************************************************************
         * 
         * Update method:
         *     + Return type: IActionResult.
         *     + @param userDto: first argument,type UserDto.
         *     + It is used to update a specified user.
         *     + HttpPut request.
         * 
         ***********************************************************************************/
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
        /***********************************************************************************
         * 
         * Delete method:
         *     + Return type: IActionResult.
         *     + @param id: first argument,type int.
         *     + It is used to delete a specified user.
         *     + HttpDelete request.
         * 
         ***********************************************************************************/
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("verifyPassword")]
        /***********************************************************************************
         * 
         * VerifyPassord method:
         *     + Return type: IActionResult.
         *     + @param userDto: first argument,type UserDto.
         *     + It is used to verifiy if the password is correct.
         *     + HttpPost request.
         * 
         ***********************************************************************************/
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
        /***********************************************************************************
         * 
         * GetTeamMembersByTeamId method:
         *     + Return type: IActionResult.
         *     + @param id: first argument,type int.
         *     + It is used to get all the users that are registered in the same team-by id.
         *     + HttpGet request.
         * 
         ***********************************************************************************/
        [HttpGet("getTeamMembers/{id}")]//ROUTE
        public IActionResult GetTeamMembersByTeamId(int id)
        {
            var users = _userService.GetAllByIdTeam(id);
            var userDto = _mapper.Map<IList<UserDto>>(users);
            return Ok(userDto);
        }

        [AllowAnonymous]
        [HttpGet("getProjectMembers/{id}")]//ROUTE
        /***********************************************************************************
         * 
         * GetProjectMembersByProjectId method:
         *     + Return type: IActionResult.
         *     + @param id: first argument,type int.
         *     + It is used to get all the project members for a project.
         *     + HttpGet request.
         * 
         ***********************************************************************************/
        public IActionResult GetProjectMembersByProjectId(int id)
        {
            var users = _userService.GetAllByIdProject(id);
            var userDto = _mapper.Map<IList<UserDto>>(users);
            return Ok(userDto);
        }

    }//CLASS UsersController

}//NAMESPACE WebApi.Controllers

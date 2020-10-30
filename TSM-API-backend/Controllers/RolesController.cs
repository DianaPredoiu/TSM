/*********************************************************************************
 * \file 
 * 
 * RolesController.cs file contains the RolesController class, which is 
 * included in Controllers namespace.
 * 
 ********************************************************************************/

//list of namespaces used in RolesController class
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
    [Authorize]
    [ApiController]
    [Route("[controller]")]//MAIN ROUTE

    /*******************************************************
     * 
     * \class
     * 
     * RolesController class contains all the methods
     * that contain http requests and dto mapping for Roles 
     * related data.
     * 
     ******************************************************/
    public class RolesController : ControllerBase
    {
        //ATTRIBUTES
        private IRoleService _roleService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        /*************************************************************
          * 
          * RolesController Constructor injects 
          * RolesService,the Mapper class and AppSettings class.
          * 
          ************************************************************/
        public RolesController(
            IRoleService roleService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _roleService = roleService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        //GETALL USERS
        [HttpGet]

        /***********************************************************************************
        * 
        * GetAll method:
        *     + Return type: IActionResult.
        *     + It is used to get all the roles that are regitered in the Roles table.
        *     + HttpGet request.
        * 
        ***********************************************************************************/
        public IActionResult GetAll()
        {
            var roles = _roleService.GetAll();
            var rolesDtos = _mapper.Map<IList<RoleDto>>(roles);
            return Ok(rolesDtos);
        }

    }
}

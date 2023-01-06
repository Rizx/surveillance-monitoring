using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Repositories;
using API.Shared;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using API.Extentions;
using System.Net;
using System.Linq;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(
            UserRepository userRepository,
            IMapper mapper,
            ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// List User
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var entities = await _userRepository.GetList();
            var result = _mapper.Map<IEnumerable<UserListResponse>>(entities);
            return Ok(Result.Ok(result));
        }

        /// <summary>
        /// Get by id
        /// </summary>
        [HttpGet("id")]
        public async Task<IActionResult> Get([FromQuery] long id)
        {
            var entity = await _userRepository.Get(id);
            var result = _mapper.Map<UserResponse>(entity);
            if (result is null)
                return BadRequest(Result.Fail("Not Found"));
            return Ok(Result.Ok(result));
        }

        /// <summary>
        /// List User Role
        /// </summary>
        [HttpGet("roles")]
        public IActionResult GetRoles()
        {
            return Ok(Result.Ok(new []{"Administrator","Guest"}));
        }

        /// <summary>
        /// Register User
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserRequest request)
        {
            var entity = await _userRepository.GetByUsername(request.UserName);
            if (entity is not null)
            {
                return BadRequest(Result.Fail("User Registered"));
            }

            if(string.IsNullOrEmpty(request.Role))
                request.Role = "Administrator";

            var user = new User(
                id: 0,
                request.FullName,
                request.UserName,
                request.Password.Hashing(),
                request.Role,
                active: true);

            var result = await _userRepository.Add(user);

            if (result == 0)
                return BadRequest(Result.Fail("Regiter User Failed"));
            return Ok(Result.Ok("Register User Succed"));
        }

        /// <summary>
        /// Update User
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserRequest request)
        {
            var entity = await _userRepository.Get(request.Id);
            if (entity is null)
            {
                return BadRequest(Result.Fail("User Unregistered"));
            }

            entity.Changes(request.FullName, request.Role, request.Active);
            entity.ChangePassword(request.Password);

            var result = await _userRepository.Update(entity);

            if (result == 0)
                return BadRequest(Result.Fail("Update User Failed"));
            return Ok(Result.Ok("Update User Succed"));
        }
    }
}

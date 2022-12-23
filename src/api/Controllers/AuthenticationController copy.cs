using System.Threading.Tasks;
using API.Repositories;
using API.Shared;
using AutogateAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("api")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly UserRepository _userRepository;

        public AuthenticationController(
            UserRepository userRepository,
            ILogger<AuthenticationController> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpPost("[Action]")]
        public async Task<IActionResult> Authentication([FromBody] AuthenticationRequest request)
        {
            var user = await _userRepository.GetByUsername(request.Username);
            if(user == null)
                return BadRequest(Result.Fail("Username not registered"));
            else if(user.Password != request.Password)
                return BadRequest(Result.Fail("Password not match"));
            else
                return Ok(Result<Users>.Ok(user));
        }
    }
}

using System.Threading.Tasks;
using API.Repositories;
using API.Shared;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API.Extentions;
using AutoMapper;

namespace API.Controllers
{
    [ApiController]
    [Route("api")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly AuthenticationRepository _authRepository;
        private readonly IMapper _mapper;

        public AuthenticationController(
            AuthenticationRepository authRepository,
            IMapper mapper,
            ILogger<AuthenticationController> logger)
        {
            _authRepository = authRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("[Action]")]
        public async Task<IActionResult> Authentication([FromBody] AuthenticationRequest request)
        {
            var user = await _authRepository.GetByUsername(request.Username);
            if(user == null)
                return BadRequest(Result.Fail("User not registered"));

            if (!user.Active)
                return BadRequest(Result.Fail("Inactive User"));
            
            if(user.Password != request.Password.Hashing())
                return BadRequest(Result.Fail("Password not match"));

            var result = _mapper.Map<AuthenticationResponse>(user);
            result.Token = JwtGenerator.Generate(user.Id);
            return Ok(Result.Ok(result));
        }
    }
}

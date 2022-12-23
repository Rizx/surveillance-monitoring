using System.Collections;
using System.Threading.Tasks;
using API.Repositories;
using API.Shared;
using AutogateAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CameraController : ControllerBase
    {
        private readonly ILogger<CameraController> _logger;
        private readonly CameraRepository _cameraRepository;

        public CameraController(
            CameraRepository cameraRepository,
            ILogger<CameraController> logger)
        {
            _logger = logger;
            _cameraRepository = cameraRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cameras = await _cameraRepository.Gets();
            return Ok(Result<IEnumerable>.Ok(cameras));
        }

        [HttpGet("videos")]
        public async Task<IActionResult> GetVideos()
        {
            var videos = await _cameraRepository.GetVideos();
            return Ok(Result<IEnumerable>.Ok(videos));
        }
    }
}

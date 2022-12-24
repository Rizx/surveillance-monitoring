using System.Collections;
using System.Threading.Tasks;
using API.Repositories;
using API.Shared;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System.Collections.Generic;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CameraController : ControllerBase
    {
        private readonly ILogger<CameraController> _logger;
        private readonly CameraRepository _cameraRepository;
        private readonly IMapper _mapper;

        public CameraController(
            CameraRepository cameraRepository,
            IMapper mapper,
            ILogger<CameraController> logger)
        {
            _logger = logger;
            _cameraRepository = cameraRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cameras = await _cameraRepository.GetList();
            var result = _mapper.Map<IEnumerable<CameraResponse>>(cameras);
            return Ok(Result.Ok(result));
        }

        [HttpGet("videos")]
        public async Task<IActionResult> GetVideos()
        {
            var videos = await _cameraRepository.GetVideos();
            return Ok(Result.Ok(videos));
        }
    }
}

using System.Collections;
using System.Threading.Tasks;
using API.Repositories;
using API.Shared;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System.Collections.Generic;
using API.Services;
using System;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CameraController : ControllerBase
    {
        private readonly ILogger<CameraController> _logger;
        private readonly CameraRepository _cameraRepository;
        private readonly VideoStreamingService _videoStreamingService;
        private readonly IMapper _mapper;

        public CameraController(
            CameraRepository cameraRepository,
            VideoStreamingService videoStreamingService,
            IMapper mapper,
            ILogger<CameraController> logger)
        {
            _logger = logger;
            _cameraRepository = cameraRepository;
            _videoStreamingService = videoStreamingService;
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

        [HttpGet("streaming")]
        public async Task<IActionResult> GetStreaming([FromQuery] long id)
        {
            var camera = await _cameraRepository.Get(id);

            var stream = await _videoStreamingService.GetStreamAsync(camera.UserName, camera.Password, camera.VideoUrl);
            return new FileStreamResult(stream, "multipart/x-mixed-replace; boundary=myboundary")
            {
                FileDownloadName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".mjpeg",
                LastModified = DateTime.UtcNow.AddSeconds(-5),
                EntityTag = new Microsoft.Net.Http.Headers.EntityTagHeaderValue("\"VideoStreaming\"")
            };
        }
    }
}

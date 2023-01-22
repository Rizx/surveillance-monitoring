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
using System.IO;

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

        // [HttpGet("streaming")]
        // public async Task<IActionResult> GetStreaming([FromQuery] string name)
        // {
        //     var camera = await _cameraRepository.Get(name);
        //     var filename = "/videos/" + camera.Name + "/index.m3u8";
        //     // var command = $"ffmpeg -rtsp_transport tcp -i {camera.VideoUrl} -an -c:v libx264 -crf 21 -preset veryfast -fflags nobuffer -flags low_delay -t 20 -f hls -hls_time 1 -hls_list_size 3 -hls_flags delete_segments {filename}";
        //     var command = $"ffmpeg -rtsp_transport tcp -i '{camera.VideoUrl}' -an -c:v libx264 -crf 21 -preset veryfast -fflags nobuffer -flags low_delay -t 60 -f hls -hls_time 1 -hls_list_size 3 -hls_flags delete_segments {filename}";
        //     var result = FfmpegService.Start(camera.Name, command);
        //     if(result)
        //         return Ok(filename);
        //     return StatusCode(500);
        // }

        [HttpGet("{camera}/index.m3u8")]
        public async Task<IActionResult> GetStreamingAsync(string camera)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            var filePath = $"/videos/{camera}/index.m3u8";
            if (!System.IO.File.Exists(filePath) || System.IO.File.GetLastWriteTime(filePath).AddSeconds(5) < DateTime.Now)
            {
                var entity = await _cameraRepository.Get(camera);
                FfmpegService.Start(entity.Name, entity.Parameters, entity.VideoUrl, filePath);
                System.Threading.Thread.Sleep(3000);
            }

            try
            {
                return File(System.IO.File.OpenRead(filePath), "application/octet-stream", enableRangeProcessing: true);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{camera}/{fileName}")]
        public IActionResult GetFile(string camera, string fileName)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            var filePath = $"/videos/{camera}/{fileName}";
            if (System.IO.File.Exists(filePath))
                return File(System.IO.File.OpenRead($"/Videos/{camera}/{fileName}"), "application/octet-stream", enableRangeProcessing: true);
            return BadRequest();
        }

        // [HttpGet("streaming")]
        // public async Task<IActionResult> GetStreaming([FromQuery] long id)
        // {
        //     var camera = await _cameraRepository.Get(id);
        //     var filename = "/videos/" + camera.Name + "/index.m3u8";
        //     var command = $"ffmpeg -rtsp_transport tcp -i {camera.VideoUrl} -an -c:v libx264 -crf 21 -preset veryfast -fflags nobuffer -flags low_delay -t 20 -f hls -hls_time 1 -hls_list_size 3 -hls_flags delete_segments {filename}";
        //     var result = FfmpegService.Start(camera.Name, command);
        //     if(result)
        //         return Ok(filename);
        //     return StatusCode(500);
        // }

        // [HttpGet("streaming")]
        // public async Task<IActionResult> GetStreaming([FromQuery] long id)
        // {
        //     var camera = await _cameraRepository.Get(id);

        //     var stream = await _videoStreamingService.GetStreamAsync(camera.UserName, camera.Password, camera.VideoUrl);
        //     return new FileStreamResult(stream, "multipart/x-mixed-replace; boundary=myboundary")
        //     {
        //         FileDownloadName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".mjpeg",
        //         LastModified = DateTime.UtcNow.AddSeconds(-5),
        //         EntityTag = new Microsoft.Net.Http.Headers.EntityTagHeaderValue("\"VideoStreaming\"")
        //     };
        // }
    }
}

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
using Microsoft.Extensions.Options;
using API.Settings;
using System.IO;
using API.Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class HistoryController : ControllerBase
    {
        private readonly ILogger<HistoryController> _logger;
        private readonly HistoryRepository _historyRepository;
        private readonly IMapper _mapper;
        private readonly MemberRepository _memberRepository;
        private readonly CameraRepository _cameraRepository;
        private readonly ServiceSetting _serviceSetting;

        public HistoryController(
            HistoryRepository historyRepository,
            MemberRepository memberRepository,
            CameraRepository cameraRepository,
            IMapper mapper,
            IOptions<ServiceSetting> serviceSetting, 
            ILogger<HistoryController> logger)
        {
            _historyRepository = historyRepository;
            _memberRepository = memberRepository;
            _cameraRepository = cameraRepository;
            _serviceSetting = serviceSetting.Value;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// List History
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var entities = await _historyRepository.GetList();
            var result = _mapper.Map<IEnumerable<HistoryResponse>>(entities);
            return Ok(Result.Ok(result));
        }

        /// <summary>
        /// Register History
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] HistoryRequest request)
        {
            var cameras = (await _cameraRepository.GetCaptureUrl(request.Activity)).ToList();
            var photos = new List<string>();
            for (int counter = 1; counter <= cameras.Count; counter++)
            {
                var camera = cameras[counter-1];
                var name = GeneratePhotoName(request.Activity, request.CardId, counter);
                var url = CombineUrl(_serviceSetting.PhotoDirectory, name.Replace('\\', '/'));
                photos.Add(url);

                var image = CaptureImageService.Capture(camera);
                if (image.Length > 0)
                {
                    var fullPath = Path.Combine(_serviceSetting.PhotoPath, name);
                    FileService.WriteImage(fullPath, image);
                }
            }

            var member = await _memberRepository.GetByCard(request.CardId);

            var history = new History(
                id: 0,
                request.Activity,
                request.CardId,
                name: member?.Fullname,
                address: member?.Address,
                request.State,
                date: DateTime.Now,
                photos: photos.ToArray());

            var result = await _historyRepository.Add(history);

            if (result == 0)
                return BadRequest(Result.Fail("Add History Failed"));
            return Ok(Result.Ok("Add History Succed"));
        }

        private static string GeneratePhotoName(string activity, string cardid, int counter)
        {
            return Path.Combine(
                $"{DateTime.Now:yyyy}",
                $"{DateTime.Now:MM}",
                $"{DateTime.Now:dd}",
                $"{activity.ToLower()}_{DateTime.Now:yyyyMMddHHmmss}_{cardid}_{counter}.jpg");
        }

        private static string CombineUrl(string url, string param)
        {
            if (url.Last() == '/')
                url = url[..^1];

            return UriCombine(url, param);
        }

        private static string UriCombine(string uri, params string[] segments)
        {
            if (string.IsNullOrWhiteSpace(uri))
                return null;

            if (segments == null || segments.Length == 0)
                return uri;

            return segments.Aggregate(uri, (current, segment) => $"{current.TrimEnd('/')}/{segment.TrimStart('/')}");
        }
    }
}

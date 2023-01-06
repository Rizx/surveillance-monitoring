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
    public class MemberController : ControllerBase
    {
        private readonly ILogger<MemberController> _logger;
        private readonly MemberRepository _memberRepository;
        private readonly HistoryRepository _historyRepository;
        private readonly IMapper _mapper;

        public MemberController(
            MemberRepository memberRepository,
            HistoryRepository historyRepository,
            IMapper mapper,
            ILogger<MemberController> logger)
        {
            _memberRepository = memberRepository;
            _historyRepository = historyRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// List Member
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var entities = await _memberRepository.GetList();
            var result = _mapper.Map<IEnumerable<MemberResponse>>(entities);
            return Ok(Result.Ok(result));
        }

        /// <summary>
        /// Get by id
        /// </summary>
        [HttpGet("id")]
        public async Task<IActionResult> Get([FromQuery] long id)
        {
            var entity = await _memberRepository.Get(id);
            var result = _mapper.Map<MemberResponse>(entity);
            if (result is null)
                return BadRequest(Result.Fail("Not Found"));
            return Ok(Result.Ok(result));
        }

        /// <summary>
        /// Register Member
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> RegisterMember([FromBody] MemberRequest request)
        {
            var entity = await _memberRepository.GetByUsername(request.UserName);
            if (entity is not null)
            {
                return BadRequest(Result.Fail("Member Registered"));
            }

            var member = new Member(
                id: 0,
                request.FullName,
                request.UserName,
                request.Password.Hashing(),
                request.Address,
                request.CardId,
                request.FotoProfile,
                active: true);

            var result = await _memberRepository.Add(member);

            if (result == 0)
                return BadRequest(Result.Fail("Regiter Member Failed"));
            return Ok(Result.Ok("Register Member Succed"));
        }

        /// <summary>
        /// Update Member
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> UpdateMember([FromBody] MemberRequest request)
        {
            var entity = await _memberRepository.Get(request.Id);
            if (entity is null)
            {
                return BadRequest(Result.Fail("Member Unregistered"));
            }

            entity.Changes(request.FullName, request.Address, request.CardId, request.FotoProfile, request.Active);
            entity.ChangePassword(request.Password);

            var result = await _memberRepository.Update(entity);

            if (result == 0)
                return BadRequest(Result.Fail("Update Member Failed"));
            return Ok(Result.Ok("Update Member Succed"));
        }

        /// <summary>
        /// Register Member
        /// </summary>
        [HttpPost("card")]
        public async Task<IActionResult> RegisterCard([FromBody] RegisterCardRequest request)
        {
            var entity = await _memberRepository.GetByUsername(request.UserName);
            if (entity is null)
            {
                return BadRequest(Result.Fail("Member Unregistered"));
            }

            if(entity.CardId is null)
                entity.CardId = request.CardId;
            else if(!entity.CardId.Contains(request.CardId))
                entity.CardId += $"|{request.CardId}";

            var result = await _memberRepository.Update(entity);
            var history = await _historyRepository.Get(request.HistoryId);
            if (history is not null)
            {
                history.Name = entity.Fullname;
                history.Address = entity.Address;
                await _historyRepository.Update(history);
            }

            if (result == 0)
                return BadRequest(Result.Fail("Register Card Failed"));
            return Ok(Result.Ok("Register Card Succed"));
        }

    }
}

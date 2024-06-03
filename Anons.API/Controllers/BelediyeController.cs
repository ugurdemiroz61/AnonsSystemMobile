using Anons.Core.DTOs;
using Anons.Core.Services;
using Anons.Service.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Anons.API.Controllers
{

    public class BelediyeController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IBelediyeService _belediyeService;
        public BelediyeController(IBelediyeService belediyeService, IMapper mapper)
        {
            _mapper = mapper;

            _belediyeService = belediyeService;
        }

        
        [HttpGet("[action]")]
        public async Task<IActionResult> GetUstBelediyeler()
        {
            return CreateActionResult(await _belediyeService.GetUstBelediyelerAsync());
        }

        [Authorize]
        [HttpGet("[action]/{ustBelediyeId}")]
        public async Task<IActionResult> GetAltBelediyeler(int ustBelediyeId)
        {
            return CreateActionResult(await _belediyeService.GetAltBelediyelerAsync(ustBelediyeId));
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBelediyeById(int id)
        {
            return CreateActionResult(CustomResponseDto<BelediyeDto>.Success(StatusCodes.Status200OK, _mapper.Map<BelediyeDto>(await _belediyeService.GetByIdAsync(id))));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddBelediye(BelediyeDto belediyeDto)
        {
            belediyeDto.CurrentUserName = HttpContext.User.Identity.Name;
            return CreateActionResult(await _belediyeService.AddBelediyeAsync(belediyeDto));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateBelediye(BelediyeDto belediyeDto)
        {
            belediyeDto.CurrentUserName = HttpContext.User.Identity.Name;
            return CreateActionResult(await _belediyeService.UpdateBelediyeAsync(belediyeDto));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{belediyeId}")]
        public async Task<IActionResult> RemoveBelediye(int belediyeId)
        {
            return CreateActionResult(await _belediyeService.RemoveBelediyeAsync(belediyeId));
        }

    }
}

using Anons.Core.DTOs;
using Anons.Core.Entities;
using Anons.Core.Services;
using Anons.Service.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Anons.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DuyuruController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IDuyuruService _duyuruService;
        public DuyuruController(IDuyuruService duyuruService, IMapper mapper)
        {
            _duyuruService = duyuruService;
            _mapper = mapper;
        }
        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetDuyurular(PagingFilterDto<DuyuruFiltrelemeDto> paging)
        {
            return CreateActionResult(await _duyuruService.GetDuyurularAsync(paging));
        }

        [Authorize(Roles = "Belediye")]
        [HttpPost]
        public async Task<IActionResult> AddDuyuru(DuyuruDto duyuruDto)
        {
            duyuruDto.CurrentUserName = HttpContext.User.Identity.Name;
            return CreateActionResult(await _duyuruService.AddDuyuruAsync(duyuruDto));
        }

        [Authorize(Roles = "Belediye")]
        [HttpPut]
        public async Task<IActionResult> UpdateDuyuru(DuyuruDto duyuruDto)
        {
            duyuruDto.CurrentUserName = HttpContext.User.Identity.Name;
            return CreateActionResult(await _duyuruService.UpdateDuyuruAsync(duyuruDto));
        }

        [Authorize(Roles = "Belediye")]
        [HttpDelete("{duyuruId}")]
        public async Task<IActionResult> RemoveDuyuru(int duyuruId)
        {
            return CreateActionResult(await _duyuruService.RemoveDuyuruAsync(duyuruId, HttpContext.User.Identity.Name));
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDuyuruById(int id)
        {
            return CreateActionResult(CustomResponseDto<DuyuruDto>.Success(StatusCodes.Status200OK, _mapper.Map<DuyuruDto>(await _duyuruService.GetByIdAsync(id))));
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetDuyuruTipleri()
        {
            return CreateActionResult(await _duyuruService.GetDuyuruTipleriAsync());
        }
        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetDuyuruTipById(int id)
        {
            return CreateActionResult(await _duyuruService.GetDuyuruTipByIdAsync(id));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddDuyuruTip(DuyuruTipDto duyuruTipDto)
        {
            duyuruTipDto.CurrentUserName = HttpContext.User.Identity.Name;
            return CreateActionResult(await _duyuruService.AddDuyuruTipAsync(duyuruTipDto));
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateDuyuruTip(DuyuruTipDto duyuruTipDto)
        {
            duyuruTipDto.CurrentUserName = HttpContext.User.Identity.Name;
            return CreateActionResult(await _duyuruService.UpdateDuyuruTipAsync(duyuruTipDto));
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("[action]/{duyuruTipId}")]
        public async Task<IActionResult> RemoveDuyuruTip(int duyuruTipId)
        {
            return CreateActionResult(await _duyuruService.RemoveDuyuruTipAsync(duyuruTipId));
        }
    }
}

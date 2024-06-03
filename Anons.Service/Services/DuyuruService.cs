using Anons.Core.DTOs;
using Anons.Core.Entities;
using Anons.Core.Repositories;
using Anons.Core.Services;
using Anons.Core.UnitOfWorks;
using Anons.Repository.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Service.Services
{
    public class DuyuruService : Service<Duyuru>, IDuyuruService
    {
        private readonly IDuyuruRepository _duyuruRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDuyuruTipRepository _duyuruTipRepository;
        private readonly IMapper _mapper;
        public DuyuruService(IGenericRepository<Duyuru> repository, IUnitOfWork unitOfWork, IDuyuruRepository duyuruRepository, IUserRepository userRepository, IMapper mapper, IDuyuruTipRepository duyuruTipRepository) : base(repository, unitOfWork)
        {
            _duyuruRepository = duyuruRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _duyuruTipRepository = duyuruTipRepository;
        }



        public async Task<CustomResponseDto<RecordResultDto<List<DuyuruResultDto>, NoContentDto>>> GetDuyurularAsync(PagingFilterDto<DuyuruFiltrelemeDto> paging)
        {
            if (paging == null || paging.Filtre == null)
                return CustomResponseDto<RecordResultDto<List<DuyuruResultDto>, NoContentDto>>.Fail(StatusCodes.Status400BadRequest, "Hatalı istek");

            RecordResultDto<List<DuyuruResultDto>, NoContentDto> recordResultDto = await _duyuruRepository.GetDuyurularAsync(paging);

            return CustomResponseDto<RecordResultDto<List<DuyuruResultDto>, NoContentDto>>.Success(StatusCodes.Status200OK, recordResultDto);
        }
        public async Task<CustomResponseDto<DuyuruDto>> AddDuyuruAsync(DuyuruDto duyuruDto)
        {
            Duyuru entity = _mapper.Map<Duyuru>(duyuruDto);
            if (_duyuruTipRepository.Where(s => s.Id == entity.DuyuruTipId).Count() == 0)
            {
                return CustomResponseDto<DuyuruDto>.Fail(StatusCodes.Status400BadRequest, "Duyuru tip id bulunamadı");
            }
            entity.CreateUser = duyuruDto.CurrentUserName;
            entity.Id = 0;
            entity.BelediyeId = await _userRepository.Where(s => s.UserName == duyuruDto.CurrentUserName).Select(s => s.BelediyeId).FirstOrDefaultAsync();
            return CustomResponseDto<DuyuruDto>.Success(StatusCodes.Status201Created, _mapper.Map<DuyuruDto>(await AddAsync(entity)));
        }
        public async Task<CustomResponseDto<NoContentDto>> UpdateDuyuruAsync(DuyuruDto duyuruDto)
        {
            Duyuru entity = await GetByIdAsync(duyuruDto.Id);

            if (entity == null)
            {
                return CustomResponseDto<NoContentDto>.Fail(StatusCodes.Status404NotFound, "Güncellenecek duyuru id bulunamadı");
            }

            User currentUser = await _userRepository.Where(s => s.UserName == duyuruDto.CurrentUserName).FirstOrDefaultAsync();
            if (currentUser.BelediyeId != entity.BelediyeId)
            {
                return CustomResponseDto<NoContentDto>.Fail(StatusCodes.Status400BadRequest, "Sizin belediyenize ait olmayan duyuruyu güncelleyemezsiniz");
            }
            entity.DuyuruTipId = duyuruDto.DuyuruTipId;
            entity.DuyuruIcerik = duyuruDto.DuyuruIcerik;
            entity.DuyuruTarihi = duyuruDto.DuyuruTarihi;
            entity.UpdateUser = duyuruDto.CurrentUserName;
            await UpdateAsync(entity);
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDto<NoContentDto>> RemoveDuyuruAsync(int duyuruId, string currentUserName)
        {
            Duyuru entity = await GetByIdAsync(duyuruId);
            if (entity == null)
            {
                return CustomResponseDto<NoContentDto>.Fail(StatusCodes.Status404NotFound, "Silinecek duyuru id bulunamadı");
            }
            User currentUser = await _userRepository.Where(s => s.UserName == currentUserName).FirstOrDefaultAsync();
            if (currentUser.BelediyeId != entity.BelediyeId)
            {
                return CustomResponseDto<NoContentDto>.Fail(StatusCodes.Status400BadRequest, "Sizin belediyenize ait olmayan duyuruyu silemezsiniz");
            }

            await RemoveAsync(entity);
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDto<List<DuyuruTipDto>>> GetDuyuruTipleriAsync()
        {
            return CustomResponseDto<List<DuyuruTipDto>>.Success(StatusCodes.Status200OK, _mapper.Map<List<DuyuruTipDto>>(await _duyuruTipRepository.GetAll().ToListAsync()));
        }

        public async Task<CustomResponseDto<DuyuruTipDto>> AddDuyuruTipAsync(DuyuruTipDto duyuruTipDto)
        {
            DuyuruTip addingDuyuruTip = _mapper.Map<DuyuruTip>(duyuruTipDto);
            addingDuyuruTip.CreateUser = duyuruTipDto.CurrentUserName;
            DuyuruTip duyuruTip = await _duyuruTipRepository.AddDuyuruTipAsync(addingDuyuruTip);
            return CustomResponseDto<DuyuruTipDto>.Success(StatusCodes.Status201Created, _mapper.Map<DuyuruTipDto>(duyuruTip));
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateDuyuruTipAsync(DuyuruTipDto duyuruTipDto)
        {
            DuyuruTip updatingDuyuruTip = _mapper.Map<DuyuruTip>(duyuruTipDto);
            updatingDuyuruTip.UpdateUser = duyuruTipDto.CurrentUserName;
            await _duyuruTipRepository.UpdateDuyuruTipAsync(_mapper.Map<DuyuruTip>(updatingDuyuruTip));
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDto<NoContentDto>> RemoveDuyuruTipAsync(int duyuruTipId)
        {
            await _duyuruTipRepository.RemoveDuyuruTipAsync(duyuruTipId);
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDto<DuyuruTipDto>> GetDuyuruTipByIdAsync(int id)
        {
            if (await _duyuruTipRepository.DuyuruTipKullanildiMiAsync(id))
            {
                return CustomResponseDto<DuyuruTipDto>.Fail(StatusCodes.Status406NotAcceptable, "Bu duyuru tipi ile oluşturumuş kayıt mevcut silinemez");
            }
            return CustomResponseDto<DuyuruTipDto>.Success(StatusCodes.Status200OK, _mapper.Map<DuyuruTipDto>(await _duyuruTipRepository.GetByIdAsync(id)));
        }
    }
}

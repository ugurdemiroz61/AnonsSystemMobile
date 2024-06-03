using Anons.Core.DTOs;
using Anons.Core.Entities;
using Anons.Core.Repositories;
using Anons.Core.Services;
using Anons.Core.UnitOfWorks;
using Anons.Repository.Repositories;
using Anons.Repository.UnitOfWorks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Service.Services
{
    public class BelediyeService : Service<Belediye>, IBelediyeService
    {
        private readonly IBelediyeRepository _belediyeRepository;

        private readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;
        public BelediyeService(IGenericRepository<Belediye> repository, IGenericRepository<Duyuru> duyuruRepository, IUnitOfWork unitOfWork, IBelediyeRepository belediyeRepository, IUserRepository userRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _belediyeRepository = belediyeRepository;

            _mapper = mapper;

            _userRepository = userRepository;

        }

        public async Task<CustomResponseDto<BelediyeDto>> AddBelediyeAsync(BelediyeDto dto)
        {
            Belediye entity = _mapper.Map<Belediye>(dto);
            String ustBelediyeError = await ustBelediyeKontrol(dto.UstBelediyeId);
            if (!string.IsNullOrEmpty(ustBelediyeError))
            {
                return CustomResponseDto<BelediyeDto>.Fail(StatusCodes.Status406NotAcceptable, ustBelediyeError);
            }
            entity.CreateUser = dto.CurrentUserName;
            entity.Id = 0;
            return CustomResponseDto<BelediyeDto>.Success(StatusCodes.Status201Created, _mapper.Map<BelediyeDto>(await AddAsync(entity)));
        }

        public async Task<CustomResponseDto<List<BelediyeDto>>> GetAltBelediyelerAsync(int ustBelediyeId)
        {
            List<Belediye> altBelediyeler = await _belediyeRepository.Where(s => s.UstBelediyeId == ustBelediyeId).ToListAsync();
            List<BelediyeDto> altBelediyeDtos = _mapper.Map<List<BelediyeDto>>(altBelediyeler);
            return CustomResponseDto<List<BelediyeDto>>.Success(StatusCodes.Status200OK, altBelediyeDtos);
        }

        public async Task<CustomResponseDto<List<BelediyeDto>>> GetUstBelediyelerAsync()
        {
            List<Belediye> ustBelediyeler = await _belediyeRepository.Where(s => s.UstBelediyeId == 0).ToListAsync();
            List<BelediyeDto> ustBelediyeDtos = _mapper.Map<List<BelediyeDto>>(ustBelediyeler);
            return CustomResponseDto<List<BelediyeDto>>.Success(StatusCodes.Status200OK, ustBelediyeDtos);
        }

        public async Task<CustomResponseDto<NoContentDto>> RemoveBelediyeAsync(int belediyeId)
        {
            Belediye entity = await GetByIdAsync(belediyeId);
            if (entity == null)
            {
                return CustomResponseDto<NoContentDto>.Fail(StatusCodes.Status404NotFound, "Silinecek belediye bulunamadı");
            }

            int bagliAltBelediyeSayisi = _belediyeRepository.Where(s => s.UstBelediyeId == belediyeId).Count();
            if (bagliAltBelediyeSayisi > 0)
            {
                return CustomResponseDto<NoContentDto>.Fail(StatusCodes.Status406NotAcceptable, "Silmek istediğiniz belediyeye bağlı alt belediyeler mevcut silinemez");
            }
            //int belediyeninDuyuruları = _duyuruRepository. daha yazılmadı

            int belediyeninKullaniciSayisi = _userRepository.Where(s => s.BelediyeId == belediyeId).Count();
            if (belediyeninKullaniciSayisi > 0)
            {
                return CustomResponseDto<NoContentDto>.Fail(StatusCodes.Status406NotAcceptable, "Silmek istediğiniz belediyeye kayıtlı kullanıcılar mevcut silinemez");
            }
            await RemoveAsync(entity);

            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }
        private async Task<string> ustBelediyeKontrol(int UstBelediyeId)
        {
            if (UstBelediyeId != 0)
            {
                Belediye ustBelediye = await _belediyeRepository.GetByIdAsync(UstBelediyeId);
                if (ustBelediye == null)
                {
                    return "Üst belediye bulunamadı";
                }
                else if (ustBelediye.UstBelediyeId != 0)
                {
                    return "Belirtilen üst belediye id bir üst belediyeye ait değil";
                }
                else
                {
                    return "";
                }
            }
            else { return ""; }
        }
        public async Task<CustomResponseDto<NoContentDto>> UpdateBelediyeAsync(BelediyeDto dto)
        {


            String ustBelediyeError = await ustBelediyeKontrol(dto.UstBelediyeId);
            if (!string.IsNullOrEmpty(ustBelediyeError))
            {
                return CustomResponseDto<NoContentDto>.Fail(StatusCodes.Status406NotAcceptable, ustBelediyeError);
            }
            Belediye entity = await GetByIdAsync(dto.Id);
            if (entity == null)
            {
                return CustomResponseDto<NoContentDto>.Fail(StatusCodes.Status404NotFound, "Güncellenecek belediye id bulunamadı");
            }
            entity.BelediyeAdi = dto.BelediyeAdi;
            entity.UstBelediyeId = dto.UstBelediyeId;
            entity.UpdateUser = dto.CurrentUserName;
            await UpdateAsync(entity);
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);

        }
    }
}

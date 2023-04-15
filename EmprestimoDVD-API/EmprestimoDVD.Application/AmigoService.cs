using AutoMapper;
using Common.DTO;
using EmprestimoDVD.Domain.Entidades;
using EmprestimoDVD.DTO;
using EmprestimoDVD.Infrastructure.Repository;
using EmprestimoDVD.Infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmprestimoDVD.Application
{
    public class AmigoService : IAmigoService
    {
        private readonly IAmigoRepository _amigoRepository;
        private readonly IMapper _mapper;

        public AmigoService(IAmigoRepository amigoRepository, IMapper mapper)
        {
            _amigoRepository = amigoRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDTO<Amigo>> CreateOrUpdate(int? id, AmigoDTO dto)
        {
            ResponseDTO<Amigo> responseDTO = new();
            try
            {
                Amigo entity;
                if (id == null)
                {
                    entity = _mapper.Map<Amigo>(dto);
                    await _amigoRepository.InsertAsync(entity);
                }
                else
                {
                    entity = await _amigoRepository.GetTrackedEntities().FirstOrDefaultAsync(x => x.Id == id) ??
                             throw new Exception("Registro não encotrado!");
                    _mapper.Map(dto, entity);
                }
                await _amigoRepository.SaveChangesAsync();
                responseDTO.Object = entity;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }

        public async Task<ResponseDTO<Amigo>> Delete(int id)
        {
            ResponseDTO<Amigo> responseDTO = new();
            try
            {
                var entity = await _amigoRepository.GetEntities().FirstOrDefaultAsync(x => x.Id == id) ??
                             throw new Exception("Registro não encotrado!");
                responseDTO.Object = entity;
                _amigoRepository.Delete(entity);
                await _amigoRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }

        public async Task<ResponseDTO<IEnumerable<Amigo>>> GetAll()
        {
            ResponseDTO<IEnumerable<Amigo>> responseDTO = new();
            try
            {
                var entity = await _amigoRepository.GetListAsync();
                responseDTO.Object = entity;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }

        public async Task<ResponseDTO<Amigo>> GetById(int? id)
        {
            ResponseDTO<Amigo> responseDTO = new();
            try
            {
                var entity = await _amigoRepository.GetEntities().FirstOrDefaultAsync(x => x.Id == id) ??
                             throw new Exception("Registro não encotrado!");
                responseDTO.Object = entity;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }
    }
}

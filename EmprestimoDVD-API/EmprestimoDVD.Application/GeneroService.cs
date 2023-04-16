using AutoMapper;
using Common.DTO;
using EmprestimoDVD.Domain.Entidades;
using EmprestimoDVD.DTO;
using EmprestimoDVD.Infrastructure.Repository;
using EmprestimoDVD.Infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmprestimoDVD.Application
{
    public class GeneroService : IGeneroService
    {
        private readonly IGeneroRepository _generoRepository;
        private readonly IMapper _mapper;

        public GeneroService(IGeneroRepository generoRepository, IMapper mapper)
        {
            _generoRepository = generoRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDTO<Genero>> CreateOrUpdate(int? id, GeneroDTO dto)
        {
            ResponseDTO<Genero> responseDTO = new();
            try
            {
                Genero entity;
                if (id == null)
                {
                    entity = _mapper.Map<Genero>(dto);
                    await _generoRepository.InsertAsync(entity);
                }
                else
                {
                    entity = await _generoRepository.GetTrackedEntities().FirstOrDefaultAsync(x => x.Id == id) ??
                             throw new Exception("Registro não encotrado!");
                    _mapper.Map(dto, entity);
                }
                await _generoRepository.SaveChangesAsync();
                responseDTO.Object = entity;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }

        public async Task<ResponseDTO<Genero>> Delete(int id)
        {
            ResponseDTO<Genero> responseDTO = new();
            try
            {
                var entity = await _generoRepository.GetEntities().FirstOrDefaultAsync(x => x.Id == id) ??
                             throw new Exception("Registro não encotrado!");
                responseDTO.SetNotImplemented();
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }

        public async Task<ResponseDTO<IEnumerable<Genero>>> GetAll()
        {
            ResponseDTO<IEnumerable<Genero>> responseDTO = new();
            try
            {
                var entity = await _generoRepository.GetListAsync();
                responseDTO.Object = entity;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }

        public async Task<ResponseDTO<Genero>> GetById(int? id)
        {
            ResponseDTO<Genero> responseDTO = new();
            try
            {
                var entity = await _generoRepository.GetEntities().FirstOrDefaultAsync(x => x.Id == id) ??
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

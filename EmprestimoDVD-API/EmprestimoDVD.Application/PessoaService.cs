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
    public class PessoaService : IPessoaService
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IMapper _mapper;

        public PessoaService(IPessoaRepository pessoaRepository, IMapper mapper)
        {
            _pessoaRepository = pessoaRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDTO<Pessoa>> CreateOrUpdate(int? id, PessoaDTO dto)
        {
            ResponseDTO<Pessoa> responseDTO = new();
            try
            {
                Pessoa entity;
                if (id == null)
                {
                    entity = _mapper.Map<Pessoa>(dto);
                    await _pessoaRepository.InsertAsync(entity);
                }
                else
                {
                    entity = await _pessoaRepository.GetTrackedEntities().FirstOrDefaultAsync(x => x.Id == id) ??
                             throw new Exception("Registro não encotrado!");
                    _mapper.Map(dto, entity);
                }
                await _pessoaRepository.SaveChangesAsync();
                responseDTO.Object = entity;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }

        public async Task<ResponseDTO<Pessoa>> Delete(int id)
        {
            ResponseDTO<Pessoa> responseDTO = new();
            try
            {
                var entity = await _pessoaRepository.GetEntities().FirstOrDefaultAsync(x => x.Id == id) ??
                             throw new Exception("Registro não encotrado!");
                responseDTO.SetNotImplemented();
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }

        public async Task<ResponseDTO<IEnumerable<Pessoa>>> GetAll()
        {
            ResponseDTO<IEnumerable<Pessoa>> responseDTO = new();
            try
            {
                var entity = await _pessoaRepository.GetAllPessoa();
                responseDTO.Object = entity;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }

        public async Task<ResponseDTO<Pessoa>> GetById(int? id)
        {
            ResponseDTO<Pessoa> responseDTO = new();
            try
            {
                var entity = await _pessoaRepository.GetPessoaById(id) ??
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

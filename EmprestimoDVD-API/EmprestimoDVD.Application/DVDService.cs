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
    public class DVDService : IDVDService
    {
        private readonly IDVDRepository _dvdRepository;
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IGeneroRepository _generoRepository;
        private readonly IMapper _mapper;
        private readonly IEmprestimoRepository _emprestimoRepository;

        public DVDService(IDVDRepository dvdRepository,
                          IMapper mapper,
                          IPessoaRepository pessoaRepository,
                          IGeneroRepository generoRepository,
                          IEmprestimoRepository emprestimoRepository)
        {
            _dvdRepository = dvdRepository;
            _mapper = mapper;
            _pessoaRepository = pessoaRepository;
            _generoRepository = generoRepository;
            _emprestimoRepository = emprestimoRepository;
        }

        public async Task<ResponseDTO<DVD>> CreateOrUpdate(int? id, DVDDTO dto)
        {
            ResponseDTO<DVD> responseDTO = new();
            try
            {
                DVD entity;
                var diretor = await _pessoaRepository.GetTrackedEntities().FirstOrDefaultAsync(x => x.Id == dto.IdDiretor) ?? throw new Exception("Diretor não encotrado!");
                var artistaPrincipal = await _pessoaRepository.GetTrackedEntities().FirstOrDefaultAsync(x => x.Id == dto.IdArtistaPrincipal) ?? throw new Exception("Artista Principal não encotrado!");
                var genero = await _generoRepository.GetTrackedEntities().FirstOrDefaultAsync(x => x.Id == dto.IdGenero) ?? throw new Exception("Genero não encotrado!");
                if (id == null)
                {
                    entity = _mapper.Map<DVD>(dto);
                    entity.ArtistaPrincipal = artistaPrincipal;
                    entity.Diretor = diretor;
                    entity.Genero = genero;
                    await _dvdRepository.InsertAsync(entity);
                }
                else
                {
                    entity = await _dvdRepository.GetTrackedEntities().FirstOrDefaultAsync(x => x.Id == id) ??
                             throw new Exception("Registro não encotrado!");
                    _mapper.Map(dto, entity);
                    entity.ArtistaPrincipal = artistaPrincipal;
                    entity.Diretor = diretor;
                    entity.Genero = genero;
                }
                await _dvdRepository.SaveChangesAsync();
                responseDTO.Object = entity;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }

        public async Task<ResponseDTO<DVD>> Delete(int id)
        {
            ResponseDTO<DVD> responseDTO = new();
            try
            {
                var entity = await _dvdRepository.GetEntities().FirstOrDefaultAsync(x => x.Id == id) ??
                             throw new Exception("Registro não encotrado!");
                responseDTO.Object = entity;
                if (await _emprestimoRepository.GetEntities().AnyAsync(x => x.DVD == entity))
                    throw new Exception("Este DVD já foi emprestado, e não pode ser excluído.");
                _dvdRepository.Delete(entity);
                await _dvdRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }

        public async Task<ResponseDTO<IEnumerable<DVD>>> GetAll()
        {
            ResponseDTO<IEnumerable<DVD>> responseDTO = new();
            try
            {
                var entity = await _dvdRepository.GetEntities()
                                                 .Include(x => x.Genero)
                                                 .Include(x => x.ArtistaPrincipal)
                                                 .Include(x => x.Diretor)
                                                 .ToListAsync();
                responseDTO.Object = entity;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }

        public async Task<ResponseDTO<DVD>> GetById(int? id)
        {
            ResponseDTO<DVD> responseDTO = new();
            try
            {
                var entity = await _dvdRepository.GetEntities()
                                                 .Include(x => x.Genero)
                                                 .Include(x => x.ArtistaPrincipal)
                                                 .Include(x => x.Diretor)
                                                 .FirstOrDefaultAsync(x => x.Id == id) ??
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

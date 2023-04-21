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
    public class EmprestimoService : IEmprestimoService
    {
        private readonly IAmigoRepository _amigoRepository;
        private readonly IDVDRepository _DVDRepository;
        private readonly IEmprestimoRepository _emprestimoRepository;

        public EmprestimoService(IAmigoRepository amigoRepository,
                                 IDVDRepository DVDRepository,
                                 IEmprestimoRepository emprestimoRepository)
        {
            _amigoRepository = amigoRepository;
            _DVDRepository = DVDRepository;
            _emprestimoRepository = emprestimoRepository;
        }

        public async Task<ResponseDTO<List<DVDEmprestadoDTO>>> BuscaDVDEmprestimo()
        {
            ResponseDTO<List<DVDEmprestadoDTO>> responseDTO = new();
            try
            {
                var data = await _DVDRepository.GetEntities().Select(x => new DVDEmprestadoDTO
                {
                    Id = x.Id,
                    Genero = x.Genero,
                    ArtistaPrincipal = x.ArtistaPrincipal,
                    Diretor = x.Diretor,
                    Sinopse = x.Sinopse,
                    Titulo = x.Titulo,
                    IdadeMinima = x.IdadeMinima,
                    Amigo = x.Emprestimos.OrderByDescending(x => x.DataEmprestimo).FirstOrDefault(z => z.DVD.Id == x.Id && z.DataDevolver == null).Amigo.Nome
                }).ToListAsync();

                responseDTO.Object = data;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }

        public async Task<ResponseDTO<Emprestimo>> DevolveDVD(EmprestimoDVDDTO emprestimoDVDDTO)
        {
            ResponseDTO<Emprestimo> responseDTO = new();
            try
            {
                var dvd = await _DVDRepository.GetTrackedEntities().FirstOrDefaultAsync(x => x.Id == emprestimoDVDDTO.IdDVD) ?? throw new Exception("DVD não encontrado!");

                var emprestimo = await _emprestimoRepository.GetTrackedEntities().Include(x => x.Amigo).FirstOrDefaultAsync(x => x.DVD == dvd && x.DataDevolver == null) ?? throw new Exception($"Este DVD não está sendo emprestado");
                emprestimo.DataDevolver = DateTime.Now;

                await _emprestimoRepository.SaveChangesAsync();
                responseDTO.Object = emprestimo;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }

        public async Task<ResponseDTO<Emprestimo>> EmprestaDVD(EmprestimoAmigoDTO emprestimoAmigoDTO)
        {
            ResponseDTO<Emprestimo> responseDTO = new();
            try
            {
                var amigo = await _amigoRepository.GetTrackedEntities().FirstOrDefaultAsync(x => x.Id == emprestimoAmigoDTO.IdAmigo) ?? throw new Exception("Amigo não encontrado!");
                var dvd = await _DVDRepository.GetTrackedEntities().FirstOrDefaultAsync(x => x.Id == emprestimoAmigoDTO.IdDVD) ?? throw new Exception("DVD não encontrado!");

                var emprestimo = await _emprestimoRepository.GetEntities().Include(x => x.Amigo).FirstOrDefaultAsync(x => x.DVD == dvd && x.DataDevolver == null);
                if (emprestimo != null)
                    throw new Exception($"Este DVD já está sendo emprestado para: {emprestimo.Amigo.Nome}");

                var faixaEtaria = await VerificaFaixaEtaria(emprestimoAmigoDTO);
                if (!faixaEtaria.Object)
                    throw new Exception("Este DVD não pode ser emprestado, pois o amigo não tem a idade mínima");

                emprestimo = new()
                {
                    DVD = dvd,
                    Amigo = amigo,
                    DataEmprestimo = DateTime.Now,
                };
                await _emprestimoRepository.InsertAsync(emprestimo);
                await _emprestimoRepository.SaveChangesAsync();
                responseDTO.Object = emprestimo;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }

        public async Task<ResponseDTO<List<Emprestimo>>> HistoricoEmprestimo(EmprestimoDVDDTO emprestimoDVDDTO)
        {
            ResponseDTO<List<Emprestimo>> responseDTO = new();
            try
            {
                var dvd = await _DVDRepository.GetEntities().FirstOrDefaultAsync(x => x.Id == emprestimoDVDDTO.IdDVD) ?? throw new Exception("DVD não encontrado!");
                var historico = await _emprestimoRepository.GetEntities().Include(x => x.DVD).Include(x => x.Amigo).Where(x => x.DVD == dvd).ToListAsync();
                responseDTO.Object = historico;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }

        private async Task<ResponseDTO<bool>> VerificaFaixaEtaria(EmprestimoAmigoDTO emprestimoAmigoDTO)
        {
            ResponseDTO<bool> responseDTO = new();
            try
            {
                var amigo = await _amigoRepository.GetEntities().FirstOrDefaultAsync(x => x.Id == emprestimoAmigoDTO.IdAmigo) ?? throw new Exception("Amigo não encontrado!");
                var dvd = await _DVDRepository.GetEntities().FirstOrDefaultAsync(x => x.Id == emprestimoAmigoDTO.IdDVD) ?? throw new Exception("DVD não encontrado!");

                responseDTO.Object = (DateTime.Now - amigo.DataNascimento).TotalDays >= 365.25 * 18;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }
    }
}

using Common.DTO;
using EmprestimoDVD.Domain.Entidades;
using EmprestimoDVD.DTO;
using EmprestimoDVD.Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmprestimoDVD.API.Controllers
{
    public class EmprestimoController : BaseController
    {
        private readonly IEmprestimoService _emprestimoService;

        public EmprestimoController(IEmprestimoService emprestimoService)
        {
            _emprestimoService = emprestimoService;
        }

        [HttpGet("HistoricoEmprestimo")]
        [Produces(typeof(ResponseDTO<List<DVDEmprestadoDTO>>))]
        public async Task<IActionResult> HistoricoEmprestimo()
        {
            var data = await _emprestimoService.HistoricoEmprestimo();
            return StatusCode(data.Code, data);
        }

        [HttpGet("BuscaDVDEmprestimo")]
        [Produces(typeof(ResponseDTO<List<DVDEmprestadoDTO>>))]
        public async Task<IActionResult> BuscaDVDEmprestimo()
        {
            var data = await _emprestimoService.BuscaDVDEmprestimo();
            return StatusCode(data.Code, data);
        }

        [HttpPost("EmprestaDVD")]
        [Produces(typeof(ResponseDTO<Emprestimo>))]
        public async Task<IActionResult> EmprestaDVD([FromBody] EmprestimoAmigoDTO emprestimoAmigoDTO)
        {
            var data = await _emprestimoService.EmprestaDVD(emprestimoAmigoDTO);
            return StatusCode(data.Code, data);
        }

        [HttpPost("DevolveDVD")]
        [Produces(typeof(ResponseDTO<Emprestimo>))]
        public async Task<IActionResult> DevolveDVD([FromBody] EmprestimoDVDDTO emprestimoDVDDTO)
        {
            var data = await _emprestimoService.DevolveDVD(emprestimoDVDDTO);
            return StatusCode(data.Code, data);
        }
    }
}

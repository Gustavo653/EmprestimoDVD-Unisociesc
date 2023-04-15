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
    public class PessoaController : BaseController
    {
        private readonly IPessoaService _pessoaService;

        public PessoaController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        [HttpGet("GetAll")]
        [Produces(typeof(ResponseDTO<List<Pessoa>>))]
        public async Task<IActionResult> GetAll()
        {
            var data = await _pessoaService.GetAll();
            return StatusCode(data.Code, data);
        }

        [HttpGet("GetById")]
        [Produces(typeof(ResponseDTO<Pessoa>))]
        public async Task<IActionResult> GetById([FromQuery] int? id)
        {
            var data = await _pessoaService.GetById(id);
            return StatusCode(data.Code, data);
        }

        [HttpPost("CreateOrUpdate")]
        [Produces(typeof(ResponseDTO<Pessoa>))]
        public async Task<IActionResult> CreateOrUpdate([FromQuery] int? id, [FromBody] PessoaDTO pessoaDTO)
        {
            var data = await _pessoaService.CreateOrUpdate(id, pessoaDTO);
            return StatusCode(data.Code, data);
        }

        [HttpDelete("Delete")]
        [Produces(typeof(ResponseDTO<Pessoa>))]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var data = await _pessoaService.Delete(id);
            return StatusCode(data.Code, data);
        }
    }
}

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
    public class GeneroController : BaseController
    {
        private readonly IGeneroService _generoService;

        public GeneroController(IGeneroService generoService)
        {
            _generoService = generoService;
        }

        [HttpGet("GetAll")]
        [Produces(typeof(ResponseDTO<List<Genero>>))]
        public async Task<IActionResult> GetAll()
        {
            var data = await _generoService.GetAll();
            return StatusCode(data.Code, data);
        }

        [HttpGet("GetById")]
        [Produces(typeof(ResponseDTO<Genero>))]
        public async Task<IActionResult> GetById([FromQuery] int? id)
        {
            var data = await _generoService.GetById(id);
            return StatusCode(data.Code, data);
        }

        [HttpPost("CreateOrUpdate")]
        [Produces(typeof(ResponseDTO<Genero>))]
        public async Task<IActionResult> CreateOrUpdate([FromQuery] int? id, [FromBody] GeneroDTO generoDTO)
        {
            var data = await _generoService.CreateOrUpdate(id, generoDTO);
            return StatusCode(data.Code, data);
        }

        [HttpDelete("Delete")]
        [Produces(typeof(ResponseDTO<Genero>))]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var data = await _generoService.Delete(id);
            return StatusCode(data.Code, data);
        }
    }
}

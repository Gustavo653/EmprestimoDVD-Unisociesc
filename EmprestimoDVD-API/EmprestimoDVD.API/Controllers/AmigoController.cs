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
    public class AmigoController : BaseController
    {
        private readonly IAmigoService _amigoService;

        public AmigoController(IAmigoService amigoService)
        {
            _amigoService = amigoService;
        }

        [HttpGet("GetAll")]
        [Produces(typeof(ResponseDTO<List<Amigo>>))]
        public async Task<IActionResult> GetAll()
        {
            var data = await _amigoService.GetAll();
            return StatusCode(data.Code, data);
        }

        [HttpGet("GetById")]
        [Produces(typeof(ResponseDTO<Amigo>))]
        public async Task<IActionResult> GetById([FromQuery] int? id)
        {
            var data = await _amigoService.GetById(id);
            return StatusCode(data.Code, data);
        }

        [HttpPost("CreateOrUpdate")]
        [Produces(typeof(ResponseDTO<Amigo>))]
        public async Task<IActionResult> CreateOrUpdate([FromQuery] int? id, [FromBody] AmigoDTO amigoDTO)
        {
            var data = await _amigoService.CreateOrUpdate(id, amigoDTO);
            return StatusCode(data.Code, data);
        }

        [HttpDelete("Delete")]
        [Produces(typeof(ResponseDTO<Amigo>))]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var data = await _amigoService.Delete(id);
            return StatusCode(data.Code, data);
        }
    }
}

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
    public class DVDController : BaseController
    {
        private readonly IDVDService _dvdService;

        public DVDController(IDVDService dvdService)
        {
            _dvdService = dvdService;
        }

        [HttpGet("GetAll")]
        [Produces(typeof(ResponseDTO<List<DVD>>))]
        public async Task<IActionResult> GetAll()
        {
            var data = await _dvdService.GetAll();
            return StatusCode(data.Code, data);
        }

        [HttpGet("GetById")]
        [Produces(typeof(ResponseDTO<DVD>))]
        public async Task<IActionResult> GetById([FromQuery] int? id)
        {
            var data = await _dvdService.GetById(id);
            return StatusCode(data.Code, data);
        }

        [HttpPost("CreateOrUpdate")]
        [Produces(typeof(ResponseDTO<DVD>))]
        public async Task<IActionResult> CreateOrUpdate([FromQuery] int? id, [FromBody] DVDDTO dvdDTO)
        {
            var data = await _dvdService.CreateOrUpdate(id, dvdDTO);
            return StatusCode(data.Code, data);
        }

        [HttpDelete("Delete")]
        [Produces(typeof(ResponseDTO<DVD>))]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var data = await _dvdService.Delete(id);
            return StatusCode(data.Code, data);
        }
    }
}

using Common.Infrastructure;
using EmprestimoDVD.Domain.Entidades;
using EmprestimoDVD.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmprestimoDVD.Infrastructure.Service
{
    public interface IGeneroService : IServiceBase<Genero, GeneroDTO>
    {
    }
}

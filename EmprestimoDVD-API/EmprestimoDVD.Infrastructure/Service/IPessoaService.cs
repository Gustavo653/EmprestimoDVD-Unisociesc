using Common.Infrastructure;
using EmprestimoDVD.Domain.Entidades;
using EmprestimoDVD.DTO;

namespace EmprestimoDVD.Infrastructure.Service
{
    public interface IPessoaService : IServiceBase<Pessoa, PessoaDTO>
    {
    }
}

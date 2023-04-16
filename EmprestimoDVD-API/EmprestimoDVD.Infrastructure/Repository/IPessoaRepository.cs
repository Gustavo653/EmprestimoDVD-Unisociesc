using Common.Infrastructure;
using EmprestimoDVD.Domain.Entidades;

namespace EmprestimoDVD.Infrastructure.Repository
{
    public interface IPessoaRepository : IRepositoryBase<Pessoa>
    {
        Task<List<Pessoa>> GetAllPessoa();
        Task<Pessoa> GetPessoaById(int? id);
    }
}

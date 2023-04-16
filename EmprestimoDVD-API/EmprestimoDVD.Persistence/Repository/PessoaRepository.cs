using Common.DataAccess;
using EmprestimoDVD.Domain.Entidades;
using EmprestimoDVD.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmprestimoDVD.Persistence.Repository
{
    public class PessoaRepository : BaseRepository<Pessoa, EmprestimoDVDContext>, IPessoaRepository
    {
        public PessoaRepository(EmprestimoDVDContext context) : base(context)
        {
        }

        private IQueryable<Pessoa> GetIQueryablePessoa()
        {
            return GetContext().Pessoa.Where(p => !(p is Amigo)).AsNoTracking().AsQueryable();
        }

        public async Task<List<Pessoa>> GetAllPessoa()
        {
            return await GetIQueryablePessoa().ToListAsync();
        }

        public async Task<Pessoa> GetPessoaById(int? id)
        {
            return await GetIQueryablePessoa().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

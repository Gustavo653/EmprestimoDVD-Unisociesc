using Common.DataAccess;
using EmprestimoDVD.Domain.Entidades;
using EmprestimoDVD.Infrastructure.Repository;

namespace EmprestimoDVD.Persistence.Repository
{
    public class EmprestimoRepository : BaseRepository<Emprestimo, EmprestimoDVDContext>, IEmprestimoRepository
    {
        public EmprestimoRepository(EmprestimoDVDContext context) : base(context)
        {
        }
    }
}

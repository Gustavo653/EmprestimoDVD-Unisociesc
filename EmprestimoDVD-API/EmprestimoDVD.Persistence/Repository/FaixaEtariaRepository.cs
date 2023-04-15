using Common.DataAccess;
using EmprestimoDVD.Domain.Entidades;
using EmprestimoDVD.Infrastructure.Repository;

namespace EmprestimoDVD.Persistence.Repository
{
    public class FaixaEtariaRepository : BaseRepository<FaixaEtaria, EmprestimoDVDContext>, IFaixaEtariaRepository
    {
        public FaixaEtariaRepository(EmprestimoDVDContext context) : base(context)
        {
        }
    }
}

using Common.DataAccess;
using EmprestimoDVD.Domain.Entidades;
using EmprestimoDVD.Persistence.Interface;

namespace EmprestimoDVD.Persistence.Repository
{
    public class FaixaEtariaRepository : BaseRepository<FaixaEtaria, EmprestimoDVDContext>, IFaixaEtariaRepository
    {
        public FaixaEtariaRepository(EmprestimoDVDContext context) : base(context)
        {
        }
    }
}

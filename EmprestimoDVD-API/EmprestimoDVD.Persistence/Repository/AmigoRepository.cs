using Common.DataAccess;
using EmprestimoDVD.Domain.Entidades;
using EmprestimoDVD.Persistence.Interface;

namespace EmprestimoDVD.Persistence.Repository
{
    public class AmigoRepository : BaseRepository<Amigo, EmprestimoDVDContext>, IAmigoRepository
    {
        public AmigoRepository(EmprestimoDVDContext context) : base(context)
        {
        }
    }
}

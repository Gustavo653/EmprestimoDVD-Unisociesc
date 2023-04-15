using Common.DataAccess;
using EmprestimoDVD.Domain.Entidades;
using EmprestimoDVD.Infrastructure.Repository;

namespace EmprestimoDVD.Persistence.Repository
{
    public class AmigoRepository : BaseRepository<Amigo, EmprestimoDVDContext>, IAmigoRepository
    {
        public AmigoRepository(EmprestimoDVDContext context) : base(context)
        {
        }
    }
}

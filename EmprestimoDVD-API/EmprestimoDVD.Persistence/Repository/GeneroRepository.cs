using Common.DataAccess;
using EmprestimoDVD.Domain.Entidades;
using EmprestimoDVD.Infrastructure.Repository;

namespace EmprestimoDVD.Persistence.Repository
{
    public class GeneroRepository : BaseRepository<Genero, EmprestimoDVDContext>, IGeneroRepository
    {
        public GeneroRepository(EmprestimoDVDContext context) : base(context)
        {
        }
    }
}

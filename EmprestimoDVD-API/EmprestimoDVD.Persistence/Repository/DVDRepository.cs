using Common.DataAccess;
using EmprestimoDVD.Domain.Entidades;
using EmprestimoDVD.Infrastructure.Repository;

namespace EmprestimoDVD.Persistence.Repository
{
    public class DVDRepository : BaseRepository<DVD, EmprestimoDVDContext>, IDVDRepository
    {
        public DVDRepository(EmprestimoDVDContext context) : base(context)
        {
        }
    }
}

using Common.DataAccess;
using EmprestimoDVD.Domain.Entidades;
using EmprestimoDVD.Persistence.Interface;

namespace EmprestimoDVD.Persistence.Repository
{
    public class DVDRepository : BaseRepository<DVD, EmprestimoDVDContext>, IDVDRepository
    {
        public DVDRepository(EmprestimoDVDContext context) : base(context)
        {
        }
    }
}

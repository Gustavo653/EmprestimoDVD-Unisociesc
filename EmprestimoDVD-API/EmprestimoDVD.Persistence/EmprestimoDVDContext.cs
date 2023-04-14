using Microsoft.EntityFrameworkCore;
using EmprestimoDVD.DataAccess.Configuration;
using EmprestimoDVD.Domain;

namespace EmprestimoDVD.DataAccess
{
    public class EmprestimoDVDContext : DbContext
    {
        public EmprestimoDVDContext(DbContextOptions<EmprestimoDVDContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
using EmprestimoDVD.Domain.Entidades;
using EmprestimoDVD.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace EmprestimoDVD.Persistence
{
    public class EmprestimoDVDContext : DbContext
    {
        public DbSet<Amigo> Amigo { get; set; }
        public DbSet<DVD> DVD { get; set; }
        public DbSet<Emprestimo> Emprestimo { get; set; }
        public DbSet<Genero> Genero { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }
        public EmprestimoDVDContext(DbContextOptions<EmprestimoDVDContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new DVDConfig());
            modelBuilder.ApplyConfiguration(new EmprestimoConfig());
            modelBuilder.ApplyConfiguration(new GeneroConfig());
            modelBuilder.ApplyConfiguration(new PessoaConfig());
        }
    }
}
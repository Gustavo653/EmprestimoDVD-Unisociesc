using EmprestimoDVD.Domain.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmprestimoDVD.Persistence.Configuration
{
    public class EmprestimoConfig : BaseEntityConfig<Emprestimo>
    {
        public override void Configure(EntityTypeBuilder<Emprestimo> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.DataEmprestimo).IsRequired();
            builder.Property(x => x.DataDevolver).IsRequired();
        }
    }
}

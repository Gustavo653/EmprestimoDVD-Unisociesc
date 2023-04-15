using EmprestimoDVD.Domain.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmprestimoDVD.Persistence.Configuration
{
    public class PessoaConfig : BaseEntityConfig<Pessoa>
    {
        public override void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Nome).IsRequired();
        }
    }
}

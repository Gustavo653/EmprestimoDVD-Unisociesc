using EmprestimoDVD.Domain.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmprestimoDVD.Persistence.Configuration
{
    public class FaixaEtariaConfig : BaseEntityConfig<FaixaEtaria>
    {
        public override void Configure(EntityTypeBuilder<FaixaEtaria> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.De).IsRequired();
            builder.Property(x => x.Ate).IsRequired();
        }
    }
}

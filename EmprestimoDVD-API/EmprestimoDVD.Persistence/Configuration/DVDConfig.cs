using EmprestimoDVD.Domain.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmprestimoDVD.Persistence.Configuration
{
    public class DVDConfig : BaseEntityConfig<DVD>
    {
        public override void Configure(EntityTypeBuilder<DVD> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Titulo).IsRequired();
            builder.Property(x => x.Sinopse).IsRequired();
        }
    }
}

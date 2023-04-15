using EmprestimoDVD.Domain.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmprestimoDVD.Persistence.Configuration
{
    public class GeneroConfig : BaseEntityConfig<Genero>
    {
        public override void Configure(EntityTypeBuilder<Genero> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Descricao).IsRequired();
        }
    }
}

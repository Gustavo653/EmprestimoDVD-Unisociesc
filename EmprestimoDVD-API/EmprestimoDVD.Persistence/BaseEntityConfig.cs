using EmprestimoDVD.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmprestimoDVD.Persistence
{
    public abstract class BaseEntityConfig<TType> : IEntityTypeConfiguration<TType> where TType : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TType> builder)
        {
            builder.HasKey(obj => obj.Id);
        }
    }
}

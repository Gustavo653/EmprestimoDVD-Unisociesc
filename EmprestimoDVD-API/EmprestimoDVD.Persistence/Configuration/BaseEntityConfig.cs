﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EmprestimoDVD.Domain;

namespace EmprestimoDVD.DataAccess.Configuration
{
    public abstract class BaseEntityConfig<TType> : IEntityTypeConfiguration<TType> where TType : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TType> builder)
        {
            builder.HasKey(obj => obj.Id);
        }
    }
}

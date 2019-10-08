using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaNotas.Entidades.Administrar;

namespace SistemaNotas.Datos.Mapping.Administrar
{
    public class AreaMap : IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> builder)
        {
            builder.ToTable("area")
                .HasKey(a => a.idarea);
        }
    }
}

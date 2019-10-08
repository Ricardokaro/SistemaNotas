using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaNotas.Entidades.Administrar;

namespace SistemaNotas.Datos.Mapping.Administrar
{
    class PeriodoescolarMap : IEntityTypeConfiguration<Periodoescolar>
    {
        public void Configure(EntityTypeBuilder<Periodoescolar> builder)
        {
            builder.ToTable("periodo")
                .HasKey(p => p.idperiodo);
        }
    }
}

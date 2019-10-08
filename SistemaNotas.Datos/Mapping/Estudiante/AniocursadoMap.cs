using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaNotas.Entidades.Estudiante;

namespace SistemaNotas.Datos.Mapping.Estudiante
{
    class AniocursadoMap : IEntityTypeConfiguration<Aniocursado>
    {
        public void Configure(EntityTypeBuilder<Aniocursado> builder)
        {
            builder.ToTable("aniocursado")
                .HasKey(ac => ac.idanio_cursado);
            builder.HasOne(ac => ac.estudiante)
                .WithMany(e => e.aniocursados)
                .HasForeignKey(ac => ac.idestudiante);
        }
    }
}

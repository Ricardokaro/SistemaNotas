using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaNotas.Entidades.Administrar;
namespace SistemaNotas.Datos.Mapping.Administrar
{
    class AnioescolarMap : IEntityTypeConfiguration<Anioescolar>
    {
        public void Configure(EntityTypeBuilder<Anioescolar> builder)
        {
            builder.ToTable("anioescolar")
                .HasKey(a => a.idanio_escolar);
        }
    }
}

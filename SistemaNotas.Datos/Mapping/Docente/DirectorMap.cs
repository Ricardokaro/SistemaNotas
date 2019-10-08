using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaNotas.Entidades.Docente;

namespace SistemaNotas.Datos.Mapping.Docente
{
    public class DirectorMap : IEntityTypeConfiguration<Director>
    {
        public void Configure(EntityTypeBuilder<Director> builder)
        {
            builder.ToTable("director")
                .HasKey(d => d.iddirector);

            builder.HasOne(d => d.docente)
                .WithMany(d => d.directores)
                .HasForeignKey(d => d.iddocente);

            builder.HasOne(d => d.curso)
                .WithMany(c => c.directores)
                .HasForeignKey(d => d.idcurso);

            builder.HasOne(d => d.anioescolar)
               .WithMany(a => a.directores)
               .HasForeignKey(a => a.idanio_escolar);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaNotas.Entidades.Docente;

namespace SistemaNotas.Datos.Mapping.Docente
{
    class CalificacionMap : IEntityTypeConfiguration<Calificacion>
    {
        public void Configure(EntityTypeBuilder<Calificacion> builder)
        {
            builder.ToTable("calificacion")
                .HasKey(c => c.idcalificacion);
            builder.HasOne(c => c.estudiante)
                .WithMany(e => e.calificaciones)
                .HasForeignKey(c => c.idestudiante);
            builder.HasOne(c => c.cursoxmateriaxdocente)
                .WithMany(c => c.calificaciones)
                .HasForeignKey(c => c.idcurso_materia_docente);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaNotas.Entidades.Docente;

namespace SistemaNotas.Datos.Mapping.Docente
{
    class CursoxmateriaxdocenteMap : IEntityTypeConfiguration<Cursoxmateriaxdocente>
    {
        public void Configure(EntityTypeBuilder<Cursoxmateriaxdocente> builder)
        {
            builder.ToTable("cursoxmateriaxdocente")
                .HasKey(cmd => cmd.id);
            builder.HasOne(cmd => cmd.docente)
                .WithMany(d => d.cursoxmateriaxdocentes)
                .HasForeignKey(cmd => cmd.iddocente);
        }
    }
}

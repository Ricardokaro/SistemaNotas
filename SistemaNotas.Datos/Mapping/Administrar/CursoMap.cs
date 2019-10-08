using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaNotas.Entidades.Administrar;
namespace SistemaNotas.Datos.Mapping.Administrar
{
    public class CursoMap : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("curso")
                .HasKey(c => c.idcurso);
        }
    }
}

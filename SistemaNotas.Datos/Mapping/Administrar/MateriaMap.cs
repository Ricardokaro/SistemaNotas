using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using SistemaNotas.Entidades.Administrar;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SistemaNotas.Datos.Mapping.Administrar
{
    public class MateriaMap : IEntityTypeConfiguration<Materia>
    {
        public void Configure(EntityTypeBuilder<Materia> builder)
        {
            builder.ToTable("materia")
                .HasKey(m => m.idmateria);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using SistemaNotas.Entidades.Docente;
namespace SistemaNotas.Entidades.Administrar
{
   public class Anioescolar
    {
        public int idanio_escolar { get; set; }
        public string nombre { get; set; }
        [Required]
        public DateTime fecha_inicio { get; set; }
        [Required]
        public DateTime fecha_final { get; set; }
        [Required]
        public string anio { get; set; }
        [Required]
        public bool estado { get; set; }

        public ICollection<Periodoescolar> periodoescolares { get; set; }
        public ICollection<Director> directores { get; set; }
    }
}

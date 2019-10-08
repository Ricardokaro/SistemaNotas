using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SistemaNotas.Entidades.Administrar
{
    public class Periodoescolar
    {
        public int idperiodo { get; set; }
        [Required]
        public int idanio_escolar { get; set; }
        [Required]
        public DateTime fecha_inicio { get; set; }
        [Required]
        public DateTime fecha_final { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public int porcentaje { get; set; }

        public Anioescolar anioescolar { get; set; }    
    }
}

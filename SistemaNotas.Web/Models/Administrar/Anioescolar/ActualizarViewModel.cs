using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;     

namespace SistemaNotas.Web.Models.Administrar.Anioescolar
{
    public class ActualizarViewModel
    {
        public int idanio_escolar { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public string fecha_inicio { get; set; }
        [Required]
        public string fecha_final { get; set; }
        [Required]
        public string anio { get; set; }
    }
}

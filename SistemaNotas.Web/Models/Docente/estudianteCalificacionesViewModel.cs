using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaNotas.Web.Models.Docente
{
    public class estudianteCalificacionesViewModel
    {
        public int idcalificacion { get; set; }
        public int idperiodo { get; set; }
        public string periodo { get; set; }
        public int porcentaje { get; set; }
        public decimal calificacion { get; set; }
        public string observacion { get; set; }
    }
}

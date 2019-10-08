using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaNotas.Web.Models.Administrar.Periodoescolar
{
    public class PeriodoescolarViewModel
    {
        public int idperiodo { get; set; }        
        public int idanio_escolar { get; set; }
        public string anio_escolar { get; set; }
        public string fecha_inicio { get; set; }        
        public string fecha_final { get; set; }        
        public string nombre { get; set; }        
        public int porcentaje { get; set; }
    }
}

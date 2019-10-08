using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaNotas.Web.Models.Administrar.Anioescolar
{
    public class AnioescolarViewModel
    {
        public int idanio_escolar { get; set; }
        public string nombre { get; set; }
        public string fecha_inicio { get; set; }
        public string fecha_final { get; set; }
        public string anio { get; set; }
        public bool estado { get; set; }
    }
}

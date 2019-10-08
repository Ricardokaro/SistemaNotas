using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaNotas.Web.Models.Estudiante
{
    public class SelectEstudianteSincalificacionViewModel
    {
        public int idanio_escolar { get; set; }
        public int idcurso { get; set; }
        public int idmateria { get; set; }
        public int idperiodo { get; set; }
    }
}

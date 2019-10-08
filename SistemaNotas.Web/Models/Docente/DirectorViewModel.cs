using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaNotas.Web.Models.Docente
{
    public class DirectorViewModel
    {
        public int iddirector { get; set; }
        public int iddocente { get; set; }
        public string docente { get; set; }
        public int idcurso { get; set; }
        public string curso { get; set; }
        public int idanio_escolar { get; set; }
        public string anio_escolar { get; set; }
    }
}

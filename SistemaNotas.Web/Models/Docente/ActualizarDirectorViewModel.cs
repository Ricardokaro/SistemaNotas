using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaNotas.Web.Models.Docente
{
    public class ActualizarDirectorViewModel
    {
        public int iddirector { get; set; }
        public int iddocente { get; set; }        
        public int idcurso { get; set; }        
        public int idanio_escolar { get; set; }        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaNotas.Web.Models.Docente
{
    public class CursoxmateriaxdocenteViewModel
    {
        public int id { get; set; }
        public int idcurso { get; set; }
        public string curso { get; set; }
        public int idmateria { get; set; }
        public string materia { get; set; }
        public int iddocente { get; set; }
        public string docente { get; set; }
        public int idanio_escolar{ get; set; }
        public string anioescolar { get; set; }
        public int intensidad_horaria { get; set; }
        public bool estado { get; set; }
    }
}

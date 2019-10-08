using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaNotas.Web.Models.Administrar.Curso
{
    public class CursoViewModel
    {
        public int idcurso { get; set; }
        public string nombre { get; set; }
        public bool estado { get; set; }
        public int nivel { get; set; }
    }
}

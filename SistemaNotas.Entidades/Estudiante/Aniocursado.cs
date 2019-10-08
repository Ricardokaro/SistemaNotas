using System;
using System.Collections.Generic;
using System.Text;
using SistemaNotas.Entidades.Usuarios;

using SistemaNotas.Entidades.Administrar;

namespace SistemaNotas.Entidades.Estudiante
{
    public class Aniocursado
    {
        public int idanio_cursado { get; set; }
        public int idcurso { get; set; }
        public int idestudiante { get; set; }
        public int idanio_escolar { get; set; }
        public string estado { get; set; }

        public Curso curso { get; set; }       
        public Usuario estudiante { get; set; }
        public Anioescolar anioescolar { get; set; }
    }
}

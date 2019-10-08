using System;
using System.Collections.Generic;
using System.Text;
using SistemaNotas.Entidades.Usuarios;
using SistemaNotas.Entidades.Administrar;

namespace SistemaNotas.Entidades.Docente
{
    public class Director
    {
        public int iddirector { get; set; }
        public int iddocente { get; set; }
        public int idcurso { get; set; }
        public int idanio_escolar { get; set; }

        public Usuario docente { get; set; }
        public Curso curso { get; set; }
        public Anioescolar anioescolar { get; set; }
    }
}

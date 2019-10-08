using System;
using System.Collections.Generic;
using System.Text;
using SistemaNotas.Entidades.Administrar;
using SistemaNotas.Entidades.Usuarios;

namespace SistemaNotas.Entidades.Docente
{
    public class Cursoxmateriaxdocente
    {
        public int id { get; set; }
        public int idcurso { get; set; }
        public int idmateria { get; set; }
        public int iddocente { get; set; }
        public int idanio_escolar { get; set; }
        public int intensidad_horaria { get; set; }
        public bool estado { get; set; }

        public Anioescolar anioescolar { get; set; }
        public Materia materia { get; set; }
        public Curso curso { get; set; }
        public Usuario docente { get; set; }
        public ICollection<Calificacion> calificaciones { get; set; }
    }
}

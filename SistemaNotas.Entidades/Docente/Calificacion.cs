using System;
using System.Collections.Generic;
using System.Text;
using SistemaNotas.Entidades.Usuarios;
using SistemaNotas.Entidades.Docente;
using SistemaNotas.Entidades.Administrar;

namespace SistemaNotas.Entidades.Docente
{
    public class Calificacion
    {
        public int idcalificacion { get; set; }
        public int idestudiante { get; set; }
        public int idcurso_materia_docente { get; set; }
        public int idperiodo { get; set; }
        public decimal calificacion { get;set; }
        public string obervacion { get; set; }
        public bool estado { get; set; }

        public Usuario estudiante { get; set; }        
        public Cursoxmateriaxdocente cursoxmateriaxdocente { get; set; }
        public Periodoescolar periodoescolar { get; set; }

    }
}

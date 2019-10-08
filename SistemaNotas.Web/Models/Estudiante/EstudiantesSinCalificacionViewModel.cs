using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaNotas.Web.Models.Estudiante
{
    public class EstudiantesSinCalificacionViewModel
    {
        public int idanio_cursado { get; set; }
        public int idcurso { get; set; }
        public string curso { get; set; }
        public int idestudiante { get; set; }
        public string estudiante { get; set; }
        public int idanio_escolar { get; set; }
        public string anio_escolar { get; set; }
        public double calificacion { get; set; }
        public string observacion { get; set; }
        public string estado { get; set; }
    }
}

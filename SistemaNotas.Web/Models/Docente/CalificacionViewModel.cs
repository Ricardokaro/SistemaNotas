using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaNotas.Entidades.Docente;


namespace SistemaNotas.Web.Models.Docente
{
    public class CalificacionViewModel
    {        
        public int idestudiante { get; set; }
        public string estudiante { get; set; }
        public int idmateria { get; set; }
        public string materia { get; set; }
        public decimal calificacion { get; set; }
        public List<Calificacion> listadoCalificaciones { get; set; }
    }
}

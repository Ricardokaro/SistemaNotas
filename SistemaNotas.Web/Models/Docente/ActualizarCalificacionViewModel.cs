using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SistemaNotas.Web.Models.Docente
{
    public class ActualizarCalificacionViewModel
    {
        [Required]
        public int idcalificacion { get; set; }
        [Required]
        public int idestudiante { get; set; }        
        [Required]
        public int idcurso_materia_docente { get; set; } 
        [Required]
        public int idperiodo { get; set; }    
        [Required]
        public decimal calificacion { get; set; }
        [StringLength(2000,ErrorMessage = "la Observacion debe tener como maximo 2000 caracetres")]
        public string observacion { get; set; }        
    }
}

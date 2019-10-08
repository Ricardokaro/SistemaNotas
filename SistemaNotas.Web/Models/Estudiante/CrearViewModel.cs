using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SistemaNotas.Web.Models.Estudiante
{
    public class CrearViewModel
    {
        [Required]
        public int idcurso { get; set; }
        [Required]
        public int idestudiante { get; set; }
        [Required]
        public int idanio_escolar { get; set; }
    }
}

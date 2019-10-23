using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SistemaNotas.Web.Models.Administrar.Aniocursado
{
    public class EstudiantexDirectorViewModel
    {
        [Required]
        public int idanio_escolar { get; set; }
        [Required]
        public int idcurso { get; set; }
        [Required]
        public int iddirector { get; set; }
    }
}

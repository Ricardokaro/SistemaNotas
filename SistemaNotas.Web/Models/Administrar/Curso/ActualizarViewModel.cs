using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SistemaNotas.Web.Models.Administrar.Curso
{
    public class ActualizarViewModel
    {
        [Required]
        public int idcurso { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nombre maximo 100 caracteres y minimo 3 caracteres")]
        public string nombre { get; set; }
        [Required]
        public int nivel { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SistemaNotas.Web.Models.Administrar.Materia
{
    public class CrearViewModel
    {
        [Required]
        public int idarea { get; set; }
        [Required]
        public string nombre { get; set; }        
    }
}

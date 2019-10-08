using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SistemaNotas.Web.Models.Administrar.Area
{
    public class ActualizarViewModel
    {
        [Required]
        public int idarea { get; set; }
        [Required]
        [StringLength(50,MinimumLength = 3,ErrorMessage = "E nomre de del area debe contener por maximo 50 caracteres y minimo 3")]
        public string nombre { get; set; }
    }
}

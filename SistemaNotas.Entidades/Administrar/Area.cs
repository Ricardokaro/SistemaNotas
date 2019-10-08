using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SistemaNotas.Entidades.Administrar
{
    public class Area
    {
        public int idarea { get; set; }
        [Required]
        [StringLength(50,MinimumLength = 3,ErrorMessage ="El nombre del area debe tener como minimo 3 caracteres y maximo 50")]
        public string nombre { get; set; }
        public bool estado { get; set; }

        public ICollection<Materia> materias { get; set; }
    }
}

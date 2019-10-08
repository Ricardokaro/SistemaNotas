using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace SistemaNotas.Entidades.Usuarios
{
    public class Rol
    {
        public int idrol { get; set; }
        [Required]
        [StringLength(50,MinimumLength = 3,ErrorMessage ="El nombre del rol no puede ser mayor de 50 caracteres, ni menos de 3")]
        public string nombre { get; set; }
        public string descripcion { get; set; }
        [Required]
        public bool condicion { get; set; }

        public ICollection<Usuario> usuarios { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SistemaNotas.Web.Models.Usuarios
{
    public class ActualizarViewModel
    {
        [Required]
        public int idusuario { get; set; }
        [Required]
        public int idrol { get; set; }
        [Required]
        public string tipo_documento { get; set; }
        [Required]
        public string num_documento { get; set; }
        [Required]
        public string primer_nombre { get; set; }
        public string segundo_nombre { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El primer apellido no puede ser mayor de 100 caracteres, ni menos de 3")]
        public string primer_apellido { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El segundo apellido no puede ser mayor de 100 caracteres, ni menos de 3")]
        public string segundo_apellido { get; set; }
        public string correo_electronico { get; set; }
        [Required]
        public string celular { get; set; }
        [Required]
        public string direccion { get; set; }
        public string password { get; set; }
        public bool act_password { get; set; }
    }
}

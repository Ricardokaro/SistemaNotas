using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using SistemaNotas.Entidades.Docente;
using SistemaNotas.Entidades.Estudiante;

namespace SistemaNotas.Entidades.Usuarios
{
   public class Usuario
   {        
        public int idusuario { get; set; }
        [Required]
        public int idrol { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre no pued tener mas de 50 caracteres")]
        public string tipo_documento { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre no pued tener mas de 100 caracteres")]
        public string num_documento { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre no pued tener mas de 100 caracteres, ni menos de 3")]
        public string primer_nombre { get; set; }
        public string segundo_nombre { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre no pued tener mas de 100 caracteres, ni menos de 3")]
        public string primer_apellido { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre no pued tener mas de 100 caracteres, ni menos de 3")]
        public string segundo_apellido { get; set; }
        public string correo_electronico { get; set; }
        [Required]
        public string celular { get; set; }
        [Required]
        public string direccion { get; set; }
        public byte[] password_hash { get; set; }
        public byte[] password_salt { get; set; }
        [Required]
        public bool condicion { get; set; }

        public Rol rol { get; set; }
        public ICollection<Cursoxmateriaxdocente> cursoxmateriaxdocentes { get; set; }
        public ICollection<Aniocursado> aniocursados { get; set; }
        public ICollection<Calificacion> calificaciones { get; set; }
        public ICollection<Director> directores { get; set; }
   }
}

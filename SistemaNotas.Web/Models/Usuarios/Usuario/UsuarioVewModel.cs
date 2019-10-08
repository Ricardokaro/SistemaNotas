using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaNotas.Web.Models.Usuarios
{
    public class UsuarioVewModel
    {
        public int idusuario { get; set; }
        public int idrol { get; set; }
        public string rol { get; set; }
        public string tipo_documento { get; set; }
        public string num_documento { get; set; }
        public string primer_nombre { get; set; }
        public string segundo_nombre { get; set; }
        public string primer_apellido { get; set; }
        public string segundo_apellido { get; set; }
        public string correo_electronico { get; set; }
        public string celular { get; set; }
        public string direccion { get; set; }
        public byte[] password_hash { get; set; }
        public bool condicion { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SistemaNotas.Web.Models.Usuarios
{
    public class LoginViewModel
    { 
        [Required]
        public string num_documento { get; set; }
        [Required]
        public string password { get; set; }
    }
}

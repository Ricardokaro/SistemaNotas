using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using SistemaNotas.Entidades.Docente;

namespace SistemaNotas.Entidades.Administrar
{
    public class Curso
    {
        public int idcurso { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public int nivel { get; set; }
        [Required]
        public bool estado { get; set; }

        public ICollection<Director> directores { get; set; }
    }
}

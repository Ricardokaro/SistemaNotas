using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaNotas.Web.Models.Administrar.Materia
{
    public class MateriaViewModels
    {
        public int idmateria { get; set; }
        public int idarea { get; set; }
        public string area { get; set; }
        public string nombre { get; set; }
        public bool estado { get; set; }
    }
}

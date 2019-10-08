using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaNotas.Entidades.Administrar
{
    public class Materia
    {
        public int idmateria { get; set; }
        public int idarea { get; set; }        
        public string nombre {get;set;}
        public bool estado { get; set; }

        public Area area { get; set; }
    }
}

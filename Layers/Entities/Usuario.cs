using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_gestor.Layers.Entities
{
    public class Usuario
    {
        public string Login { get; set; }
        public int IdRol { get; set; }
        public string Password { get; set; }
        public string Nombre{ get; set; }
        public bool Estado{ get; set; }

        public override string ToString() => $"{Login.Trim()} - {Nombre.Trim()}";
    }
}

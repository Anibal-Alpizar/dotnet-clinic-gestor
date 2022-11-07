using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_gestor.Layers.Entities
{
    public class Cliente  
    {
        public string IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public int Sexo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int IdProvincia { get; set; }
        public string Correo { get; set; }
        public override string ToString() => $"{Nombre} {Apellido1}";
    }
}

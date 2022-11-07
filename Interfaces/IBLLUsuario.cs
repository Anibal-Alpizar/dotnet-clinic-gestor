using Clinic_gestor.Layers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_gestor.Interfaces
{
    interface IBLLUsuario
    {
        Usuario Login(string pLogin, string Password);
        IEnumerable<Usuario> GetAllLogin();
        Usuario GetUsuarioById(string pLogin);
        Usuario SaveUsuario(Usuario pUsuario);
        bool DeleteUsuario(string pLogin);
    }
}

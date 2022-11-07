using Clinic_gestor.Layers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_gestor.Interfaces
{
    public interface IDALUsuario
    {
        Usuario Login(string pLogin, string pPassword);
        IEnumerable<Usuario> GetAllLogin();
        Usuario SaveUsuario(Usuario pUsuario);
        Usuario UpdateUsuario(Usuario pUsuario);
        Usuario GetUsuarioById(string pLogin);
        bool DeleteUsuario(string pLogin);
    }
}

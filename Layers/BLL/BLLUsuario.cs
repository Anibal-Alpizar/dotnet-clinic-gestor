using Clinic_gestor.Interfaces;
using Clinic_gestor.Layers.DAL;
using Clinic_gestor.Layers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_gestor.Layers.BLL
{
    public class BLLUsuario : IBLLUsuario
    {
        public bool DeleteUsuario(string pLogin)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> GetAllLogin()
        {
            throw new NotImplementedException();
        }

        public Usuario GetUsuarioById(string pLogin)
        {
            throw new NotImplementedException();               
        }

        public Usuario Login(string pLogin, string pPassword)
        {
            IDALUsuario _DalUsuario = new DALUsuario();
            //Encriptar la contraseña
            //string crytpPasswd = Cryptography.EncrypthAES(pPassword);
            return _DalUsuario.Login(pLogin, pPassword);
        }

        public Usuario SaveUsuario(Usuario pUsuario)
        {
            throw new NotImplementedException();
        }
    }
}

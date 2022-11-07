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
    public class BLLCliente : IBLLCliente
    {
        public Task<bool> DeleteCliente(string pId)
        {
            IDALCliente _DALCliente = new DALCliente();

            return _DALCliente.DeleteCliente(pId);
        }

        public Task<IEnumerable<Cliente>> GetAllCliente()
        {
            IDALCliente _DALCliente = new DALCliente();
            return _DALCliente.GetAllCliente();
        }

        public List<Cliente> GetClienteByFilter(string pDescripcion)
        {
            IDALCliente _DALCliente = new DALCliente();
            return _DALCliente.GetClienteByFilter(pDescripcion);
        }

        public Cliente GetClienteById(string pIdCliente)
        {
            IDALCliente _DALCliente = new DALCliente();
            return _DALCliente.GetClienteById(pIdCliente);
        }

        public Task<Cliente> SaveCliente(Cliente pCliente)
        {
            IDALCliente _DALCliente = new DALCliente();
            Task<Cliente> oCliente = null;

            if (_DALCliente.GetClienteById(pCliente.IdCliente) == null)
                oCliente = _DALCliente.SaveCliente(pCliente);
            else
                oCliente = _DALCliente.UpdateCliente(pCliente);

            return oCliente;
        }
    }
}

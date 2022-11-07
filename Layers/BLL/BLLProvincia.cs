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
    public class BLLProvincia : IBLLProvincia
    {
        public List<Provincia> GetAllProvincia()
        {
            IDALProvincia _DALProvincia = new DALProvincia();

            return _DALProvincia.GetAllProvincia();

        }
    }
}

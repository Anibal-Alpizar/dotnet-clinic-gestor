using Clinic_gestor.Layers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_gestor.Interfaces
{
    public interface IBLLProvincia
    {
        List<Provincia> GetAllProvincia();
    }
}

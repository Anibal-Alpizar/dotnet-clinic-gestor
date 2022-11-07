using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Clinic_gestor.UI.Load;
using Clinic_gestor.UI.Loading;
using Clinic_gestor.UI.Login;
using Clinic_gestor.UI.Main;

namespace Clinic_gestor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogin());
        }
    }
}

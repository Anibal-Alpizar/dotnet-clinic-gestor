using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UTN.Winform.Electronics.Properties;

namespace Clinic_gestor.UI.Main
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            guna2ShadowForm1.SetShadowForm(this);
            Settings.Default.Doctor = lblDoctor.Text;
            Settings.Default.Save();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            container(new DashBoard());
        }
        private void container(object _form)
        {
            if (guna2Panel_container.Controls.Count > 0) guna2Panel_container.Controls.Clear();

            Form fm = _form as Form;
            fm.TopLevel = false;
            fm.FormBorderStyle = FormBorderStyle.None;
            fm.Dock = DockStyle.Fill;
            guna2Panel_container.Controls.Add(fm);
            guna2Panel_container.Tag = fm;
            fm.Show();            
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            container(new Patients());
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            container(new About());
        }
    }
}

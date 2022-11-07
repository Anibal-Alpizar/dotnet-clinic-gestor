﻿using Clinic_gestor.UI.Main;
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

namespace Clinic_gestor.UI.Loading
{
    public partial class frmLoad : Form
    {
        public frmLoad()
        {
            InitializeComponent();
        }
        //int cont = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1) this.Opacity += 0.05;
            circularProgressBar1.Value += 1;
            circularProgressBar1.Text = circularProgressBar1.Value.ToString();
            if (circularProgressBar1.Value == 100)
            {
                timer1.Stop();
                timer2.Start();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.Opacity -= 0.01;
            if(this.Opacity == 0)
            {
                timer2.Stop();
                this.Close();
                frmMain frmMainO = new frmMain();
                frmMainO.Show();
                
            }
        }

        private void frmLoad_Load(object sender, EventArgs e)
        {
            lblUserName.Text = Settings.Default.Nombre;
            this.Opacity = 0.0;
            circularProgressBar1.Value = 0;
            circularProgressBar1.Minimum = 0;
            circularProgressBar1.Maximum = 100;
            timer1.Start();
        }
    }
}

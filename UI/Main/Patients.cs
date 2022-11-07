using Clinic_gestor.Clases;
using Clinic_gestor.Interfaces;
using Clinic_gestor.Layers.BLL;
using Clinic_gestor.Layers.Entities;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UTN.Winform.Electronics.Properties;

namespace Clinic_gestor.UI.Main
{
    public partial class Patients : Form
    {
        private static readonly ILog _MyLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");
        public Patients()
        {
            InitializeComponent();
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            // this.CargarDatos();
        }
        private async void CargarDatos()
        {
            IBLLCliente _BLLCliente = new BLLCliente();
            IBLLProvincia _BLLProvincia = new BLLProvincia();
            List<Provincia> lista = null;

            try
            {
                await Task.Delay(500);
                this.dgvDatos.DataSource = await _BLLCliente.GetAllCliente();
                this.cmbProvincia.Items.Clear();
                lista = _BLLProvincia.GetAllProvincia();
                foreach (Provincia oProvincia in lista)
                {
                    this.cmbProvincia.Items.Add(oProvincia);
                }
                this.cmbProvincia.SelectedIndex = 0;
            }
            catch (Exception er)
            {
                StringBuilder msg = new StringBuilder();
                msg.AppendFormat(UtilError.CreateGenericErrorExceptionDetail(MethodBase.GetCurrentMethod(), er));
                _MyLogControlEventos.ErrorFormat("Error {0}", msg.ToString());
                MessageBox.Show("Se ha producido el siguiente error: " + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }


        private void Patients_Load(object sender, EventArgs e)
        {
            try
            {
                CargarDatos();

                int numPacientes = dgvDatos.Rows.Count;

                lblNumPatients.Text = numPacientes.ToString();



            }
            catch (Exception er)
            {
                StringBuilder msg = new StringBuilder();
                msg.AppendFormat(UtilError.CreateGenericErrorExceptionDetail(MethodBase.GetCurrentMethod(), er));
                _MyLogControlEventos.ErrorFormat("Error {0}", msg.ToString());
                MessageBox.Show("Se ha producido el siguiente error: " + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Cliente oCliente = null;
            try
            {
                if (this.dgvDatos.SelectedRows.Count > 0)
                {
                    oCliente = this.dgvDatos.SelectedRows[0].DataBoundItem as Cliente;
                    this.txtIdCliente.Text = oCliente.IdCliente;
                    this.txtApellido1.Text = oCliente.Apellido1;
                    this.txtApellido2.Text = oCliente.Apellido2;
                    this.txtNombre.Text = oCliente.Nombre;
                    this.rdbFemenino.Checked = oCliente.Sexo == 2 ? true : false;
                    this.rdbMasculino.Checked = oCliente.Sexo == 1 ? true : false;
                    this.dtpFechaNacimiento.Value = oCliente.FechaNacimiento;
                    this.txtCorreo.Text = oCliente.Correo;
                    cmbProvincia.SelectedIndex = cmbProvincia.FindString(oCliente.IdProvincia.ToString());
                }
                else
                {
                    MessageBox.Show("Seleccione el registro !", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception er)
            {
                StringBuilder msg = new StringBuilder();
                msg.AppendFormat(UtilError.CreateGenericErrorExceptionDetail(MethodBase.GetCurrentMethod(), er));
                _MyLogControlEventos.ErrorFormat("Error {0}", msg.ToString());
                MessageBox.Show("Se ha producido el siguiente error: " + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnAceptar_Click(object sender, EventArgs e)
        {
            IBLLCliente _BLLCliente = new BLLCliente();
            try
            {
                Cliente oCliente = new Cliente();

                if (string.IsNullOrEmpty(txtIdCliente.Text))
                {
                    //erp.SetError(txtIdCliente, "Id Requerido");
                    txtIdCliente.Focus();
                    return;
                }

                oCliente.IdCliente = this.txtIdCliente.Text;
                oCliente.Nombre = this.txtNombre.Text;
                oCliente.Apellido1 = this.txtApellido1.Text;
                oCliente.Apellido2 = this.txtApellido2.Text;
                oCliente.FechaNacimiento = dtpFechaNacimiento.Value;
                oCliente.IdProvincia = (cmbProvincia.SelectedItem as Provincia).IdProvincia;
                oCliente.Correo = txtCorreo.Text;
                oCliente.Sexo = rdbFemenino.Checked ? 2 : 1;

                oCliente = await _BLLCliente.SaveCliente(oCliente);

                if (oCliente != null)
                    this.CargarDatos();

            }
            catch (Exception er)
            {
                StringBuilder msg = new StringBuilder();
                msg.AppendFormat(UtilError.CreateGenericErrorExceptionDetail(MethodBase.GetCurrentMethod(), er));
                _MyLogControlEventos.ErrorFormat("Error {0}", msg.ToString());
                MessageBox.Show("Se ha producido el siguiente error: " + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            IBLLCliente _IBLLCliente = new BLLCliente();

            try
            {
                if (this.dgvDatos.SelectedRows.Count > 0)
                {
                    Cliente oCliente = this.dgvDatos.SelectedRows[0].DataBoundItem as Cliente;
                    if (MessageBox.Show($"¿Seguro que desea borrar el registro {oCliente.IdCliente.Trim()} {oCliente.Nombre.Trim()}?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _IBLLCliente.DeleteCliente(oCliente.IdCliente);
                        this.CargarDatos();
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione el registro !", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception er)
            {
                StringBuilder msg = new StringBuilder();
                msg.AppendFormat(UtilError.CreateGenericErrorExceptionDetail(MethodBase.GetCurrentMethod(), er));
                _MyLogControlEventos.ErrorFormat("Error {0}", msg.ToString());
                MessageBox.Show("Se ha producido el siguiente error: " + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            try
            {
                if (this.dgvDatos.SelectedRows.Count > 0)
                {

                    Cliente oCliente = this.dgvDatos.SelectedRows[0].DataBoundItem as Cliente;


                    Settings.Default.IdSeleccionado = oCliente.IdCliente;
                    Settings.Default.NombrePaciente = oCliente.Nombre;
                    Settings.Default.Apellido1 = oCliente.Apellido1;
                    Settings.Default.Apellido2 = oCliente.Apellido2;
                    Settings.Default.Sexo = oCliente.Sexo;
                    Settings.Default.FechaNacimiento = oCliente.FechaNacimiento;
                    Settings.Default.Correo = oCliente.Correo;
                    Settings.Default.IdProvincia = oCliente.IdProvincia;


                    string provincia = cmbProvincia.SelectedItem.ToString();
                    string[] provinciaArray = provincia.Split(' ');
                    string provinciaSinNumeros = provinciaArray[1];
                    Settings.Default.Provincia = provinciaSinNumeros;

                    Settings.Default.Save();

                    frmPrescription frmPrescription = new frmPrescription();
                    frmPrescription.ShowDialog();

                    this.Close();





                }
                else
                {
                    MessageBox.Show("Seleccione el registro !", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception er)
            {
                StringBuilder msg = new StringBuilder();
                msg.AppendFormat(UtilError.CreateGenericErrorExceptionDetail(MethodBase.GetCurrentMethod(), er));
                _MyLogControlEventos.ErrorFormat("Error {0}", msg.ToString());
                MessageBox.Show("Se ha producido el siguiente error: " + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void btnActualizarTotal_Click(object sender, EventArgs e)
        {

            int numPacientes = dgvDatos.Rows.Count;

            lblNumPatients.Text = numPacientes.ToString();

        }
    }
}



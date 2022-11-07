using Clinic_gestor.Interfaces;
using Clinic_gestor.Layers.BLL;
using Clinic_gestor.Layers.Entities;
using Clinic_gestor.Properties;
using Clinic_gestor.UI.Loading;
using log4net;
using System;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UTN.Winform.Electronics.Properties;

namespace Clinic_gestor.UI.Login
{
    public partial class frmLogin : Form
    {
        private static readonly ILog _MyLogControlEventos = log4net.LogManager.GetLogger("MyControlEventos");
        private int contador = 0;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            guna2ShadowForm1.SetShadowForm(this);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            IBLLUsuario _BLLUsuario = new BLLUsuario();
            epError.Clear();
            Usuario oUsuario = null;
            try
            {
                if (string.IsNullOrEmpty(this.txtLogin.Text))
                {
                    epError.SetError(txtLogin, "Usuario requerido");
                    this.txtLogin.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(this.txtPassword.Text))
                {
                    epError.SetError(txtPassword, "Contraseña requerida");
                    this.txtPassword.Focus();
                    return;
                }

                oUsuario = _BLLUsuario.Login(this.txtLogin.Text,
                                           this.txtPassword.Text);

                if (oUsuario == null)
                {
                    ++contador;
                    MessageBox.Show("Error en el acceso", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (contador == 3)
                    {
                        MessageBox.Show("Se equivocó en 3 ocasiones, el Sistema se Cerrará por seguridad", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.DialogResult = DialogResult.Cancel;
                        Application.Exit();
                    }
                }
                else
                {

                    Settings.Default.Login = this.txtLogin.Text.Trim();
                    Settings.Default.Password = this.txtPassword.Text.Trim();
                    Settings.Default.Nombre = oUsuario.Nombre;
                    Settings.Default.RolId = oUsuario.IdRol.ToString();

                    _MyLogControlEventos.InfoFormat("Entró a la aplicación :{0}", Settings.Default.Nombre);
                    this.DialogResult = DialogResult.OK;
                    this.Hide();
                    frmLoad frmload = new frmLoad();
                    frmload.Show();
              
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

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            this.txtPassword.PasswordChar = '*';
        }

        private void guna2CustomCheckBox1_Click(object sender, EventArgs e)
        {
            if (guna2CustomCheckBox1.Checked == true)
            {
                this.txtPassword.PasswordChar = '\0';
            }
            else
            {
                this.txtPassword.PasswordChar = '*';
            }

        }
    }

}

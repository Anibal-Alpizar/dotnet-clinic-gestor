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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.IO;
using Rectangle = System.Drawing.Rectangle;
using System.Net.Mail;
using Clinic_gestor.Layers.Entities;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Net;
using ImageMagick;
using Guna.UI2.WinForms;
using Clinic_gestor.UI.Loading;
using System.Threading;

namespace Clinic_gestor.UI.Main
{
    public partial class frmPrescription : Form
    {
        public frmPrescription()
        {
            InitializeComponent();
        }

        private void frmPrescription_Load(object sender, EventArgs e)
        {

            txtId.Text = Settings.Default.IdSeleccionado;
            
            string nombre = Settings.Default.NombrePaciente;
            string apellido1 = Settings.Default.Apellido1;
            string apellido2 = Settings.Default.Apellido2;
            DateTime fechaNacimiento = Settings.Default.FechaNacimiento;
            int domicilio = Settings.Default.IdProvincia;

            txtNombre.Text = nombre.ToUpper() + " " + apellido1.ToUpper() + " " + apellido2.ToUpper();


            DateTime fechaActual = DateTime.Now;
            int edad = fechaActual.Year - fechaNacimiento.Year;
            if (fechaActual.Month < fechaNacimiento.Month || (fechaActual.Month == fechaNacimiento.Month && fechaActual.Day < fechaNacimiento.Day))
            {
                edad--;
            }
            int meses = fechaActual.Month - fechaNacimiento.Month;
            if (fechaActual.Day < fechaNacimiento.Day)
            {
                meses--;
            }
            int dias = fechaActual.Day - fechaNacimiento.Day;
            if (dias < 0)
            {
                dias = dias + 30;
            }
            txtEdad.Text = edad.ToString() + " años, " + meses.ToString() + " meses y " + dias.ToString() + " días";

            txtDomicilio.Text = Settings.Default.Provincia;



            lblFechaYHora.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            lblFecha.Text = DateTime.Now.ToString("dd-MM-yyyy");

        }

        private void btnPdf_Click(object sender, EventArgs e)
        {

            btnPdf.Visible = false;

            SaveFileDialog guardar = new SaveFileDialog();
            guardar.Filter = "Imagenes JPG,PNG|*.jpg;*.png";

            guardar.FileName = "RECETA_" + txtNombre.Text.ToUpper() + ".jpg";

            guardar.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            guardar.ShowDialog();
            if (guardar.FileName != "")
            {
                Bitmap bm = new Bitmap(this.Width, this.Height);
                this.DrawToBitmap(bm, new Rectangle(0, 0, this.Width, this.Height));

                FileStream flujo = new FileStream(guardar.FileName, FileMode.Create, FileAccess.Write);
                bm.Save(flujo, System.Drawing.Imaging.ImageFormat.Bmp);

                flujo.Close();
                bm.Dispose();

            }

            string path = guardar.FileName;

            string pathpdf = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\RECETA_" + txtNombre.Text.ToUpper() + ".pdf";

            using (MagickImageCollection collection = new MagickImageCollection())
            {
                collection.Read(path);
                collection.Write(pathpdf);

                string correo = Settings.Default.Correo;
                string nombre = Settings.Default.NombrePaciente.ToUpper() + " " + Settings.Default.Apellido1.ToUpper() + " " + Settings.Default.Apellido2.ToUpper();

                try
                {
                    MailMessage mensaje = new MailMessage();
                    mensaje.IsBodyHtml = true;
                    mensaje.Subject = "Hola! " + nombre + " te enviamos tu receta";
                    mensaje.Body = "Receta";
                    mensaje.From = new MailAddress("anibalpruebas123@gmail.com");
                    mensaje.To.Add(correo);
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                    smtp.Port = 587;
                    smtp.Credentials = new NetworkCredential("email", "code v");
                    smtp.EnableSsl = true;
                    mensaje.Attachments.Add(new Attachment(pathpdf));
                    smtp.Send(mensaje);
                    MessageBox.Show("Correo Enviado");

                    guna2Button1.Visible = true;
                }
                catch ( Exception error )
                {
                    MessageBox.Show("No se a encontrado el correo, porfavor verificar el correo del paciente");
                }

            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

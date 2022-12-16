using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Pedidos.Vista
{
    public partial class frmLogin : Form
    {
        private int contadorIntentosLogin = 0;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(txtUsuario.Text))
                    throw new ArgumentException("Usuario no puede ser nulo o vacio.");

                if (string.IsNullOrEmpty(txtContraseña.Text))
                    throw new ArgumentException("Contraseña no puede ser nulo o vacio.");

                var usuario = ConsultarUsuario(txtUsuario.Text, txtContraseña.Text);
                if (usuario == null)
                    throw new ArgumentException("Usuario no existe.");

                MessageBox.Show("Bienvenido!",
                    "Inicio de Sesión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                IniciarAplicacion();
            }
            catch (Exception ex)
            {
                contadorIntentosLogin++;

                MessageBox.Show(ex.Message + " " + (contadorIntentosLogin == 3 ? "\nSe alcanzo el limite de intentos." : string.Empty),
                    "Inicio de Sesión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                if (contadorIntentosLogin == 3)
                {
                    CerrarAplicacion();
                }
            }
        }

        private UsuarioDto ConsultarUsuario(string usuario, string password)
        {
            using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
            {
                var productoBuscado = (from u in bd.Usuario
                                       where u.Usuario1.ToUpper().Trim() == usuario.ToUpper().Trim() && 
                                                u.Password.Trim() == password.Trim()
                                       select new UsuarioDto
                                       {
                                           ID_Empleado = u.ID_Empleado,
                                           ID_Usuario = u.ID_Usuario,
                                       }).FirstOrDefault();

                return productoBuscado;
            }
        }

        private void IniciarAplicacion()
        {
            frmPrincipal frmMain = new frmPrincipal();
            frmMain.Show();
            this.Hide();
        }

        private void CerrarAplicacion()
        {
            this.Close();
        }
    }
}

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
        private const string msgUsuarioInvalido = "Usuario o Contraseña Incorrecto.";
        private int contadorIntentosLogin = 0;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text.ToUpper().Trim() == "VENDEDOR" && 
                txtContraseña.Text.ToUpper().Trim() == "1234")
            {
                MessageBox.Show("Bienvenido!", 
                    "Inicio de Sesión", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Information);

                IniciarAplicacion();
            }
            else
            {
                contadorIntentosLogin++;

                MessageBox.Show(msgUsuarioInvalido + " " + (contadorIntentosLogin == 3 ? "Se alcanzo el limite de intentos." : string.Empty),
                    "Inicio de Sesión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                if (contadorIntentosLogin  == 3)
                {
                    CerrarAplicacion();
                }
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

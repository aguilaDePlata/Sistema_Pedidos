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
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text.ToUpper().Trim() == "VENDEDOR" && txtContraseña.Text.ToUpper().Trim() == "1234")
            {
                MessageBox.Show("Bienvenido!");

                frmPrincipal frmMain = new frmPrincipal();
                frmMain.Show();

            }
            else
            {
                MessageBox.Show("Usuario o Contraseña Incorrecto");
            }
        }
    }
}

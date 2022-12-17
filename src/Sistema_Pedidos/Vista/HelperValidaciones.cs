using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Pedidos.Vista
{
    public static class HelperValidaciones
    {
        public static bool NoEsDigito(KeyPressEventArgs e)
        {
            bool noEsDigito = false;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                noEsDigito = true;
            }

            return noEsDigito;
        }

        public static bool NoEsParteNumeroDecimal(object sender, KeyPressEventArgs e)
        {
            bool noEsParteNumeroDecimal = false;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                noEsParteNumeroDecimal = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                noEsParteNumeroDecimal = true;
            }

            return noEsParteNumeroDecimal;
        }
    }
}

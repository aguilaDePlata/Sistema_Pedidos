using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Pedidos
{
    public partial class frmPrincipal : Form
    {
        private frmCliente fCliente;
        private frmEmpleado fEmpleado;
        private frmProducto fProducto;
        private frmPedido fPedido;
        private frmProveedor fProveedor;
        private frmComprobante fComprobante;
        private frmCargo fCargo;
        private frmMarca fMarca;
        private frmModelo fModelo;
        private frmDetallePedido fDetallePedido;

        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void clienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fCliente == null)
            {
                fCliente = new frmCliente();
                fCliente.MdiParent = this;
                fCliente.FormClosed += new FormClosedEventHandler(CerrarFormularioCliente);
                fCliente.Show();
            }
            else
            {
                fCliente.Activate();
            }
        }
        void CerrarFormularioCliente(object sender, FormClosedEventArgs e)
        {
            fCliente = null;
        }

        private void proveedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fProveedor == null)
            {
                fProveedor = new frmProveedor();
                fProveedor.MdiParent = this;
                fProveedor.FormClosed += new FormClosedEventHandler(CerrarFormularioProveedor);
                fProveedor.Show();
            }
            else
            {
                fProveedor.Activate();
            }
        }
        void CerrarFormularioProveedor(object sender, FormClosedEventArgs e)
        {
            fProveedor = null;
        }

        private void cargoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fCargo == null)
            {
                fCargo = new frmCargo();
                fCargo.MdiParent = this;
                fCargo.FormClosed += new FormClosedEventHandler(CerrarFormularioCargo);
                fCargo.Show();
            }
            else
            {
                fCargo.Activate();
            }
        }
        void CerrarFormularioCargo(object sender, FormClosedEventArgs e)
        {
            fCargo = null;
        }

        private void marcaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fMarca == null)
            {
                fMarca = new frmMarca();
                fMarca.MdiParent = this;
                fMarca.FormClosed += new FormClosedEventHandler(CerrarFormularioMarca);
                fMarca.Show();
            }
            else
            {
                fMarca.Activate();
            }
        }
        void CerrarFormularioMarca(object sender, FormClosedEventArgs e)
        {
            fMarca = null;
        }

        private void modeloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fModelo == null)
            {
                fModelo = new frmModelo();
                fModelo.MdiParent = this;
                fModelo.FormClosed += new FormClosedEventHandler(CerrarFormularioModelo);
                fModelo.Show();
            }
            else
            {
                fModelo.Activate();
            }
        }
        void CerrarFormularioModelo(object sender, FormClosedEventArgs e)
        {
            fModelo = null;
        }

        private void detallePedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fDetallePedido == null)
            {
                fDetallePedido = new frmDetallePedido();
                fDetallePedido.MdiParent = this;
                fDetallePedido.FormClosed += new FormClosedEventHandler(CerrarFormularioDetallePedido);
                fDetallePedido.Show();
            }
            else
            {
                fDetallePedido.Activate();
            }
        }
        void CerrarFormularioDetallePedido(object sender, FormClosedEventArgs e)
        {
            fDetallePedido = null;
        }

        private void comprobanteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fComprobante == null)
            {
                fComprobante = new frmComprobante();
                fComprobante.MdiParent = this;
                fComprobante.FormClosed += new FormClosedEventHandler(CerrarFormularioComprobante);
                fComprobante.Show();
            }
            else
            {
                fComprobante.Activate();
            }
        }
        void CerrarFormularioComprobante(object sender, FormClosedEventArgs e)
        {
            fComprobante = null;
        }

        private void empleadoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (fEmpleado == null)
            {
                fEmpleado = new frmEmpleado();
                fEmpleado.MdiParent = this;
                fEmpleado.FormClosed += new FormClosedEventHandler(CerrarFormularioEmpleado);
                fEmpleado.Show();
            }
            else
            {
                fEmpleado.Activate();
            }
        }
        void CerrarFormularioEmpleado(object sender, FormClosedEventArgs e)
        {
            fEmpleado = null;
        }

        private void productoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (fProducto == null)
            {
                fProducto = new frmProducto();
                fProducto.MdiParent = this;
                fProducto.FormClosed += new FormClosedEventHandler(CerrarFormularioProducto);
                fProducto.Show();
            }
            else
            {
                fProducto.Activate();
            }
        }
        void CerrarFormularioProducto(object sender, FormClosedEventArgs e)
        {
            fProducto = null;
        }

        private void pedidoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (fPedido == null)
            {
                fPedido = new frmPedido();
                fPedido.MdiParent = this;
                fPedido.FormClosed += new FormClosedEventHandler(CerrarFormularioPedido);
                fPedido.Show();
            }
            else
            {
                fPedido.Activate();
            }
        }
        void CerrarFormularioPedido(object sender, FormClosedEventArgs e)
        {
            fPedido = null;
        }
    }
}

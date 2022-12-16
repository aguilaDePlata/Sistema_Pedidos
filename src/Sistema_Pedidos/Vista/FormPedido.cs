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
    public partial class frmPedido : Form
    {
        private PedidoDto nuevoPedido;

        public frmPedido()
        {
            InitializeComponent();
        }

        private void frmPedido_Load(object sender, EventArgs e)
        {
            nuevoPedido = new PedidoDto();
            nuevoPedido.Id_Empleado = 1;
            nuevoPedido.Activo = true;

            InicializaControles();
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtClienteId.Text))
                    throw new ArgumentException("El codigo del cliente no puede ser nulo o vacio.");

                var cliente = ConsultarCliente(Convert.ToInt32(txtClienteId.Text));
                if(cliente == null)
                    throw new ArgumentException("El cliente no existe.");

                nuevoPedido.Id_Cliente = cliente.Id_Cliente;

                txtNombreCliente.Text = cliente.NombreCompleto;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                      "Nuevo Pedido",
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Error);
            }
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtProductoId.Text))
                    throw new ArgumentException("El codigo del Producto no puede ser nulo o vacio.");

                var producto = ConsultarProducto(Convert.ToInt32(txtProductoId.Text));
                if(producto == null)
                    throw new ArgumentException("El producto no existe.");
                txtProducto.Text = producto.Descripcion;
                txtPrecio.Text = producto.Precio_Venta.ToString();
                txtCantidad.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                       "Nuevo Pedido",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                e.Handled = NoEsDigito(e);

                if (e.KeyChar == (char)Keys.Enter)
                {
                    if(string.IsNullOrEmpty(txtProducto.Text))
                        throw new ArgumentException(" Seleccione un producto.");


                    nuevoPedido.PedidoDetalles.Add(new Detalle_PedidoDto()
                    {
                        Id_Producto = Convert.ToInt32(txtProductoId.Text),
                        Producto = txtProducto.Text,
                        Precio_Venta = Convert.ToDecimal(txtPrecio.Text),
                        Cantidad = Convert.ToInt32(txtCantidad.Text),
                        Subtotal_Prod = Convert.ToDecimal(txtPrecio.Text) * Convert.ToInt32(txtCantidad.Text),
                    });
                    nuevoPedido.Valor_Total = nuevoPedido.PedidoDetalles.Sum(t => t.Subtotal_Prod);

                    MostrarGridDetallesPedidoYTotal();
                    LimpiarControlesAgregaDetallePedido();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Nuevo Pedido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            nuevoPedido.Fecha_Pedido = dtpFechaPedido.Value;
            nuevoPedido.Fecha_MaxEntrega = dtpFechaEntrega.Value;
            try
            {
                if(!nuevoPedido.PedidoDetalles.Any())
                    throw new ArgumentException(" Minimo insertar un producto para el pedido.");


                this.AgregarPedido(nuevoPedido);

                MessageBox.Show("Pedido grabado exitosamente.", 
                    "Nuevo Pedido ", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, 
                    "Nuevo Pedido", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }

        private void txtClienteId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = NoEsDigito(e);
        }

        private void txtProductoId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = NoEsDigito(e);
        }

        private Pedido MaterializarPedidoDesdeDto(PedidoDto pedidoDto)
        {
            var pedido = new Pedido()
            {
                ID_Cliente = nuevoPedido.Id_Cliente,
                ID_Empleado = nuevoPedido.Id_Empleado,
                Fecha_Pedido = nuevoPedido.Fecha_Pedido,
                Fecha_MaxEntrega = nuevoPedido.Fecha_MaxEntrega,
                Valor_Total = nuevoPedido.Valor_Total,
                Activo = nuevoPedido.Activo,
            };

            pedidoDto.PedidoDetalles.ForEach(f =>
            {
                pedido.Detalle_Pedido.Add(new Detalle_Pedido()
                {
                    ID_Producto = f.Id_Producto,
                    Cantidad = f.Cantidad,
                    Precio_Venta = f.Precio_Venta,
                    Subtotal_Prod = f.Subtotal_Prod,
                });
            });

            return pedido;
        }

        private ClienteDto ConsultarCliente(int clienteId)
        {
            using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
            {
                var clienteBuscado = (from c in bd.Cliente
                                      where c.ID_Cliente == clienteId
                                      select new ClienteDto
                                      {
                                          Id_Cliente = c.ID_Cliente,
                                          NombreCompleto = c.Nombre + " " + c.Apellidos
                                      }).FirstOrDefault();

                return clienteBuscado;
            }
        }

        private ProductoDto ConsultarProducto(int productoId)
        {
            using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
            {
                var productoBuscado = (from p in bd.Producto
                                       where p.ID_Producto == productoId
                                       select new ProductoDto
                                       {
                                           Id_Producto = p.ID_Producto,
                                           Descripcion = p.Descripcion,
                                           Precio_Venta = p.Precio_Venta.ToString(),
                                       }).FirstOrDefault();

                return productoBuscado;
            }
        }

        private void AgregarPedido(PedidoDto pedidoDto)
        {
            using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
            {
                bd.Pedido.Add(MaterializarPedidoDesdeDto(nuevoPedido));

                bd.SaveChanges();
            }
        }

        private void MostrarGridDetallesPedidoYTotal()
        {
            dgvDetallePedido.DataSource = null;
            dgvDetallePedido.DataSource = nuevoPedido.PedidoDetalles;

            this.dgvDetallePedido.Columns["ID_Pedido"].Visible = false;
            this.dgvDetallePedido.Columns["ID_Producto"].Width = 30;
            this.dgvDetallePedido.Columns["Producto"].Width = 350;
            this.dgvDetallePedido.Columns["Cantidad"].Width = 50;
            this.dgvDetallePedido.Columns["Precio_Venta"].Width = 80;
            this.dgvDetallePedido.Columns["Subtotal_Prod"].Width = 80;

            this.dgvDetallePedido.Columns["ID_Producto"].HeaderText = "Id";
            this.dgvDetallePedido.Columns["Precio_Venta"].HeaderText = "Precio";
            this.dgvDetallePedido.Columns["Subtotal_Prod"].HeaderText = "SubTotal";

            txtTotal.Text = Convert.ToString(nuevoPedido.Valor_Total);
        }

        private void LimpiarControlesAgregaDetallePedido()
        {
            txtProductoId.Text = string.Empty;
            txtProducto.Text = string.Empty;
            txtPrecio.Text = "0.00";
            txtCantidad.Text = "";
        }

        private void InicializaControles()
        {
            dtpFechaPedido.Format = DateTimePickerFormat.Custom;
            dtpFechaEntrega.Format = DateTimePickerFormat.Custom;
            dtpFechaPedido.CustomFormat = "dd/MM/yyyy";
            dtpFechaEntrega.CustomFormat = "dd/MM/yyyy";
            dtpFechaPedido.Value = DateTime.Now;
            dtpFechaEntrega.Value = DateTime.Now;
        }

        private bool NoEsDigito(KeyPressEventArgs e)
        {
            bool noEsDigito = false;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                noEsDigito = true;
            }

            return noEsDigito;
        }


    }
}

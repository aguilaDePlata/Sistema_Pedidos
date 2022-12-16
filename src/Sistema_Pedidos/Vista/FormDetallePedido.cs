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
    public partial class frmDetallePedido : Form
    {
        public frmDetallePedido()
        {
            InitializeComponent();
        }

        private void frmDetallePedido_Load(object sender, EventArgs e)
        {
            MostrarGridPedidos();
        }

        private void dgvPedido_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1 &&
                    dgvPedido.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                MostrarGridDetallesPedido(Convert.ToInt32(dgvPedido.Rows[e.RowIndex].Cells["Id_Pedido"].Value));
            }
        }

        private void MostrarGridPedidos()
        {
            dgvPedido.DataSource = null;
            dgvPedido.DataSource = ConsultarPedidos();

            this.dgvPedido.Columns["Id_Pedido"].Width = 35;
            this.dgvPedido.Columns["NombreCliente"].Width = 210;
            this.dgvPedido.Columns["Fecha_Pedido"].Width = 80;
            this.dgvPedido.Columns["Fecha_MaxEntrega"].Width = 80;
            this.dgvPedido.Columns["Valor_Total"].Width = 70;
            this.dgvPedido.Columns["Activo"].Width = 40;

            this.dgvPedido.Columns["Id_Pedido"].HeaderText = "Id Ped.";
            this.dgvPedido.Columns["NombreCliente"].HeaderText = "Cliente";
            this.dgvPedido.Columns["Fecha_Pedido"].HeaderText = "F. Pedido";
            this.dgvPedido.Columns["Fecha_MaxEntrega"].HeaderText = "F. Entrega";
            this.dgvPedido.Columns["Valor_Total"].HeaderText = "Total";

            this.dgvPedido.Columns["Id_Cliente"].Visible = false;
            this.dgvPedido.Columns["Id_Empleado"].Visible = false;

            this.dgvPedido.Columns["Valor_Total"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvPedido.Columns["Valor_Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void MostrarGridDetallesPedido(int pedidoId)
        {
            dgvDetallesPedido.DataSource = null;
            dgvDetallesPedido.DataSource = ConsultarDetallesPedido(pedidoId);

            this.dgvDetallesPedido.Columns["ID_Detalle"].Width = 35;
            this.dgvDetallesPedido.Columns["Producto"].Width = 290;
            this.dgvDetallesPedido.Columns["Cantidad"].Width = 60;
            this.dgvDetallesPedido.Columns["Precio_Venta"].Width = 80;
            this.dgvDetallesPedido.Columns["Subtotal_Prod"].Width = 70;


            this.dgvDetallesPedido.Columns["ID_Detalle"].HeaderText = "Item";
            this.dgvDetallesPedido.Columns["Producto"].HeaderText = "Producto";
            this.dgvDetallesPedido.Columns["Precio_Venta"].HeaderText = "Precio";
            this.dgvDetallesPedido.Columns["Subtotal_Prod"].HeaderText = "SubTotal";

            this.dgvDetallesPedido.Columns["Id_Pedido"].Visible = false;
            this.dgvDetallesPedido.Columns["Id_Producto"].Visible = false;

            this.dgvDetallesPedido.Columns["Cantidad"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvDetallesPedido.Columns["Cantidad"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvDetallesPedido.Columns["Precio_Venta"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvDetallesPedido.Columns["Precio_Venta"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvDetallesPedido.Columns["Subtotal_Prod"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvDetallesPedido.Columns["Subtotal_Prod"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private List<PedidoDto> ConsultarPedidos()
        {
            using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
            {
                var pedidos = (from p in bd.Pedido
                               join c in bd.Cliente on p.ID_Cliente equals c.ID_Cliente
                               orderby p.ID_Pedido descending
                               select new PedidoDto
                               {
                                   Id_Pedido = p.ID_Pedido,
                                   NombreCliente = c.Nombre + " " + c.Apellidos,
                                   Fecha_Pedido = p.Fecha_Pedido,
                                   Fecha_MaxEntrega = p.Fecha_MaxEntrega,
                                   Valor_Total = p.Valor_Total,
                                   Activo = p.Activo
                               }).ToList();

                return pedidos;
            }
        }

        private List<Detalle_PedidoDto> ConsultarDetallesPedido(int pedidoId)
        {
            using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
            {
                var productoBuscado = (from d in bd.Detalle_Pedido
                                       join p in bd.Producto on d.ID_Producto equals p.ID_Producto
                                       where d.ID_Pedido == pedidoId
                                       orderby d.ID_Detalle ascending
                                       select new Detalle_PedidoDto
                                       {
                                           ID_Detalle = d.ID_Detalle,
                                           Producto = p.Descripcion,
                                           Cantidad = d.Cantidad,
                                           Precio_Venta = d.Precio_Venta,
                                           Subtotal_Prod = d.Subtotal_Prod
                                       }).ToList();

                return productoBuscado;
            }
        }
    }
}

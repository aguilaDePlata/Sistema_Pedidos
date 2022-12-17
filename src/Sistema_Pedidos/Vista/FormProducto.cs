using Sistema_Pedidos.Vista;
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
    public partial class frmProducto : Form
    {
        public frmProducto()
        {
            InitializeComponent();
        }

        public List<ProductoDto> ObtenerProductos()
        {
            try
            {
                using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
                {
                    var consulta = (from n in bd.Producto
                                    orderby n.Nombre_Producto ascending
                                    select new ProductoDto
                                    {
                                        Id_Producto = n.ID_Producto,
                                        Nombre_Producto = n.Nombre_Producto,
                                        Descripcion = n.Descripcion,
                                        Precio_Venta = n.Precio_Venta.ToString(),
                                        Id_Marca = (int)n.ID_Marca,
                                        Id_Modelo = (int)n.ID_Modelo,
                                        Id_Proveedor = (int)n.ID_Proveedor,
                                        Stock = (int)n.Stock,
                                        Activo = n.Activo
                                    }).ToList();
                    return consulta;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void LimpiarFormulario()
        {
            txtId.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrecioVenta.Text = "";
            txtIdMarca.Text = "";
            cmbMarca.Text = "";
            txtIdModelo.Text = "";
            cmbModelo.Text = "";
            txtIdProveedor.Text = "";
            cmbProveedor.Text = "";
            txtStock.Text = "";
            chkActivo.Checked = false;
            txtNombre.Focus();
        }

        private bool FormularioValido()
        {
            bool valido = false;
            string nombre = txtNombre.Text.Trim();
            string descripcion = txtDescripcion.Text.Trim();
            string precioventa = txtPrecioVenta.Text.Trim();
            string idmarca = txtIdMarca.Text.Trim();
            string idmodelo = txtIdModelo.Text.Trim();
            string idproveedor = txtIdProveedor.Text.Trim();
            string stock = txtStock.Text.Trim();
            if (nombre.Length > 0 && descripcion.Length > 0 && precioventa.Length > 0 &&
                idmarca.Length > 0 && idmodelo.Length > 0 && idproveedor.Length > 0 && stock.Length > 0)
            {
                if (EsNumero(precioventa))
                {
                    if (EsNumero(stock))
                    {
                        valido = true;
                    }
                    else
                    {
                        MessageBox.Show("El Stock debe ser un número.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtStock.Text = "";
                        txtStock.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("El Precio de venta debe ser un número.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPrecioVenta.Text = "";
                    txtPrecioVenta.Focus();
                }
            }
            return valido;
        }

        private bool EsNumero(string pNumero)
        {
            bool esNumero = true;

            try
            {
                decimal numero = Convert.ToDecimal(pNumero);
                return esNumero;
            }
            catch (Exception)
            {
                esNumero = false;
                return esNumero;
            }
        }

        public void InsertarProducto(Producto pProducto)
        {
            try
            {
                using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
                {
                    bd.Producto.Add(pProducto);
                    bd.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProductoDto ObtenerUltimoProducto()
        {
            try
            {
                using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
                {
                    var consulta = (from n in bd.Producto
                                    orderby n.ID_Producto descending
                                    select new ProductoDto
                                    {
                                        Id_Producto = n.ID_Producto,
                                        Nombre_Producto = n.Nombre_Producto,
                                        Descripcion = n.Descripcion,
                                        Precio_Venta = n.Precio_Venta.ToString(),
                                        Id_Marca = (int)n.ID_Marca,
                                        Id_Modelo = (int)n.ID_Modelo,
                                        Id_Proveedor = (int)n.ID_Proveedor,
                                        Stock = (int)n.Stock,
                                        Activo = n.Activo
                                    }).ToList();
                    ProductoDto productoDto = consulta.First();
                    return productoDto;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ActualizarProducto(Producto pProducto)
        {
            try
            {
                using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
                {
                    var productos = from n in bd.Producto
                                   where n.ID_Producto == pProducto.ID_Producto
                                   select n;
                    foreach (Producto prod in productos)
                    {
                        prod.Nombre_Producto = pProducto.Nombre_Producto;
                        prod.Descripcion = pProducto.Descripcion;
                        prod.Precio_Venta = pProducto.Precio_Venta;
                        prod.ID_Marca = pProducto.ID_Marca;
                        prod.ID_Modelo = pProducto.ID_Modelo;
                        prod.ID_Proveedor = pProducto.ID_Proveedor;
                        prod.Stock = pProducto.Stock;
                        prod.Activo = pProducto.Activo;
                    }
                    bd.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void EliminarProducto(Producto pProducto)
        {
            try
            {
                using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
                {
                    var productos = from n in bd.Producto
                                   where n.ID_Producto == pProducto.ID_Producto
                                   select n;
                    foreach (Producto prod in productos)
                    {
                        bd.Producto.Remove(prod);
                    }
                    bd.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void frmProducto_Load(object sender, EventArgs e)
        {
            dgvProductos.DataSource = ObtenerProductos();
            llenar_cmbMarca();
            llenar_cmbModelo();
            llenar_cmbProveedor();
        }

        public List<MarcaDto> ObtenerMarcas()
        {
            try
            {
                using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
                {
                    var consulta = (from n in bd.Marca
                                    orderby n.Marca1 ascending
                                    select new MarcaDto
                                    {
                                        Id_Marca = n.ID_Marca,
                                        Marca = n.Marca1
                                    }).ToList();
                    return consulta;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void llenar_cmbMarca()
        {
            cmbMarca.ValueMember = "Id_Marca";
            cmbMarca.DisplayMember = "Marca";
            cmbMarca.DataSource = ObtenerMarcas();
            txtIdMarca.Text = cmbMarca.SelectedValue.ToString();
        }

        private void cmbMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtIdMarca.Text = cmbMarca.SelectedValue.ToString();
        }

        public List<ModeloDto> ObtenerModelos()
        {
            try
            {
                using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
                {
                    var consulta = (from n in bd.Modelo
                                    orderby n.Modelo1 ascending
                                    select new ModeloDto
                                    {
                                        Id_Modelo = n.ID_Modelo,
                                        Modelo = n.Modelo1
                                    }).ToList();
                    return consulta;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void llenar_cmbModelo()
        {
            cmbModelo.ValueMember = "Id_Modelo";
            cmbModelo.DisplayMember = "Modelo";
            cmbModelo.DataSource = ObtenerModelos();
            txtIdModelo.Text = cmbModelo.SelectedValue.ToString();
        }

        private void cmbModelo_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtIdModelo.Text = cmbModelo.SelectedValue.ToString();
        }

        public List<ProveedorDto> ObtenerProveedores()
        {
            try
            {
                using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
                {
                    var consulta = (from n in bd.Proveedor
                                    orderby n.Nombres_Prov ascending
                                    select new ProveedorDto
                                    {
                                        Id_Proveedor = n.ID_Proveedor,
                                        Nombres_Prov = n.Nombres_Prov,
                                        Apellidos_Prov = n.Apellidos_Prov,
                                        Direccion = n.Direccion,
                                        Distrito = n.Distrito,
                                        Telefono = n.Telefono,
                                        Activo = n.Activo
                                    }).ToList();
                    return consulta;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void llenar_cmbProveedor()
        {
            cmbProveedor.ValueMember = "Id_Proveedor";
            cmbProveedor.DisplayMember = "Nombres_Prov";
            cmbProveedor.DataSource = ObtenerProveedores();
            txtIdProveedor.Text = cmbProveedor.SelectedValue.ToString();
        }

        private void cmbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtIdProveedor.Text = cmbProveedor.SelectedValue.ToString();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (FormularioValido())
            {
                Producto producto = new Producto();
                producto.ID_Producto = 0;
                producto.Nombre_Producto = txtNombre.Text.Trim();
                producto.Descripcion = txtDescripcion.Text.Trim();
                producto.Precio_Venta = Convert.ToDecimal(txtPrecioVenta.Text);
                producto.ID_Marca = Convert.ToInt32(txtIdMarca.Text);
                producto.ID_Modelo = Convert.ToInt32(txtIdModelo.Text);
                producto.ID_Proveedor = Convert.ToInt32(txtIdProveedor.Text);
                producto.Stock = Convert.ToInt32(txtStock.Text);
                producto.Activo = chkActivo.Checked;
                //Modificar Producto
                if (txtId.Text.Trim().Length > 0)
                {
                    //Código para modificar un Producto
                    producto.ID_Producto = Convert.ToInt32(txtId.Text);
                    ActualizarProducto(producto);
                }
                //Nuevo Producto
                else
                {
                    InsertarProducto(producto);
                    txtId.Text = ObtenerUltimoProducto().Id_Producto.ToString();
                }
                MessageBox.Show("Datos guardados satisfactoriamente", "Ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvProductos.DataSource = ObtenerProductos();
                LimpiarFormulario();
            }
            else
            {
                MessageBox.Show("Todos los datos son obligatorios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNombre.Focus();
            }
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1 && dgvProductos.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                var indiceMarca = Convert.ToInt32(dgvProductos.Rows[e.RowIndex].Cells["ID_Marca"].Value);
                var indiceModelo = Convert.ToInt32(dgvProductos.Rows[e.RowIndex].Cells["ID_Modelo"].Value);
                var indiceProveedor = Convert.ToInt32(dgvProductos.Rows[e.RowIndex].Cells["ID_Proveedor"].Value);
                dgvProductos.CurrentRow.Selected = true;
                txtId.Text = dgvProductos.Rows[e.RowIndex].Cells["ID_Producto"].FormattedValue.ToString();
                txtNombre.Text = dgvProductos.Rows[e.RowIndex].Cells["Nombre_Producto"].FormattedValue.ToString();
                txtDescripcion.Text = dgvProductos.Rows[e.RowIndex].Cells["Descripcion"].FormattedValue.ToString();
                txtPrecioVenta.Text = dgvProductos.Rows[e.RowIndex].Cells["Precio_Venta"].FormattedValue.ToString();
                txtIdMarca.Text = indiceMarca.ToString();
                txtIdModelo.Text = indiceModelo.ToString();
                txtIdProveedor.Text = indiceProveedor.ToString();
                cmbMarca.SelectedValue = indiceMarca;
                cmbModelo.SelectedValue = indiceModelo;
                cmbProveedor.SelectedValue = indiceProveedor;
                txtStock.Text = dgvProductos.Rows[e.RowIndex].Cells["Stock"].FormattedValue.ToString();
                chkActivo.Checked = false;
                if (dgvProductos.Rows[e.RowIndex].Cells["Activo"].FormattedValue.ToString() == "True")
                    chkActivo.Checked = true;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtId.Text.Trim().Length > 0)
            {
                DialogResult respuestaAdvertencia = DialogResult.OK;
                respuestaAdvertencia = MessageBox.Show("¿Está seguro de eliminar el Producto?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuestaAdvertencia == DialogResult.Yes)
                {
                    Producto producto = new Producto();
                    producto.ID_Producto = Convert.ToInt32(txtId.Text);
                    EliminarProducto(producto);
                    dgvProductos.DataSource = ObtenerProductos();
                    LimpiarFormulario();
                    MessageBox.Show("Registro eliminado satisfactoriamente", "Ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar el registro que desea eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPrecioVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = HelperValidaciones.NoEsParteNumeroDecimal(sender, e);
        }

        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = HelperValidaciones.NoEsDigito(e);
        }
    }
}

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
    public partial class frmProveedor : Form
    {
        public frmProveedor()
        {
            InitializeComponent();
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

        private void LimpiarFormulario()
        {
            txtId.Text = "";
            txtNombres.Text = "";
            txtApellidos.Text = "";
            txtDireccion.Text = "";
            txtDistrito.Text = "";
            txtTelefono.Text = "";
            chkActivo.Checked = false;
            txtNombres.Focus();
        }

        private bool FormularioValido()
        {
            bool valido = false;
            string nombres = txtNombres.Text.Trim();
            string apellidos = txtApellidos.Text.Trim();
            string direccion = txtDireccion.Text.Trim();
            string distrito = txtDistrito.Text.Trim();
            string telefono = txtTelefono.Text.Trim();
            if (nombres.Length > 0 && apellidos.Length > 0 && direccion.Length > 0 && distrito.Length > 0 && telefono.Length > 0)
                valido = true;
            return valido;
        }

        public void InsertarProveedor(Proveedor pProveedor)
        {
            try
            {
                using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
                {
                    bd.Proveedor.Add(pProveedor);
                    bd.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProveedorDto ObtenerUltimoProveedor()
        {
            try
            {
                using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
                {
                    var consulta = (from n in bd.Proveedor
                                    orderby n.ID_Proveedor descending
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
                    ProveedorDto proveedorDto = consulta.First();
                    return proveedorDto;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ActualizarProveedor(Proveedor pProveedor)
        {
            try
            {
                using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
                {
                    var proveedores = from n in bd.Proveedor
                                    where n.ID_Proveedor == pProveedor.ID_Proveedor
                                    select n;
                    foreach (Proveedor prov in proveedores)
                    {
                        prov.Nombres_Prov = pProveedor.Nombres_Prov;
                        prov.Apellidos_Prov = pProveedor.Apellidos_Prov;
                        prov.Direccion = pProveedor.Direccion;
                        prov.Distrito = pProveedor.Distrito;
                        prov.Telefono = pProveedor.Telefono;
                        prov.Activo = pProveedor.Activo;
                    }
                    bd.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void EliminarProveedor(Proveedor pProveedor)
        {
            try
            {
                using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
                {
                    var proveedores = from n in bd.Proveedor
                                   where n.ID_Proveedor == pProveedor.ID_Proveedor
                                   select n;
                    foreach (Proveedor prov in proveedores)
                    {
                        bd.Proveedor.Remove(prov);
                    }
                    bd.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void frmProveedor_Load(object sender, EventArgs e)
        {
            dgvProveedores.DataSource = ObtenerProveedores();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (FormularioValido())
            {
                Proveedor proveedor = new Proveedor();
                proveedor.ID_Proveedor = 0;
                proveedor.Nombres_Prov = txtNombres.Text.Trim();
                proveedor.Apellidos_Prov = txtApellidos.Text.Trim();
                proveedor.Direccion = txtDireccion.Text.Trim();
                proveedor.Distrito = txtDistrito.Text.Trim();
                proveedor.Telefono = txtTelefono.Text.Trim();
                proveedor.Activo = chkActivo.Checked;
                //Modificar Proveedor
                if (txtId.Text.Trim().Length > 0)
                {
                    //Código para modificar un Proveedor
                    proveedor.ID_Proveedor = Convert.ToInt32(txtId.Text);
                    ActualizarProveedor(proveedor);
                }
                //Nuevo Proveedor
                else
                {
                    InsertarProveedor(proveedor);
                    txtId.Text = ObtenerUltimoProveedor().Id_Proveedor.ToString();
                }
                MessageBox.Show("Datos guardados satisfactoriamente", "Ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvProveedores.DataSource = ObtenerProveedores();
                LimpiarFormulario();
            }
            else
            {
                MessageBox.Show("Todos los datos son obligatorios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNombres.Focus();
            }
        }

        private void dgvProveedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1 && dgvProveedores.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dgvProveedores.CurrentRow.Selected = true;
                txtId.Text = dgvProveedores.Rows[e.RowIndex].Cells["ID_Proveedor"].FormattedValue.ToString();
                txtNombres.Text = dgvProveedores.Rows[e.RowIndex].Cells["Nombres_Prov"].FormattedValue.ToString();
                txtApellidos.Text = dgvProveedores.Rows[e.RowIndex].Cells["Apellidos_Prov"].FormattedValue.ToString();
                txtDireccion.Text = dgvProveedores.Rows[e.RowIndex].Cells["Direccion"].FormattedValue.ToString();
                txtDistrito.Text = dgvProveedores.Rows[e.RowIndex].Cells["Distrito"].FormattedValue.ToString();
                txtTelefono.Text = dgvProveedores.Rows[e.RowIndex].Cells["Telefono"].FormattedValue.ToString();
                chkActivo.Checked = false;
                if (dgvProveedores.Rows[e.RowIndex].Cells["Activo"].FormattedValue.ToString() == "True")
                    chkActivo.Checked = true;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtId.Text.Trim().Length > 0)
            {
                DialogResult respuestaAdvertencia = DialogResult.OK;
                respuestaAdvertencia = MessageBox.Show("¿Está seguro de eliminar el Proveedor?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuestaAdvertencia == DialogResult.Yes)
                {
                    Proveedor proveedor = new Proveedor();
                    proveedor.ID_Proveedor = Convert.ToInt32(txtId.Text);
                    EliminarProveedor(proveedor);
                    dgvProveedores.DataSource = ObtenerProveedores();
                    LimpiarFormulario();
                    MessageBox.Show("Registro eliminado satisfactoriamente", "Ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar el registro que desea eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = HelperValidaciones.NoEsDigito(e);
        }
    }
}

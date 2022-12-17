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
    public partial class frmCliente : Form
    {
        public frmCliente()
        {
            InitializeComponent();
        }

        public List<ClienteDto> ObtenerClientes()
        {
            try
            {
                using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
                {
                    var consulta = (from n in bd.Cliente
                                    orderby n.Nombre ascending
                                    select new ClienteDto
                                    {
                                        Id_Cliente = n.ID_Cliente,
                                        Nombre = n.Nombre,
                                        Apellidos = n.Apellidos,
                                        Tipo_Cliente = n.Tipo_Cliente,
                                        Nro_Documento = n.Nro_Documento,
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
            txtTipo.Text = "";
            txtDocumento.Text = "";
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
            string tipo = txtTipo.Text.Trim();
            string documento = txtDocumento.Text.Trim();
            string direccion = txtDireccion.Text.Trim();
            string distrito = txtDistrito.Text.Trim();
            string telefono = txtTelefono.Text.Trim();
            if (nombres.Length > 0 && apellidos.Length > 0 && tipo.Length > 0 &&
                documento.Length > 0 && direccion.Length > 0 && distrito.Length > 0 && telefono.Length > 0)
                valido = true;
            return valido;
        }

        public void InsertarCliente(Cliente pCliente)
        {
            try
            {
                using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
                {
                    bd.Cliente.Add(pCliente);
                    bd.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ClienteDto ObtenerUltimoCliente()
        {
            try
            {
                using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
                {
                    var consulta = (from n in bd.Cliente
                                    orderby n.ID_Cliente descending
                                    select new ClienteDto
                                    {
                                        Id_Cliente = n.ID_Cliente,
                                        Nombre = n.Nombre,
                                        Apellidos = n.Apellidos,
                                        Tipo_Cliente = n.Tipo_Cliente,
                                        Nro_Documento = n.Nro_Documento,
                                        Direccion = n.Direccion,
                                        Distrito = n.Distrito,
                                        Telefono = n.Telefono,
                                        Activo = n.Activo
                                    }).ToList();
                    ClienteDto clienteDto = consulta.First();
                    return clienteDto;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ActualizarCliente(Cliente pCliente)
        {
            try
            {
                using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
                {
                    var clientes = from n in bd.Cliente
                                    where n.ID_Cliente == pCliente.ID_Cliente
                                    select n;
                    foreach (Cliente cli in clientes)
                    {
                        cli.Nombre = pCliente.Nombre;
                        cli.Apellidos = pCliente.Apellidos;
                        cli.Tipo_Cliente = pCliente.Tipo_Cliente;
                        cli.Nro_Documento = pCliente.Nro_Documento;
                        cli.Direccion = pCliente.Direccion;
                        cli.Distrito = pCliente.Distrito;
                        cli.Telefono = pCliente.Telefono;
                        cli.Activo = pCliente.Activo;
                    }
                    bd.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void EliminarCliente(Cliente pCliente)
        {
            try
            {
                using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
                {
                    var clientes = from n in bd.Cliente
                                   where n.ID_Cliente == pCliente.ID_Cliente
                                   select n;
                    foreach (Cliente cli in clientes)
                    {
                        bd.Cliente.Remove(cli);
                    }
                    bd.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void frmCliente_Load(object sender, EventArgs e)
        {
            dgvClientes.DataSource = ObtenerClientes();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void txtDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = HelperValidaciones.NoEsDigito(e);
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = HelperValidaciones.NoEsDigito(e);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (FormularioValido())
            {
                Cliente cliente = new Cliente();
                cliente.ID_Cliente = 0;
                cliente.Nombre = txtNombres.Text.Trim();
                cliente.Apellidos = txtApellidos.Text.Trim();
                cliente.Tipo_Cliente = txtTipo.Text.Trim();
                cliente.Nro_Documento = txtDocumento.Text.Trim();
                cliente.Direccion = txtDireccion.Text.Trim();
                cliente.Distrito = txtDistrito.Text.Trim();
                cliente.Telefono = txtTelefono.Text.Trim();
                cliente.Activo = chkActivo.Checked;
                //Modificar Cliente
                if (txtId.Text.Trim().Length > 0)
                {
                    //Código para modificar un Cliente
                    cliente.ID_Cliente = Convert.ToInt32(txtId.Text);
                    ActualizarCliente(cliente);
                }
                //Nuevo Cliente
                else
                {
                    InsertarCliente(cliente);
                    txtId.Text = ObtenerUltimoCliente().Id_Cliente.ToString();
                }
                MessageBox.Show("Datos guardados satisfactoriamente", "Ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvClientes.DataSource = ObtenerClientes();
                LimpiarFormulario();
            }
            else
            {
                MessageBox.Show("Todos los datos son obligatorios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNombres.Focus();
            }
        }

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1 && dgvClientes.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dgvClientes.CurrentRow.Selected = true;
                txtId.Text = dgvClientes.Rows[e.RowIndex].Cells["ID_Cliente"].FormattedValue.ToString();
                txtNombres.Text = dgvClientes.Rows[e.RowIndex].Cells["Nombre"].FormattedValue.ToString();
                txtApellidos.Text = dgvClientes.Rows[e.RowIndex].Cells["Apellidos"].FormattedValue.ToString();
                txtTipo.Text = dgvClientes.Rows[e.RowIndex].Cells["Tipo_Cliente"].FormattedValue.ToString();
                txtDocumento.Text = dgvClientes.Rows[e.RowIndex].Cells["Nro_Documento"].FormattedValue.ToString();
                txtDireccion.Text = dgvClientes.Rows[e.RowIndex].Cells["Direccion"].FormattedValue.ToString();
                txtDistrito.Text = dgvClientes.Rows[e.RowIndex].Cells["Distrito"].FormattedValue.ToString();
                txtTelefono.Text = dgvClientes.Rows[e.RowIndex].Cells["Telefono"].FormattedValue.ToString();
                chkActivo.Checked = false;
                if (dgvClientes.Rows[e.RowIndex].Cells["Activo"].FormattedValue.ToString() == "True")
                    chkActivo.Checked = true;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtId.Text.Trim().Length > 0)
            {
                DialogResult respuestaAdvertencia = DialogResult.OK;
                respuestaAdvertencia = MessageBox.Show("¿Está seguro de eliminar el Cliente?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuestaAdvertencia == DialogResult.Yes)
                {
                    Cliente cliente = new Cliente();
                    cliente.ID_Cliente = Convert.ToInt32(txtId.Text);
                    EliminarCliente(cliente);
                    dgvClientes.DataSource = ObtenerClientes();
                    LimpiarFormulario();
                    MessageBox.Show("Registro eliminado satisfactoriamente", "Ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar el registro que desea eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
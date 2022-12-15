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
    public partial class frmEmpleado : Form
    {
        public frmEmpleado()
        {
            InitializeComponent();
        }

        public List<EmpleadoDto> ObtenerEmpleados()
        {
            try
            {
                using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
                {
                    var consulta = (from n in bd.Empleado
                                    orderby n.Nombres ascending
                                    select new EmpleadoDto
                                    {
                                        Id_Empleado = n.ID_Empleado,
                                        Nombres = n.Nombres,
                                        Apellidos = n.Apellidos,
                                        Dni = (int)n.DNI,
                                        Direccion = n.Direccion,
                                        Distrito = n.Distrito,
                                        Edad = (int)n.Edad,
                                        Telefono = n.Telefono,
                                        Id_Cargo = (int)n.ID_Cargo,
                                        Fecha_Contrato = (DateTime)n.Fecha_Contrato,
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
            txtDni.Text = "";
            txtDireccion.Text = "";
            txtDistrito.Text = "";
            txtEdad.Text = "";
            txtTelefono.Text = "";
            txtIdCargo.Text = "";
            cmbCargo.Text = "";
            dtpFContrato.Value = DateTime.Now;
            chkActivo.Checked = false;
            txtNombres.Focus();
        }

        private bool FormularioValido()
        {
            bool valido = false;
            string nombres = txtNombres.Text.Trim();
            string apellidos = txtApellidos.Text.Trim();
            string dni = txtDni.Text.Trim();
            string direccion = txtDireccion.Text.Trim();
            string distrito = txtDistrito.Text.Trim();
            string edad = txtEdad.Text.Trim();
            string telefono = txtTelefono.Text.Trim();
            string idcargo = txtIdCargo.Text.Trim();
            string fcontrato = dtpFContrato.Text.Trim();
            if (nombres.Length > 0 && apellidos.Length > 0 && dni.Length > 0 && direccion.Length > 0 && distrito.Length > 0 &&
                edad.Length > 0 && telefono.Length > 0 && idcargo.Length > 0 && fcontrato.Length > 0)
            {
                if (EsNumero(dni))
                {
                    if (EsNumero(edad))
                    {
                        valido = true;
                    }
                    else
                    {
                        MessageBox.Show("La edad debe ser un número.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtEdad.Text = "";
                        txtEdad.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("El Dni debe ser un número.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDni.Text = "";
                    txtDni.Focus();
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

        public void InsertarEmpleado(Empleado pEmpleado)
        {
            try
            {
                using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
                {
                    bd.Empleado.Add(pEmpleado);
                    bd.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public EmpleadoDto ObtenerUltimoEmpleado()
        {
            try
            {
                using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
                {
                    var consulta = (from n in bd.Empleado
                                    orderby n.ID_Empleado descending
                                    select new EmpleadoDto
                                    {
                                        Id_Empleado = n.ID_Empleado,
                                        Nombres = n.Nombres,
                                        Apellidos = n.Apellidos,
                                        Dni = (int)n.DNI,
                                        Direccion = n.Direccion,
                                        Distrito = n.Distrito,
                                        Edad = (int)n.Edad,
                                        Telefono = n.Telefono,
                                        Id_Cargo = (int)n.ID_Cargo,
                                        Fecha_Contrato = (DateTime)n.Fecha_Contrato,
                                        Activo = n.Activo
                                    }).ToList();
                    EmpleadoDto EmpleadoDto = consulta.First();
                    return EmpleadoDto;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ActualizarEmpleado(Empleado pEmpleado)
        {
            try
            {
                using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
                {
                    var Empleados = from n in bd.Empleado
                                  where n.ID_Empleado == pEmpleado.ID_Empleado
                                  select n;
                    foreach (Empleado emp in Empleados)
                    {
                        emp.Nombres = pEmpleado.Nombres;
                        emp.Apellidos = pEmpleado.Apellidos;
                        emp.DNI = pEmpleado.DNI;
                        emp.Direccion = pEmpleado.Direccion;
                        emp.Distrito = pEmpleado.Distrito;
                        emp.Edad = pEmpleado.Edad;
                        emp.Telefono = pEmpleado.Telefono;
                        emp.ID_Cargo = pEmpleado.ID_Cargo;
                        emp.Fecha_Contrato = pEmpleado.Fecha_Contrato;
                        emp.Activo = pEmpleado.Activo;
                    }
                    bd.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void EliminarEmpleado(Empleado pEmpleado)
        {
            try
            {
                using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
                {
                    var Empleados = from n in bd.Empleado
                                  where n.ID_Empleado == pEmpleado.ID_Empleado
                                  select n;
                    foreach (Empleado emp in Empleados)
                    {
                        bd.Empleado.Remove(emp);
                    }
                    bd.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void frmEmpleado_Load(object sender, EventArgs e)
        {
            dgvEmpleados.DataSource = ObtenerEmpleados();
            llenar_cmbCargo();
        }

        public List<CargoDto> ObtenerCargos()
        {
            try
            {
                using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
                {
                    var consulta = (from n in bd.Cargo
                                    orderby n.ID_Cargo ascending
                                    select new CargoDto
                                    {
                                        Id_Cargo = n.ID_Cargo,
                                        Cargo = n.Cargo1,
                                        Sueldo = n.Sueldo.ToString()
                                    }).ToList();
                    return consulta;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void llenar_cmbCargo()
        {
            cmbCargo.ValueMember = "Id_Cargo";
            cmbCargo.DisplayMember = "Cargo";
            cmbCargo.DataSource = ObtenerCargos();
            txtIdCargo.Text = cmbCargo.SelectedValue.ToString();
        }

        private void cmbCargo_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtIdCargo.Text = cmbCargo.SelectedValue.ToString();
        }

        private void txtIdCargo_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (FormularioValido())
            {
                Empleado empleado = new Empleado();
                empleado.ID_Empleado = 0;
                empleado.Nombres = txtNombres.Text.Trim();
                empleado.Apellidos = txtApellidos.Text.Trim();
                empleado.DNI = Convert.ToInt32(txtDni.Text);
                empleado.Direccion = txtDireccion.Text.Trim();
                empleado.Distrito = txtDistrito.Text.Trim();
                empleado.Edad = Convert.ToInt32(txtEdad.Text);
                empleado.Telefono = txtTelefono.Text.Trim();
                empleado.ID_Cargo = Convert.ToInt32(txtIdCargo.Text);
                empleado.Fecha_Contrato = dtpFContrato.Value.Date;
                empleado.Activo = chkActivo.Checked;
                //Modificar Empleado
                if (txtId.Text.Trim().Length > 0)
                {
                    //Código para modificar un Empleado
                    empleado.ID_Empleado = Convert.ToInt32(txtId.Text);
                    ActualizarEmpleado(empleado);
                }
                //Nuevo Empleado
                else
                {
                    InsertarEmpleado(empleado);
                    txtId.Text = ObtenerUltimoEmpleado().Id_Empleado.ToString();
                }
                MessageBox.Show("Datos guardados satisfactoriamente", "Ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvEmpleados.DataSource = ObtenerEmpleados();
                LimpiarFormulario();
            }
            else
            {
                MessageBox.Show("Todos los datos son obligatorios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNombres.Focus();
            }
        }

        private void dgvEmpleados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1 && dgvEmpleados.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dgvEmpleados.CurrentRow.Selected = true;
                txtId.Text = dgvEmpleados.Rows[e.RowIndex].Cells["ID_Empleado"].FormattedValue.ToString();
                txtNombres.Text = dgvEmpleados.Rows[e.RowIndex].Cells["Nombres"].FormattedValue.ToString();
                txtApellidos.Text = dgvEmpleados.Rows[e.RowIndex].Cells["Apellidos"].FormattedValue.ToString();
                txtDni.Text = dgvEmpleados.Rows[e.RowIndex].Cells["DNI"].FormattedValue.ToString();
                txtDireccion.Text = dgvEmpleados.Rows[e.RowIndex].Cells["Direccion"].FormattedValue.ToString();
                txtDistrito.Text = dgvEmpleados.Rows[e.RowIndex].Cells["Distrito"].FormattedValue.ToString();
                txtEdad.Text = dgvEmpleados.Rows[e.RowIndex].Cells["Edad"].FormattedValue.ToString();
                txtTelefono.Text = dgvEmpleados.Rows[e.RowIndex].Cells["Telefono"].FormattedValue.ToString();
                txtIdCargo.Text = dgvEmpleados.Rows[e.RowIndex].Cells["ID_Cargo"].FormattedValue.ToString();
                dtpFContrato.Value = Convert.ToDateTime(dgvEmpleados.Rows[e.RowIndex].Cells["Fecha_Contrato"].Value);
                chkActivo.Checked = false;
                if (dgvEmpleados.Rows[e.RowIndex].Cells["Activo"].FormattedValue.ToString() == "True")
                    chkActivo.Checked = true;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtId.Text.Trim().Length > 0)
            {
                DialogResult respuestaAdvertencia = DialogResult.OK;
                respuestaAdvertencia = MessageBox.Show("¿Está seguro de eliminar el empleado?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuestaAdvertencia == DialogResult.Yes)
                {
                    Empleado empleado = new Empleado();
                    empleado.ID_Empleado = Convert.ToInt32(txtId.Text);
                    EliminarEmpleado(empleado);
                    dgvEmpleados.DataSource = ObtenerEmpleados();
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

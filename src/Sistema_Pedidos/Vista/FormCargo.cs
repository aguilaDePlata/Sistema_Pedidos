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
    public partial class frmCargo : Form
    {
        public frmCargo()
        {
            InitializeComponent();
        }

        public List<CargoDto> ObtenerCargos()
        {
            try
            {
                using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
                {
                    var consulta = (from n in bd.Cargo
                                    orderby n.Cargo1 ascending
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

        private void LimpiarFormulario()
        {
            txtId.Text = "";
            txtCargo.Text = "";
            txtSueldo.Text = "";
            txtCargo.Focus();
        }

        private bool FormularioValido()
        {
            bool valido = false;
            string cargo = txtCargo.Text.Trim();
            string sueldo = txtSueldo.Text.Trim();
            if (cargo.Length > 0 && sueldo.Length > 0)
            {
                if (EsNumero(sueldo))
                {
                    valido = true;
                }
                else
                {
                    MessageBox.Show("El Sueldo debe ser un número.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSueldo.Text = "";
                    txtSueldo.Focus();
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

        public void InsertarCargo(Cargo pCargo)
        {
            try
            {
                using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
                {
                    bd.Cargo.Add(pCargo);
                    bd.SaveChanges();
                }

                MessageBox.Show("Pedido grabado exitosamente.", "Nuevo Pedido", MessageBoxButtons.OK, MessageBoxIcon.Information);     
            }
            catch (Exception)
            {
                MessageBox.Show("No se pudo grabar el pedido", "Nuevo Pedido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        public CargoDto ObtenerUltimoCargo()
        {
            try
            {
                using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
                {
                    var consulta = (from n in bd.Cargo
                                    orderby n.ID_Cargo descending
                                    select new CargoDto
                                    {
                                        Id_Cargo = n.ID_Cargo,
                                        Cargo = n.Cargo1,
                                        Sueldo = n.Sueldo.ToString()
                                    }).ToList();
                    CargoDto CargoDto = consulta.First();
                    return CargoDto;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ActualizarCargo(Cargo pCargo)
        {
            try
            {
                using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
                {
                    var Cargos = from n in bd.Cargo
                                 where n.ID_Cargo == pCargo.ID_Cargo
                                 select n;
                    foreach (Cargo car in Cargos)
                    {
                        car.Cargo1 = pCargo.Cargo1;
                        car.Sueldo = pCargo.Sueldo;
                    }
                    bd.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void EliminarCargo(Cargo pCargo)
        {
            try
            {
                using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
                {
                    var Cargos = from n in bd.Cargo
                                 where n.ID_Cargo == pCargo.ID_Cargo
                                 select n;
                    foreach (Cargo car in Cargos)
                    {
                        bd.Cargo.Remove(car);
                    }
                    bd.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void frmCargo_Load(object sender, EventArgs e)
        {
            dgvCargos.DataSource = ObtenerCargos();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (FormularioValido())
            {
                Cargo cargo = new Cargo();
                cargo.ID_Cargo = 0;
                cargo.Cargo1 = txtCargo.Text.Trim();
                cargo.Sueldo = Convert.ToDecimal(txtSueldo.Text);
                //Modificar Cargo
                if (txtId.Text.Trim().Length > 0)
                {
                    //Código para modificar un Cargo
                    cargo.ID_Cargo = Convert.ToInt32(txtId.Text);
                    ActualizarCargo(cargo);
                }
                //Nuevo Cargo
                else
                {
                    InsertarCargo(cargo);
                    txtId.Text = ObtenerUltimoCargo().Id_Cargo.ToString();
                }
                MessageBox.Show("Datos guardados satisfactoriamente", "Ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvCargos.DataSource = ObtenerCargos();
                LimpiarFormulario();
            }
            else
            {
                MessageBox.Show("Todos los datos son obligatorios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCargo.Focus();
            }
        }

        private void dgvCargos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1 && dgvCargos.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dgvCargos.CurrentRow.Selected = true;
                txtId.Text = dgvCargos.Rows[e.RowIndex].Cells["ID_Cargo"].FormattedValue.ToString();
                txtCargo.Text = dgvCargos.Rows[e.RowIndex].Cells["Cargo"].FormattedValue.ToString();
                txtSueldo.Text = dgvCargos.Rows[e.RowIndex].Cells["Sueldo"].FormattedValue.ToString();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtId.Text.Trim().Length > 0)
            {
                DialogResult respuestaAdvertencia = DialogResult.OK;
                respuestaAdvertencia = MessageBox.Show("¿Está seguro de eliminar el cargo?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuestaAdvertencia == DialogResult.Yes)
                {
                    Cargo cargo = new Cargo();
                    cargo.ID_Cargo = Convert.ToInt32(txtId.Text);
                    EliminarCargo(cargo);
                    dgvCargos.DataSource = ObtenerCargos();
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

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
    public partial class frmModelo : Form
    {
        public frmModelo()
        {
            InitializeComponent();
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

        private void LimpiarFormulario()
        {
            txtId.Text = "";
            txtModelo.Text = "";
            txtModelo.Focus();
        }

        private bool FormularioValido()
        {
            bool valido = false;
            string modelo = txtModelo.Text.Trim();
            if (modelo.Length > 0)
                valido = true;
            return valido;
        }

        public void InsertarModelo(Modelo pModelo)
        {
            try
            {
                using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
                {
                    bd.Modelo.Add(pModelo);
                    bd.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ModeloDto ObtenerUltimoModelo()
        {
            try
            {
                using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
                {
                    var consulta = (from n in bd.Modelo
                                    orderby n.ID_Modelo descending
                                    select new ModeloDto
                                    {
                                        Id_Modelo = n.ID_Modelo,
                                        Modelo = n.Modelo1
                                    }).ToList();
                    ModeloDto ModeloDto = consulta.First();
                    return ModeloDto;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ActualizarModelo(Modelo pModelo)
        {
            try
            {
                using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
                {
                    var Modelos = from n in bd.Modelo
                                 where n.ID_Modelo == pModelo.ID_Modelo
                                 select n;
                    foreach (Modelo mod in Modelos)
                    {
                        mod.Modelo1 = pModelo.Modelo1;
                    }
                    bd.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void EliminarModelo(Modelo pModelo)
        {
            try
            {
                using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
                {
                    var Modelos = from n in bd.Modelo
                                 where n.ID_Modelo == pModelo.ID_Modelo
                                 select n;
                    foreach (Modelo mod in Modelos)
                    {
                        bd.Modelo.Remove(mod);
                    }
                    bd.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void frmModelo_Load(object sender, EventArgs e)
        {
            dgvModelos.DataSource = ObtenerModelos();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (FormularioValido())
            {
                Modelo modelo = new Modelo();
                modelo.ID_Modelo = 0;
                modelo.Modelo1 = txtModelo.Text.Trim();
                //Modificar Modelo
                if (txtId.Text.Trim().Length > 0)
                {
                    //Código para modificar un Modelo
                    modelo.ID_Modelo = Convert.ToInt32(txtId.Text);
                    ActualizarModelo(modelo);
                }
                //Nuevo Modelo
                else
                {
                    InsertarModelo(modelo);
                    txtId.Text = ObtenerUltimoModelo().Id_Modelo.ToString();
                }
                MessageBox.Show("Datos guardados satisfactoriamente", "Ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvModelos.DataSource = ObtenerModelos();
                LimpiarFormulario();
            }
            else
            {
                MessageBox.Show("Todos los datos son obligatorios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtModelo.Focus();
            }
        }

        private void dgvModelos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1 && dgvModelos.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dgvModelos.CurrentRow.Selected = true;
                txtId.Text = dgvModelos.Rows[e.RowIndex].Cells["ID_Modelo"].FormattedValue.ToString();
                txtModelo.Text = dgvModelos.Rows[e.RowIndex].Cells["Modelo"].FormattedValue.ToString();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtId.Text.Trim().Length > 0)
            {
                DialogResult respuestaAdvertencia = DialogResult.OK;
                respuestaAdvertencia = MessageBox.Show("¿Está seguro de eliminar el modelo?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuestaAdvertencia == DialogResult.Yes)
                {
                    Modelo modelo = new Modelo();
                    modelo.ID_Modelo = Convert.ToInt32(txtId.Text);
                    EliminarModelo(modelo);
                    dgvModelos.DataSource = ObtenerModelos();
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

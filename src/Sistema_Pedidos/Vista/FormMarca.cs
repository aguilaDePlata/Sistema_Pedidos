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
    public partial class frmMarca : Form
    {
        public frmMarca()
        {
            InitializeComponent();
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

        private void LimpiarFormulario()
        {
            txtId.Text = "";
            txtMarca.Text = "";
            txtMarca.Focus();
        }

        private bool FormularioValido()
        {
            bool valido = false;
            string marca = txtMarca.Text.Trim();
            if (marca.Length > 0)
                valido = true;
            return valido;
        }

        public void InsertarMarca(Marca pMarca)
        {
            try
            {
                using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
                {
                    bd.Marca.Add(pMarca);
                    bd.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public MarcaDto ObtenerUltimoMarca()
        {
            try
            {
                using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
                {
                    var consulta = (from n in bd.Marca
                                    orderby n.ID_Marca descending
                                    select new MarcaDto
                                    {
                                        Id_Marca = n.ID_Marca,
                                        Marca = n.Marca1
                                    }).ToList();
                    MarcaDto MarcaDto = consulta.First();
                    return MarcaDto;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ActualizarMarca(Marca pMarca)
        {
            try
            {
                using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
                {
                    var Marcas = from n in bd.Marca
                                   where n.ID_Marca == pMarca.ID_Marca
                                   select n;
                    foreach (Marca mar in Marcas)
                    {
                        mar.Marca1 = pMarca.Marca1;
                    }
                    bd.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void EliminarMarca(Marca pMarca)
        {
            try
            {
                using (C3_BD_PEDIDOS_Entities bd = new C3_BD_PEDIDOS_Entities())
                {
                    var Marcas = from n in bd.Marca
                                   where n.ID_Marca == pMarca.ID_Marca
                                   select n;
                    foreach (Marca mar in Marcas)
                    {
                        bd.Marca.Remove(mar);
                    }
                    bd.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void frmMarca_Load(object sender, EventArgs e)
        {
            dgvMarcas.DataSource = ObtenerMarcas();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (FormularioValido())
            {
                Marca marca = new Marca();
                marca.ID_Marca = 0;
                marca.Marca1 = txtMarca.Text.Trim();
                //Modificar Marca
                if (txtId.Text.Trim().Length > 0)
                {
                    //Código para modificar una Marca
                    marca.ID_Marca = Convert.ToInt32(txtId.Text);
                    ActualizarMarca(marca);
                }
                //Nueva Marca
                else
                {
                    InsertarMarca(marca);
                    txtId.Text = ObtenerUltimoMarca().Id_Marca.ToString();
                }
                MessageBox.Show("Datos guardados satisfactoriamente", "Ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvMarcas.DataSource = ObtenerMarcas();
                LimpiarFormulario();
            }
            else
            {
                MessageBox.Show("Todos los datos son obligatorios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMarca.Focus();
            }
        }

        private void dgvMarcas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1 && dgvMarcas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dgvMarcas.CurrentRow.Selected = true;
                txtId.Text = dgvMarcas.Rows[e.RowIndex].Cells["ID_Marca"].FormattedValue.ToString();
                txtMarca.Text = dgvMarcas.Rows[e.RowIndex].Cells["Marca"].FormattedValue.ToString();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtId.Text.Trim().Length > 0)
            {
                DialogResult respuestaAdvertencia = DialogResult.OK;
                respuestaAdvertencia = MessageBox.Show("¿Está seguro de eliminar la marca?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuestaAdvertencia == DialogResult.Yes)
                {
                    Marca marca = new Marca();
                    marca.ID_Marca = Convert.ToInt32(txtId.Text);
                    EliminarMarca(marca);
                    dgvMarcas.DataSource = ObtenerMarcas();
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

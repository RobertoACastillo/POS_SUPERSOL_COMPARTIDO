using POS_SUPERSOL.CapaEntidades;
using POS_SUPERSOL.CapaNegocios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_SUPERSOL.CapaPresentacion
{
    public partial class frmProductos : Form
    {
        public frmProductos()
        {
            InitializeComponent();
        }

        private void frmProductos_Load(object sender, EventArgs e)
        {
            dgvProductos.DataSource = ProductoBLL.Mostrar();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Producto p = new Producto()
            {
                Nombre = txtNombre.Text,
                Precio = Convert.ToDecimal(txtPrecio.Text),
                Stock = Convert.ToInt32(txtStock.Text),
                Id_Categoria = 1,    // si no usas combo, pon temporalmente 1
                Id_Proveedor = 1
            };

            ProductoBLL.Insertar(p);
            dgvProductos.DataSource = ProductoBLL.Mostrar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Producto p = new Producto()
            {
                Id = Convert.ToInt32(txtId.Text),
                Nombre = txtNombre.Text,
                Precio = Convert.ToDecimal(txtPrecio.Text),
                Stock = Convert.ToInt32(txtStock.Text),
                Id_Categoria = 1,
                Id_Proveedor = 1
            };

            ProductoBLL.Modificar(p);
            dgvProductos.DataSource = ProductoBLL.Mostrar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtId.Text);

            ProductoBLL.Eliminar(id);

            dgvProductos.DataSource = ProductoBLL.Mostrar();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {

            txtId.Clear();
            txtNombre.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
            txtNombre.Focus();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvProductos.Rows[e.RowIndex];

                txtId.Text = fila.Cells["Id"].Value.ToString();
                txtNombre.Text = fila.Cells["Nombre"].Value.ToString();
                txtPrecio.Text = fila.Cells["Precio"].Value.ToString();
                txtStock.Text = fila.Cells["Stock"].Value.ToString();
            }
        }
    }
}

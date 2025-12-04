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

            // VALIDACIONES
            if (txtNombre.Text.Trim() == "" ||
                txtPrecio.Text.Trim() == "" ||
                txtStock.Text.Trim() == "")
            {
                MessageBox.Show("Debe llenar todos los campos antes de continuar.",
                                "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal precio;
            int stock;

            if (!decimal.TryParse(txtPrecio.Text, out precio))
            {
                MessageBox.Show("El precio debe ser un número válido.",
                                "Dato incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(txtStock.Text, out stock))
            {
                MessageBox.Show("El stock debe ser un número entero.",
                                "Dato incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // OBJETO A INSERTAR
            Producto p = new Producto()
            {
                Nombre = txtNombre.Text.Trim(),
                Precio = precio,
                Stock = stock,
                Id_Categoria = 1,
                Id_Proveedor = 1
            };

            ProductoBLL.Insertar(p);
            MessageBox.Show("Producto agregado correctamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

            dgvProductos.DataSource = ProductoBLL.Mostrar();
            btnLimpiar.PerformClick();
            
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Debe seleccionar un producto primero.",
                                "Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro que desea modificar este producto?",
                                "Confirmar modificación",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            // VALIDACIONES IGUALES AL AGREGAR
            if (txtNombre.Text.Trim() == "" ||
                txtPrecio.Text.Trim() == "" ||
                txtStock.Text.Trim() == "")
            {
                MessageBox.Show("Debe llenar todos los campos.", "Campos vacíos",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal precio;
            int stock;

            if (!decimal.TryParse(txtPrecio.Text, out precio))
            {
                MessageBox.Show("El precio debe ser numérico.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(txtStock.Text, out stock))
            {
                MessageBox.Show("El stock debe ser numérico.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Producto p = new Producto()
            {
                Id = Convert.ToInt32(txtId.Text),
                Nombre = txtNombre.Text.Trim(),
                Precio = precio,
                Stock = stock,
                Id_Categoria = 1,
                Id_Proveedor = 1
            };

            ProductoBLL.Modificar(p);
            MessageBox.Show("Producto modificado correctamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

            dgvProductos.DataSource = ProductoBLL.Mostrar();
            btnLimpiar.PerformClick();
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Debe seleccionar un producto para eliminar.",
                                "Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro que desea eliminar este producto?\nEsta acción no se puede deshacer.",
                                "Confirmar eliminación",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            int id;
            if (!int.TryParse(txtId.Text, out id))
            {
                MessageBox.Show("El ID del producto no es válido.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ProductoBLL.Eliminar(id);
            MessageBox.Show("Producto eliminado correctamente.", "Eliminado",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

            dgvProductos.DataSource = ProductoBLL.Mostrar();
            btnLimpiar.PerformClick();
            
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

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS_SUPERSOL.CapaEntidades;
using POS_SUPERSOL.CapaNegocios;

namespace POS_SUPERSOL.CapaPresentacion
{
    public partial class FrmProveedores : Form
    {
        public FrmProveedores()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dgvProveedores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FrmProveedores_Load(object sender, EventArgs e)
        {
            CargarProveedores();
        }

        private void Limpiar()
        {
            txtIdProveedor.Clear();
            txtNombre.Clear();
            txtTelefono.Clear();
            txtDireccion.Clear();
            txtProductoSuministrado.Clear();
        }
        private void CargarProveedores()
        {
            dgvProveedores.DataSource = ProveedorBLL.Mostrar();
        }

        private void dgvProveedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvProveedores.CurrentRow != null)
            {
                txtIdProveedor.Text = dgvProveedores.CurrentRow.Cells["Id"].Value.ToString();
                txtNombre.Text = dgvProveedores.CurrentRow.Cells["Nombre"].Value.ToString();
                txtTelefono.Text = dgvProveedores.CurrentRow.Cells["Telefono"].Value.ToString();
                txtDireccion.Text = dgvProveedores.CurrentRow.Cells["Direccion"].Value.ToString();
                txtProductoSuministrado.Text = dgvProveedores.CurrentRow.Cells["ProductoSuministrado"].Value.ToString();
            }
        }

        private void btnAgregarProveedor_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text.Trim() == "")
            {
                MessageBox.Show("El campo 'Nombre' es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Proveedor obj = new Proveedor()
            {
                Nombre = txtNombre.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Direccion = txtDireccion.Text.Trim(),
                ProductoSuministrado = txtProductoSuministrado.Text.Trim()
            };

            string mensaje = ProveedorBLL.Insertar(obj);

            if (mensaje == "OK")
                MessageBox.Show("Proveedor agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Ocurrió un error al agregar el proveedor:\n" + mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            CargarProveedores();
            Limpiar();
        }

        private void btnModificarProveedor_Click(object sender, EventArgs e)
        {
            if (txtIdProveedor.Text == "")
            {
                MessageBox.Show("Debe seleccionar un proveedor para modificar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "¿Está seguro que desea modificar los datos del proveedor?",
                "Confirmar modificación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes)
                return;

            Proveedor obj = new Proveedor()
            {
                Id = int.Parse(txtIdProveedor.Text),
                Nombre = txtNombre.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Direccion = txtDireccion.Text.Trim(),
                ProductoSuministrado = txtProductoSuministrado.Text.Trim()
            };

            string mensaje = ProveedorBLL.Editar(obj);

            if (mensaje == "OK")
                MessageBox.Show("Proveedor modificado exitosamente.", "Modificación completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Error al modificar el proveedor:\n" + mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            CargarProveedores();
            Limpiar();

        }

        private void btnEliminarProveedor_Click(object sender, EventArgs e)
        {
            if (txtIdProveedor.Text == "")
            {
                MessageBox.Show("Debe seleccionar un proveedor para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "¿Está seguro que desea eliminar este proveedor?\n",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm != DialogResult.Yes)
                return;

            int id = int.Parse(txtIdProveedor.Text);
            string mensaje = ProveedorBLL.Eliminar(id);

            if (mensaje == "OK")
                MessageBox.Show("Proveedor eliminado correctamente.", "Proveedor Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Error al intentar eliminar el proveedor:\n" + mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            CargarProveedores();
            Limpiar();
        }

        private void btnLimpiarProveedor_Click(object sender, EventArgs e)
        {
            Limpiar();
            MessageBox.Show("Campos limpiados.", "Limpieza completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

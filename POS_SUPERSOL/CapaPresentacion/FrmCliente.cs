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
    public partial class FrmCliente : Form
    {
        int clienteId = 0; //almacena el ID del cliente seleccionado para editar o eliminar
        ClienteBLL bll = new ClienteBLL();//crea una instancia de la capa de negocio
        public FrmCliente()
        {
            InitializeComponent();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            dgvClientes.DataSource = bll.Buscar(txtBuscar.Text);
           

        }

        private void FrmCliente_Load(object sender, EventArgs e)
        {
            CargarDatos();
            Limpiar();
        }
        public void CargarDatos()
        {
            //obtiene los datos desde la BLL - DAL - BD
            dgvClientes.DataSource = bll.Listar();
        }
          private void Limpiar()
        {
            txtNombre.Clear();
            txtDui.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            chkEstado.Checked = true;
            txtBuscar.Clear();
            txtNombre.Focus();

            clienteId = 0;//Resetea el ID del cliente 0 = Nuevo Registro
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (clienteId == 0)
            {
                MessageBox.Show("Seleccione un cliente para eliminar.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("¿Está seguro de eliminar el cliente seleccionado?", "Confirmación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bll.Eliminar(clienteId);
                CargarDatos();
                Limpiar();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Cliente c = new Cliente
                {
                    Id = clienteId,//Si es 0 es nuevo registro, si tiene valor es MOdificación
                    Nombre = txtNombre.Text,
                    Dui = txtDui.Text,
                    Telefono = txtTelefono.Text,
                    Correo = txtCorreo.Text,
                    Estado = chkEstado.Checked
                };
                //Llamamos al metodo guardar de la BLL(EL DECIDE SI ES INSERT O UPDATE)
                int id = bll.Guardar(c);
                MessageBox.Show("Cliente guardado con exito.", "Notificación",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatos();
                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btmnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                clienteId = Convert.ToInt32(dgvClientes.Rows[e.RowIndex].Cells["Id"].Value);
                txtNombre.Text = dgvClientes.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                txtDui.Text = dgvClientes.Rows[e.RowIndex].Cells["Dui"].Value.ToString();
                txtTelefono.Text = dgvClientes.Rows[e.RowIndex].Cells["Telefono"].Value.ToString();
                txtCorreo.Text = dgvClientes.Rows[e.RowIndex].Cells["Correo"].Value.ToString();
                chkEstado.Checked = Convert.ToBoolean(dgvClientes.Rows[e.RowIndex].Cells["Estado"].Value);
            }
            ;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                clienteId = Convert.ToInt32(dgvClientes.Rows[e.RowIndex].Cells["Id"].Value);
                txtNombre.Text = dgvClientes.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                txtDui.Text = dgvClientes.Rows[e.RowIndex].Cells["Dui"].Value.ToString();
                txtTelefono.Text = dgvClientes.Rows[e.RowIndex].Cells["Telefono"].Value.ToString();
                txtCorreo.Text = dgvClientes.Rows[e.RowIndex].Cells["Correo"].Value.ToString();
                chkEstado.Checked = Convert.ToBoolean(dgvClientes.Rows[e.RowIndex].Cells["Estado"].Value);
            }
            ;
        }
    }
}
    

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS_SUPERSOL.CapaDatos;

namespace POS_SUPERSOL.CapaPresentacion
{
    public partial class FrmMenuPrincipal : Form
    {
        public FrmMenuPrincipal()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void FrmMenuPrincipal_Load(object sender, EventArgs e)
        {
            lblUsuario.Text = $"Usuario: {SesionActual.NombreUsuario} - Rol: {SesionActual.Rol}";


            /// Control básico por rol
            //Con este codigo deshabilitamos un botón de prueba para el usuario cajero, por ejemplo que no pueda Registrar Cliente(ojo esto es solo prueba)
            switch (SesionActual.Rol)
            {
                case "Admin":
                    // todo habilitado
                    break;
                case "Cajero":
                    btnClientes.Enabled = false;
                    btnUsuarios.Enabled = true;
                    break;
                default:
                    btnClientes.Enabled = false;
                    btnUsuarios.Enabled = false;
                    break;

            }

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            FrmUsuarios frm = new FrmUsuarios();
            frm.ShowDialog();

        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            SesionActual.Cerrar();
            this.Close();
            frmInicioSesion  frm= new frmInicioSesion();
            frm.ShowDialog();
        }

        private void btnProducto_Click(object sender, EventArgs e)
        {
            frmProductos frm = new frmProductos();
            frm.ShowDialog();
        }

        private void btnCategoria_Click(object sender, EventArgs e)
        {
            FrmCategoria frm = new FrmCategoria();
            frm.ShowDialog();
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            FrmRegistrarVentas frm = new FrmRegistrarVentas();
            frm.ShowDialog();
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            FrmProveedores frm = new FrmProveedores();
            frm.ShowDialog();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            FrmCliente frm = new FrmCliente();
            frm.ShowDialog();
        }
    }
}

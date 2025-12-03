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
using POS_SUPERSOL.CapaEntidades;
using POS_SUPERSOL.CapaNegocios;

namespace POS_SUPERSOL.CapaPresentacion
{
    public partial class FrmRegistrarVentas : Form
    {
        public FrmRegistrarVentas()
        {
            InitializeComponent();
        }

        private void FrmRegistrarVentas_Load(object sender, EventArgs e)
        {
            // --- CLIENTES ---
            cboCliente.DataSource = ClienteDAL.ListarActivos();
            cboCliente.DisplayMember = "Nombre";
            cboCliente.ValueMember = "Id";

            // --- TIPO PAGO ---
            cboTipoPago.DataSource = TipoPagoDAL.Listar(); //asiganmos datos al desplegable
            cboTipoPago.DisplayMember = "Nombre"; //lo que muestra
            cboTipoPago.ValueMember = "Id"; //el valor que nos importa ID

            // --- FECHA ---
            dtpFecha.Value = DateTime.Now;//obtiene la fecha de ahora
            dgvDetalle.CellEndEdit += dgvDetalle_CellEndEdit;

            // --- CONFIGURAR COLUMNAS DEL DETALLE ---
            ConfigurarTablaDetalles();



        }
        private void ConfigurarTablaDetalles()
        {
            dgvDetalle.Columns.Clear();

            // ID PRODUCTO
            DataGridViewTextBoxColumn colIdProd = new DataGridViewTextBoxColumn();
            colIdProd.Name = "Id_Producto";
            colIdProd.HeaderText = "ID";
            colIdProd.Visible = false;
            dgvDetalle.Columns.Add(colIdProd);

            // NOMBRE PRODUCTO
            dgvDetalle.Columns.Add("NombreProducto", "Producto");

            // CANTIDAD
            DataGridViewTextBoxColumn colCant = new DataGridViewTextBoxColumn();
            colCant.Name = "Cantidad";
            colCant.HeaderText = "Cant.";
            dgvDetalle.Columns.Add(colCant);

            // PRECIO UNITARIO
            DataGridViewTextBoxColumn colPrecio = new DataGridViewTextBoxColumn();
            colPrecio.Name = "PrecioUnitario";
            colPrecio.HeaderText = "Precio Unitario";
            dgvDetalle.Columns.Add(colPrecio);

            // SUBTOTAL
            DataGridViewTextBoxColumn colSub = new DataGridViewTextBoxColumn();
            colSub.Name = "Subtotal";
            colSub.HeaderText = "Subtotal";
            colSub.ReadOnly = true;
            dgvDetalle.Columns.Add(colSub);
            // Asegurar permisos de edición
            dgvDetalle.ReadOnly = false;

            // Columnas NO editables
            dgvDetalle.Columns["Subtotal"].ReadOnly = true;
            dgvDetalle.Columns["PrecioUnitario"].ReadOnly = true;
            dgvDetalle.Columns["NombreProducto"].ReadOnly = true;
            dgvDetalle.Columns["Id_Producto"].ReadOnly = true;

            // ÚNICA columna editable:
            dgvDetalle.Columns["Cantidad"].ReadOnly = false;


            CargarProductos(string.Empty);

        }

        private void CargarProductos(string filtro)
        {
            // Obtenemos la lista desde DAL
            var tabla = ProductoDal.Mostrar(); // ya lo creamos en Paso 2

            // Filtrar si hay texto
            if (!string.IsNullOrWhiteSpace(filtro))
            {
                var dv = tabla.DefaultView;
                dv.RowFilter = $"Nombre LIKE '%{filtro}%'";
                dgvProductos.DataSource = dv;
            }
            else
            {
                dgvProductos.DataSource = tabla;
            }

            dgvProductos.Columns["Id"].Visible = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string texto = txtBuscarProducto.Text.Trim();
            CargarProductos(texto);
        }

        private void txtBuscarProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CargarProductos(txtBuscarProducto.Text.Trim());
            }
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            // -----------------------------------------------------------
            // 1) Verificar si el usuario seleccionó una fila
            // Si no seleccionó nada, mostramos un mensaje y cancelamos.
            // -----------------------------------------------------------
            if (dgvProductos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un producto.");
                return;
            }

            // -----------------------------------------------------------
            // 2) Recuperar la fila seleccionada del DataGridView
            // De aquí extraemos ID, Nombre y Precio.
            // -----------------------------------------------------------
            DataGridViewRow row = dgvProductos.SelectedRows[0];

            // -----------------------------------------------------------
            // 3) Obtener los valores necesarios del producto
            // Todos vienen de la tabla cargada en dgvProductos.
            // -----------------------------------------------------------
            int idProducto = Convert.ToInt32(row.Cells["Id"].Value);
            string nombre = row.Cells["Nombre"].Value.ToString();
            decimal precio = Convert.ToDecimal(row.Cells["Precio"].Value);

            // -----------------------------------------------------------
            // 4) Inicializar la cantidad por defecto en 1
            // El usuario podrá modificarla después directamente en dgvDetalles.
            // -----------------------------------------------------------
            int cantidad = 1;

            // -----------------------------------------------------------
            // 5) Calcular el subtotal inicial
            // fórmula: precio unitario × cantidad
            // -----------------------------------------------------------
            decimal Subtotal = precio * cantidad;

            // -----------------------------------------------------------
            // 6) Agregar el producto al DataGridView del detalle de venta
            // Cada columna representa una parte del DetalleVenta.
            // -----------------------------------------------------------
            dgvDetalle.Rows.Add(
                idProducto, // Columna: Id_Producto
                nombre,     // Columna: NombreProducto
                cantidad,   // Columna: Cantidad
                precio,     // Columna: PrecioUnitario
                Subtotal    // Columna: SubTotal
            );

            // -----------------------------------------------------------
            // 7) Recalcular el total de la venta
            // Este método será creado después.
            // Si aún no lo tienes, comenta esta línea para evitar errores.
            // -----------------------------------------------------------
            RecalcularTotal(); // Coméntala si aún no has creado el método
        }

        private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //===============================================================
            // EVENTO: CellDoubleClick
            // Si el usuario hace doble clic en un producto,
            // se ejecuta exactamente la misma lógica que el botón "Agregar".
            // Esto hace el proceso más rápido e intuitivo en un POS.
            // ===============================================================
            // Reutilizamos el mismo método del botón Agregar Producto
            // Así no duplicamos código.
            btnAgregarProducto_Click(sender, e);
        }
        private void RecalcularTotal()
        {
            // ---------------------------------------------------------------
            // 1) Inicializar la variable "total"
            // Aquí almacenaremos la suma de todos los subtotales.
            // ---------------------------------------------------------------
            decimal total = 0;

            // ---------------------------------------------------------------
            // 2) Recorrer todas las filas del DataGridView "dgvDetalles"
            // Cada fila representa un "DetalleVenta" con su SubTotal.
            //
            // Por cada fila:
            //    - Convertimos el SubTotal a decimal
            //    - Lo sumamos al total general
            // ---------------------------------------------------------------
            foreach (DataGridViewRow row in dgvDetalle.Rows)
            {
                total += Convert.ToDecimal(row.Cells["Subtotal"].Value);
            }

            // ---------------------------------------------------------------
            // 3) Mostrar el total en la etiqueta lblTotal
            // Formateamos el monto a dos decimales, como es estándar en POS.
            // ---------------------------------------------------------------
            lblTotal.Text = "Total: $" + total.ToString("0.00");
        }

        private void dgvDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // ------------------------------------------------------------------
            // 1) Verificar si la celda editada es específicamente la columna "Cantidad"
            //    Si se editó otra columna, NO hacemos nada.
            // ------------------------------------------------------------------
            if (dgvDetalle.Columns[e.ColumnIndex].Name == "Cantidad")
            {
                // Recuperamos la fila que está siendo editada
                DataGridViewRow row = dgvDetalle.Rows[e.RowIndex];


                // ------------------------------------------------------------------
                // 2) Intentar convertir el valor ingresado a entero (TryParse evita errores)
                // ------------------------------------------------------------------
                bool ok = int.TryParse(row.Cells["Cantidad"].Value?.ToString(), out int cantidad);

                // ------------------------------------------------------------------
                // 3) Validar que la cantidad sea NUMÉRICA y mayor a 0
                //
                // Si no cumple → mostramos mensaje y devolvemos el valor a 1.
                // ------------------------------------------------------------------
                if (!ok || cantidad <= 0)
                {
                    MessageBox.Show("Cantidad inválida.");

                    // Volver a una cantidad válida
                    row.Cells["Cantidad"].Value = 1;
                    cantidad = 1;
                }

                // ------------------------------------------------------------------
                // 4) Obtener el precio unitario del producto (columna no editable)
                // ------------------------------------------------------------------
                decimal precio = Convert.ToDecimal(row.Cells["PrecioUnitario"].Value);

                // ------------------------------------------------------------------
                // 5) Calcular el nuevo subtotal
                //    SubTotal = Cantidad × PrecioUnitario
                // ------------------------------------------------------------------
                decimal Subtotal = cantidad * precio;

                // Guardar el subtotal actualizado en la fila
                row.Cells["Subtotal"].Value = Subtotal;

                // ------------------------------------------------------------------
                // 6) Actualizamos el total general de la venta
                //    (Suma todos los subtotales del dgvDetalles)
                // ------------------------------------------------------------------
                RecalcularTotal();
            }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (dgvDetalle.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una fila para quitar.");
                return;
            }

            dgvDetalle.Rows.RemoveAt(dgvDetalle.SelectedRows[0].Index);

            RecalcularTotal();
        }

        private void btnLimpiarDetalle_Click(object sender, EventArgs e)
        {
            dgvDetalle.Rows.Clear();
            RecalcularTotal();
        }

        private void btnRegistrarVenta_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDetalle.Rows.Count == 0)
                {
                    MessageBox.Show("La venta no tiene productos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ---------------------------------------------------
                // 1) CREAR OBJETO VENTA
                // ---------------------------------------------------
                Venta venta = new Venta()
                {
                    Fecha = dtpFecha.Value,
                    MontoTotal = ObtenerTotalVenta(), // lo creamos abajo
                    Id_Cliente = Convert.ToInt32(cboCliente.SelectedValue),
                    Id_TipoPago = Convert.ToInt32(cboTipoPago.SelectedValue),
                    Id_Empleado = 1 // Por ahora, usuario fijo
                };

                // ---------------------------------------------------
                // 2) CREAR LISTA DE DETALLES
                // ---------------------------------------------------
                List<DetalleVenta> detalles = new List<DetalleVenta>();

                foreach (DataGridViewRow row in dgvDetalle.Rows)
                {
                    detalles.Add(new DetalleVenta()
                    {
                        Id_Producto = Convert.ToInt32(row.Cells["Id_Producto"].Value),
                        Cantidad = Convert.ToInt32(row.Cells["Cantidad"].Value),
                        PrecioUnitario = Convert.ToDecimal(row.Cells["PrecioUnitario"].Value),
                        //Subtotal = Convert.ToDecimal(row.Cells["Subtotal"].Value)
                    });
                }

                // ---------------------------------------------------
                // 3) VALIDAR EN BLL
                // ---------------------------------------------------
                var validacion = VentaBLL.ValidarVenta(venta, detalles);

                if (!validacion.Exito)
                {
                    MessageBox.Show(validacion.Mensaje, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ---------------------------------------------------
                // 4) GUARDAR EN BASE DE DATOS (TRANSACCIÓN)
                // ---------------------------------------------------
                var resultado = VentaDAL.RegistrarVentaTransaccional(venta, detalles);

                if (resultado.Exito)
                {
                    MessageBox.Show(resultado.Mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                }
                else
                {
                    MessageBox.Show(resultado.Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message);
            }
        }

        private decimal ObtenerTotalVenta()
        {
            // ---------------------------------------------------------------
            // 1) Inicializamos la variable donde acumularemos el total
            // ---------------------------------------------------------------
            decimal total = 0;

            // ---------------------------------------------------------------
            // 2) Recorrer cada fila del DataGridView que contiene los detalles
            //    Cada fila representa un "DetalleVenta".
            //
            //    De cada fila tomamos el valor del SubTotal y lo sumamos.
            // ---------------------------------------------------------------
            foreach (DataGridViewRow row in dgvDetalle.Rows)
                total += Convert.ToDecimal(row.Cells["Subtotal"].Value);

            // ---------------------------------------------------------------
            // 3) Retornamos el total calculado
            //    Este valor será insertado en la tabla Venta (campo MontoTotal)
            // ---------------------------------------------------------------
            return total;
        }

        private void LimpiarFormulario()
        {
            dgvDetalle.Rows.Clear();
            lblTotal.Text = "Total: $0.00";
            txtBuscarProducto.Clear();
            CargarProductos(string.Empty); // recarga lista completa
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS_SUPERSOL.Reportes;

namespace POS_SUPERSOL.CapaPresentacion
{
    public partial class frmReporteDiario : Form
    {
        public frmReporteDiario()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime fecha = dtpFecha.Value.Date;

            try
            {
                // 1) Obtener las ventas del día desde la BLL
                DataTable tabla = ReporteBLL.ObtenerVentasPorDia(fecha);

                if (tabla.Rows.Count == 0)
                {
                    MessageBox.Show("No hay ventas registradas en esta fecha.",
                        "Sin Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 2) Seleccionar dónde guardar el PDF
                saveFileDialog1.Title = "Guardar Reporte Diario de Ventas";
                saveFileDialog1.Filter = "Archivo PDF (*.pdf)|*.pdf";
                saveFileDialog1.FileName = $"ReporteVentas_{fecha:dd-MM-yyyy}.pdf";

                if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                    return;

                string ruta = saveFileDialog1.FileName;

                // 3) Generar PDF
                ReporteVentasPDF.GenerarPDF(tabla, fecha, ruta);

                MessageBox.Show("Reporte generado correctamente.\n\nUbicación:\n" + ruta,
                    "PDF Generado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al generar el PDF:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

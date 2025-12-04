using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace POS_SUPERSOL.Reportes
{
    public class ReporteVentasPDF
    {
        public static void GenerarPDF(DataTable tabla, DateTime fecha, string rutaArchivo)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var modelo = new ReporteVentasModel(tabla, fecha);
            var documento = new ReporteVentasDocumento(modelo);

            documento.GeneratePdf(rutaArchivo);
        }
    }
}

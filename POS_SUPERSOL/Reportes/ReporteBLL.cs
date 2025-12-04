using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_SUPERSOL.Reportes
{
    public class ReporteBLL
    {
        private ReporteDAL dal = new ReporteDAL();

        /// <summary>
        /// Obtiene el reporte de ventas de un día específico
        /// </summary>
        public DataTable ObtenerReporteVentasDiario(DateTime fecha)
        {
            // Validación simple: fecha no puede ser mayor que hoy
            if (fecha.Date > DateTime.Now.Date)
                throw new ArgumentException("La fecha no puede ser futura.");

            return dal.ReporteVentasDiario(fecha);
        }

        /// <summary>
        /// Método estático alternativo
        /// </summary>
        public static DataTable ObtenerVentasPorDia(DateTime fecha)
        {
            return ReporteDAL.ObtenerVentasPorDia(fecha);
        }
    }
}

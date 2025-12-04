using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_SUPERSOL.Reportes
{
    public class ReporteVentasModel
    {
        public DataTable Tabla { get; }
        public DateTime Fecha { get; }

        public ReporteVentasModel(DataTable tabla, DateTime fecha)
        {
            Tabla = tabla;
            Fecha = fecha;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS_SUPERSOL.CapaDatos;

namespace POS_SUPERSOL.Reportes
{
    public class ReporteDAL
    {
        private string conexion = Conexion.Cadena; // Tu cadena de conexión

        /// <summary>
        /// Obtiene las ventas de un día específico
        /// </summary>
        public DataTable ReporteVentasDiario(DateTime fecha)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conn = new SqlConnection(conexion))
            {
                using (SqlCommand cmd = new SqlCommand("sp_reporte_ventas_diario", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Fecha", fecha.Date);

                    conn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        tabla.Load(dr);
                    }
                }
            }

            return tabla;
        }

        /// <summary>
        /// Método estático alternativo
        /// </summary>
        public static DataTable ObtenerVentasPorDia(DateTime fecha)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conn = new SqlConnection(Conexion.Cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_reporte_ventas_diario", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Fecha", fecha.Date);

                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);
            }

            return tabla;
        }
    }
}


using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS_SUPERSOL.CapaEntidades;

namespace POS_SUPERSOL.CapaDatos
{
    public class ProveedorDAL
    {

        public static DataTable Mostrar()
        {
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Proveedor WHERE Estado = 1", cn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static string Insertar(Proveedor obj)
        {
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Proveedor (Nombre, Telefono, Direccion, ProductoSuministrado, Estado) " +
                    "VALUES (@Nombre, @Telefono, @Direccion, @Producto, 1)", cn);

                cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                cmd.Parameters.AddWithValue("@Telefono", obj.Telefono);
                cmd.Parameters.AddWithValue("@Direccion", obj.Direccion);
                cmd.Parameters.AddWithValue("@Producto", obj.ProductoSuministrado);

                return cmd.ExecuteNonQuery() == 1 ? "OK" : "Error al insertar proveedor";
            }
        }

        public static string Editar(Proveedor obj)
        {
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Proveedor SET Nombre = @Nombre, Telefono = @Telefono, Direccion = @Direccion, " +
                    "ProductoSuministrado = @Producto WHERE Id = @Id", cn);

                cmd.Parameters.AddWithValue("@Id", obj.Id);
                cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                cmd.Parameters.AddWithValue("@Telefono", obj.Telefono);
                cmd.Parameters.AddWithValue("@Direccion", obj.Direccion);
                cmd.Parameters.AddWithValue("@Producto", obj.ProductoSuministrado);

                return cmd.ExecuteNonQuery() == 1 ? "OK" : "Error al editar proveedor";
            }
        }

        public static string Eliminar(int id)
        {
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Proveedor SET Estado = 0 WHERE Id = @Id", cn);

                cmd.Parameters.AddWithValue("@Id", id);

                return cmd.ExecuteNonQuery() == 1 ? "OK" : "Error al eliminar proveedor";
            }
        }
    }
}


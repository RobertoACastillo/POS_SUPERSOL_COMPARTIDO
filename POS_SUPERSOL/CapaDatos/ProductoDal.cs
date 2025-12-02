using POS_SUPERSOL.CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_SUPERSOL.CapaDatos
{
    public class ProductoDal
    {
        public static DataTable Mostrar()
        {
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT * FROM Producto", cn);

                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static void Insertar(Producto p)
        {
            using(SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                string sql = @"INSERT INTO Producto
                           (Nombre, Precio, Stock, Id_Categoria, Id_Proveedor)
                           VALUES (@Nombre, @Precio, @Stock, @IdCat, @IdProv)";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@Nombre", p.Nombre);
                cmd.Parameters.AddWithValue("@Precio", p.Precio);
                cmd.Parameters.AddWithValue("@Stock", p.Stock);
                cmd.Parameters.AddWithValue("@IdCat", p.Id_Categoria);
                cmd.Parameters.AddWithValue("@IdProv", p.Id_Proveedor);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void Modificar(Producto p)
        {
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                string sql = @"UPDATE Producto SET
                           Nombre=@Nombre,
                           Precio=@Precio,
                           Stock=@Stock,
                           Id_Categoria=@IdCat,
                           Id_Proveedor=@IdProv
                           WHERE Id=@Id";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@Id", p.Id);
                cmd.Parameters.AddWithValue("@Nombre", p.Nombre);
                cmd.Parameters.AddWithValue("@Precio", p.Precio);
                cmd.Parameters.AddWithValue("@Stock", p.Stock);
                cmd.Parameters.AddWithValue("@IdCat", p.Id_Categoria);
                cmd.Parameters.AddWithValue("@IdProv", p.Id_Proveedor);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void Eliminar(int id)
        {
            using(SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                string sql = "DELETE FROM Producto WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@Id", id);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}

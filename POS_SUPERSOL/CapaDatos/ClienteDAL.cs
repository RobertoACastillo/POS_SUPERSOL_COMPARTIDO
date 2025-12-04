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
    public class ClienteDAL
    {
        public DataTable Listar()
        {
            DataTable dt = new DataTable();//tabla en memoria
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            //SQLConnection: representa la conexión a una base de datos SQL
            ////Server usando la cadena de conexion
            {
                string sql = "SELECT Id, Nombre, Telefono, Correo,Dui, Estado from Cliente";
                //Consulta SQL que devuelve todos los registros de la tabla Cliente
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                //SqlCommand: prepara el comando SQL que se enviara al servidor
                {
                    cn.Open();//abre la conexion
                    new SqlDataAdapter(cmd).Fill(dt);
                    //SqlDataAdapter: Ejecuta el SELECT y llena el DataTable con los resultados
                }
            }
            return dt;//retorna la tabla con los datos
        }

        public int Insertar(Cliente c)
        {
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                string sql = @"INSERT INTO Cliente (Nombre, Telefono, Correo,Dui, Estado)
               VALUES (@Nombre, @Telefono,@Correo, @Dui, @Estado);
               SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", c.Nombre);
                    cmd.Parameters.AddWithValue("@Dui", c.Dui);
                    cmd.Parameters.AddWithValue("@Telefono", c.Telefono);
                    cmd.Parameters.AddWithValue("@Correo", c.Correo);
                    cmd.Parameters.AddWithValue("@Estado", c.Estado);
                    cn.Open();
                    //ExecuteScalar: Ejecuta el comando y devuelve el primer
                    ////valor de la primera fila del conjunto de resultados(el ID)
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public bool Actualizar(Cliente c)
        {
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                string sql = @"UPDATE Cliente SET Nombre=@nombre, Dui=@dui,
                            Telefono=@telefono, Correo=@correo, Estado=@estado
                            WHERE Id=@id";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@id", c.Id);
                    cmd.Parameters.AddWithValue("@nombre", c.Nombre);
                    cmd.Parameters.AddWithValue("@telefono", c.Telefono);
                    cmd.Parameters.AddWithValue("@correo", c.Correo);
                    cmd.Parameters.AddWithValue("@dui", c.Dui);
                    cmd.Parameters.AddWithValue("@estado", c.Estado);

                    cn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                    //ExecuteNonQuery(): devulve el numero de filas afectadas
                    //>0 significa que se actualizo al menos una fila
                }
            }
        }
        public bool Eliminar(int id)
        {
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                string sql = "DELETE FROM Cliente WHERE Id=@id";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                    //Elimina y devuelve true si se elimino al menos una fila
                }
            }
        }
        public DataTable Buscar(string filtro)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                string sql = @"SELECT Id, Nombre, Telefono, Correo, Dui, Estado
               FROM Cliente
               WHERE Nombre LIKE @filtro OR Telefono LIKE @filtro";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%");
                    cn.Open();
                    new SqlDataAdapter(cmd).Fill(dt);
                    //Llena el DataTable con los resultados de la busqueda
                }
            }
            return dt;
        }


        // Este método sirve para llenar el ComboBox de clientes.
        public static List<Cliente> ListarActivos()
        {
            List<Cliente> lista = new List<Cliente>();

            using (SqlConnection con = new SqlConnection(Conexion.Cadena))
            {
                string sql = "SELECT * FROM Cliente WHERE Estado = 1";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Cliente
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["Nombre"].ToString(),
                                Dui = dr["Dui"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }
                }
            }
            return lista;



        }

    }
}
    
    

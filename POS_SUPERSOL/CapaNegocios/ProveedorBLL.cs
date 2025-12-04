using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS_SUPERSOL.CapaDatos;
using POS_SUPERSOL.CapaEntidades;

namespace POS_SUPERSOL.CapaNegocios
{
    public class ProveedorBLL
    {
        public static DataTable Mostrar()
        {
            return ProveedorDAL.Mostrar();
        }

        public static string Insertar(Proveedor obj)
        {
            return ProveedorDAL.Insertar(obj);
        }

        public static string Editar(Proveedor obj)
        {
            return ProveedorDAL.Editar(obj);
        }

        public static string Eliminar(int id)
        {
            return ProveedorDAL.Eliminar(id);
        }
    }
}

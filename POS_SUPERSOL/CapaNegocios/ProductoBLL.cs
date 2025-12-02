using POS_SUPERSOL.CapaDatos;
using POS_SUPERSOL.CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_SUPERSOL.CapaNegocios
{
    public class ProductoBLL
    {
        public static DataTable Mostrar()
        {
            return ProductoDal.Mostrar();
        }

        public static void Insertar(Producto p)
        {
            ProductoDal.Insertar(p);
        }

        public static void Modificar(Producto p)
        {
            ProductoDal.Modificar(p);
        }

        public static void Eliminar(int id)
        {
            ProductoDal.Eliminar(id);
        }
    }
}

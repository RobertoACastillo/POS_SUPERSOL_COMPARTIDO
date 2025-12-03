using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_SUPERSOL.CapaEntidades
{
    public class DetalleVenta
        {
            public int Id { get; set; }
            public int Cantidad { get; set; }
            public decimal PrecioUnitario { get; set; }

            // SubTotal debe calcularse automáticamente
            public decimal Subtotal
            {
                get { return Cantidad * PrecioUnitario; }
            }

            public int Id_Venta { get; set; }
            public int Id_Producto { get; set; }
        }
}

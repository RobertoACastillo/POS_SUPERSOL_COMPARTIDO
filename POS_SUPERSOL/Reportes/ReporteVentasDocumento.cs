using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace POS_SUPERSOL.Reportes
{
    public class ReporteVentasDocumento: IDocument
    {
        private readonly ReporteVentasModel Modelo;

        public ReporteVentasDocumento(ReporteVentasModel modelo)
        {
            Modelo = modelo;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => new DocumentSettings();

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(30);

                // Encabezado
                page.Header().Column(col =>
                {
                    col.Item().Text("Super Sol").Bold().FontSize(20);
                    col.Item().Text("Reporte Diario de Ventas").FontSize(14);
                    col.Item().Text($"Fecha: {Modelo.Fecha:dd/MM/yyyy}").FontSize(11);
                });

                // Tabla de ventas
                page.Content().PaddingTop(20)
                    .Element(GenerarTabla);

                // Pie de página
                page.Footer().AlignCenter().Text(txt =>
                {
                    txt.Span("Generado el ").SemiBold();
                    txt.Span(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                    txt.Line(" — Sistema Super Sol");
                });
            });
        }

        private void GenerarTabla(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(cols =>
                {
                    cols.RelativeColumn(3); // Producto
                    cols.RelativeColumn(1); // Cantidad
                    cols.RelativeColumn(1); // Precio Unitario
                    cols.RelativeColumn(1); // Subtotal
                });

                table.Header(header =>
                {
                    header.Cell().Background("#EAEAEA").Padding(5).Text("Producto").SemiBold();
                    header.Cell().Background("#EAEAEA").Padding(5).Text("Cantidad").SemiBold();
                    header.Cell().Background("#EAEAEA").Padding(5).Text("Precio Unitario").SemiBold();
                    header.Cell().Background("#EAEAEA").Padding(5).Text("Subtotal").SemiBold();
                });

                decimal totalGeneral = 0;

                foreach (DataRow row in Modelo.Tabla.Rows)
                {
                    string producto = row["Producto"].ToString();
                    int cantidad = Convert.ToInt32(row["Cantidad"]);
                    decimal precio = Convert.ToDecimal(row["PrecioUnitario"]);
                    decimal subtotal = Convert.ToDecimal(row["Subtotal"]);

                    totalGeneral += subtotal;

                    table.Cell().Padding(4).Text(producto);
                    table.Cell().Padding(4).Text(cantidad.ToString());
                    table.Cell().Padding(4).Text(precio.ToString("C2"));
                    table.Cell().Padding(4).Text(subtotal.ToString("C2"));
                }

                table.Cell().ColumnSpan(4).AlignRight().Padding(10)
                    .Text($"TOTAL DEL DÍA: {totalGeneral:C2}")
                    .Bold().FontSize(14);
            });
        }
    }
}


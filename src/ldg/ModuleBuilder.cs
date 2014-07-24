using IT = iTextSharp.text;
namespace Talaran.Ldg {
   public class ModuleBuilder{
      
      private readonly IT.IDocListener document;
      
      private const int COLUMNS = 4;
      private const int SIZE_TITLE = 14;
      private const int SIZE_ROW = 10;
      private const float PAD_BOTTOM = 4F;
      private readonly int year;
      private readonly int rowsPerPage;
      public ModuleBuilder(IT.IDocListener document, int year, int rowsPerPage) {
         if (document == null) {
            throw new System.ArgumentNullException("document", "document cannot be null"); 
         } else {
            this.document = document;
         }
         this.year = year;
         this.rowsPerPage = rowsPerPage;
      }

           

      private int pageCounter = 0;
      public void AddPage() {
         var table = GetTable(year);
         for (int i = 0; i < rowsPerPage; i++) { 
            var data = new IT.Phrase((pageCounter * rowsPerPage + i + 1).ToString("000") ,
                                 IT.FontFactory.GetFont(IT.FontFactory.HELVETICA, 12, IT.Font.NORMAL));
         
            var cell = new IT.pdf.PdfPCell(data);
            cell.MinimumHeight = 60f;
            cell.HorizontalAlignment = IT.Element.ALIGN_LEFT; 
            cell.PaddingBottom = PAD_BOTTOM;
            table.AddCell(cell);
            table.AddCell(new IT.pdf.PdfPCell());
            table.AddCell(new IT.pdf.PdfPCell());
            table.AddCell(new IT.pdf.PdfPCell());
         }
         pageCounter++;
         document.Add(table);
         document.NewPage();
      }

      private IT.pdf.PdfPTable GetTable(int year) {
         var ed =
           new IT.Phrase("Trofeo Luca De Gerone " + year.ToString() ,
                         IT.FontFactory.GetFont(IT.FontFactory.HELVETICA, SIZE_TITLE + 2, IT.Font.BOLD)
                         );



         //pett, nome ,  firma
         float[] widths = {1f,6f,6f, 4f};
         IT.pdf.PdfPTable table = new IT.pdf.PdfPTable(widths);
         table.HeaderRows = 2;
         table.DefaultCell.Border = IT.Rectangle.NO_BORDER;
         
         // edizione
         var cell = new IT.pdf.PdfPCell(ed);
         cell.HorizontalAlignment = IT.Element.ALIGN_CENTER; 
         cell.Colspan = COLUMNS;
         cell.PaddingBottom = PAD_BOTTOM;
         table.AddCell(cell);

         
         // riga vuota
         //table.AddCell(GetEmptyRow());


         var data =
           new IT.Phrase("Pett.",
                         IT.FontFactory.GetFont(IT.FontFactory.HELVETICA, SIZE_ROW, IT.Font.BOLD));

         cell = new IT.pdf.PdfPCell(data);
         cell.HorizontalAlignment = IT.Element.ALIGN_LEFT; 
         cell.PaddingBottom = PAD_BOTTOM;
         table.AddCell(cell);
         
         data = new IT.Phrase("Cognome",
                         IT.FontFactory.GetFont(IT.FontFactory.HELVETICA, SIZE_ROW, IT.Font.BOLD));
         cell = new IT.pdf.PdfPCell(data);
         cell.HorizontalAlignment = IT.Element.ALIGN_LEFT; 
         cell.PaddingBottom = PAD_BOTTOM;
         table.AddCell(cell);

         data = new IT.Phrase("Nome",
                         IT.FontFactory.GetFont(IT.FontFactory.HELVETICA, SIZE_ROW, IT.Font.BOLD));
         cell = new IT.pdf.PdfPCell(data);
         cell.HorizontalAlignment = IT.Element.ALIGN_LEFT; 
         cell.PaddingBottom = PAD_BOTTOM;
         table.AddCell(cell);
        
         data = new IT.Phrase("Firma di un genitore",
                IT.FontFactory.GetFont(IT.FontFactory.HELVETICA, SIZE_ROW, IT.Font.BOLD));
         cell = new IT.pdf.PdfPCell(data);
         cell.HorizontalAlignment = IT.Element.ALIGN_RIGHT; 
         cell.PaddingBottom = PAD_BOTTOM;
         table.AddCell(cell);
         return table;
      }
   }
}
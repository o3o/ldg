using IT = iTextSharp.text;
namespace Talaran.Ldg {
   public class PdfBuilder: IBuilder {
      private int pos = 0;
      private IT.IDocListener document;
      private IT.pdf.PdfPTable table;
      private const int COLUMNS = 5;
      private const int SIZE_TITLE = 20;
      private const int SIZE_ROW = 10;
      private const float PAD_BOTTOM = 4F;
      public PdfBuilder(IT.IDocListener document) {
         if (document == null) {
            throw new System.ArgumentNullException("document", "document cannot be null." );
         }
         this.document = document;

      }

      public void BeginReport(string title, int year) {
         SetTable(title, year);

      }

      public void EndReport() {
         document.Add(table);
         document.NewPage();
      }


      public void Add(Athlete athete) {
         // posizione
          var ps =
            new IT.Phrase((++pos).ToString(),
                          IT.FontFactory.GetFont(IT.FontFactory.HELVETICA, SIZE_ROW, IT.Font.BOLD)
                          );
          var cell = new IT.pdf.PdfPCell(ps);
          cell.HorizontalAlignment = IT.Element.ALIGN_LEFT;
          // evidenzia i primi 3
          if (pos < 4) {
             cell.GrayFill = 0.90F;
          }

          cell.PaddingBottom = PAD_BOTTOM;
          table.AddCell(cell);

          var data =
            new IT.Phrase(athete.Id.ToString() ,
                          IT.FontFactory.GetFont(IT.FontFactory.HELVETICA, SIZE_ROW, IT.Font.NORMAL));

          cell = new IT.pdf.PdfPCell(data);
          cell.HorizontalAlignment = IT.Element.ALIGN_LEFT;
          cell.PaddingBottom = PAD_BOTTOM;
          table.AddCell(cell);

         data = new IT.Phrase(athete.Surname + " "  + athete.Name,
                         IT.FontFactory.GetFont(IT.FontFactory.HELVETICA, SIZE_ROW, IT.Font.NORMAL));
         cell = new IT.pdf.PdfPCell(data);
         cell.HorizontalAlignment = IT.Element.ALIGN_LEFT;
         cell.PaddingBottom = PAD_BOTTOM;
         table.AddCell(cell);

         data = new IT.Phrase(athete.Year.ToString(),
                         IT.FontFactory.GetFont(IT.FontFactory.HELVETICA, SIZE_ROW, IT.Font.NORMAL));
         cell = new IT.pdf.PdfPCell(data);
         cell.HorizontalAlignment = IT.Element.ALIGN_LEFT;
         cell.PaddingBottom = PAD_BOTTOM;
         table.AddCell(cell);


         string t = athete.Time == "99:99" ? "rit." : athete.Time;
         data = new IT.Phrase(t,
                IT.FontFactory.GetFont(IT.FontFactory.HELVETICA, SIZE_ROW, IT.Font.NORMAL));
         cell = new IT.pdf.PdfPCell(data);
         cell.HorizontalAlignment = IT.Element.ALIGN_RIGHT;
         cell.PaddingBottom = PAD_BOTTOM;
         table.AddCell(cell);

      }
      private void SetTable(string titleString, int year) {
         pos = 0;
         var ed =
           new IT.Phrase("Trofeo Luca De Gerone " + year.ToString() ,
                         IT.FontFactory.GetFont(IT.FontFactory.HELVETICA, SIZE_TITLE + 2, IT.Font.BOLD)
                         );

         var title =
           new IT.Phrase(titleString,
                         IT.FontFactory.GetFont(IT.FontFactory.HELVETICA, SIZE_TITLE, IT.Font.BOLD)
                         );
         //pos, pett, nome ,  anno tempo
         float[] widths = {1f,1f,6f,2f,4f};
         table = new IT.pdf.PdfPTable(widths);
         table.HeaderRows = 2;
         table.DefaultCell.Border = IT.Rectangle.NO_BORDER;

         // edizione
         var cell = new IT.pdf.PdfPCell(ed);
         cell.HorizontalAlignment = IT.Element.ALIGN_CENTER;
         cell.Colspan = COLUMNS;
         cell.PaddingBottom = PAD_BOTTOM;
         table.AddCell(cell);


         // intesatazione
         cell = new IT.pdf.PdfPCell(title);
         cell.HorizontalAlignment = IT.Element.ALIGN_CENTER;
         cell.Colspan = COLUMNS;
         cell.PaddingBottom = PAD_BOTTOM;
         cell.GrayFill = 0.90F;
         table.AddCell(cell);

         // riga vuota
         //table.AddCell(GetEmptyRow());

         var data =
           new IT.Phrase("Pos.",
                         IT.FontFactory.GetFont(IT.FontFactory.HELVETICA, SIZE_ROW, IT.Font.BOLD));
         cell = new IT.pdf.PdfPCell(data);
         cell.HorizontalAlignment = IT.Element.ALIGN_LEFT;
         cell.PaddingBottom = PAD_BOTTOM;
         table.AddCell(cell);

         data =
           new IT.Phrase("Pett.",
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

         data = new IT.Phrase("Anno",
                         IT.FontFactory.GetFont(IT.FontFactory.HELVETICA, SIZE_ROW, IT.Font.BOLD));
         cell = new IT.pdf.PdfPCell(data);
         cell.HorizontalAlignment = IT.Element.ALIGN_LEFT;
         cell.PaddingBottom = PAD_BOTTOM;
         table.AddCell(cell);


         data = new IT.Phrase("Tempo",
                IT.FontFactory.GetFont(IT.FontFactory.HELVETICA, SIZE_ROW, IT.Font.BOLD));
         cell = new IT.pdf.PdfPCell(data);
         cell.HorizontalAlignment = IT.Element.ALIGN_RIGHT;
         cell.PaddingBottom = PAD_BOTTOM;
         table.AddCell(cell);
      }
      private IT.pdf.PdfPCell GetEmptyRow() {
         var cell = new IT.pdf.PdfPCell();
         cell.Border = IT.Rectangle.NO_BORDER;
         cell.HorizontalAlignment = IT.Element.ALIGN_CENTER;
         cell.Colspan = COLUMNS;
         cell.PaddingBottom = 20F;
         return cell;
      }
   }
}

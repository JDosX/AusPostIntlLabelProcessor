using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;

namespace PdfProcessor {
  class Program {
    /// <summary>
    /// Main program entrypoint.
    /// </summary>
    /// <param name="args">
    /// Program args:
    /// <list type="bullet">
    /// <item>Arg 1: Path of PDF file to import.</item>
    /// </list>
    /// </param>
    static void Main(string[] args) {
      string readPath = args[0];
      string writePath = args[1];

      PdfDocument document = PdfReader.Open(readPath, PdfDocumentOpenMode.Modify);

      // Iterate through the pages and resize them.
      foreach (PdfPage page in document.Pages) {
        // Set a custom size, as `PageSize` does not contain a preset for 4" x 6".
        XPoint bottomLeft = new XPoint(0, 420);
        XPoint topRight = new XPoint(page.MediaBox.X2, page.MediaBox.Y2);

        PdfRectangle shippingLabelMediaBox = new PdfRectangle(bottomLeft, topRight);
        page.MediaBox = shippingLabelMediaBox;
      }

      document.Save(writePath);
    }
  }
}

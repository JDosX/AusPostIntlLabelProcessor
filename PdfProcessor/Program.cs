using System;
using System.Diagnostics;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;

namespace PdfProcessor {
  class Program {
    /// <summary>
    /// Error code to throw if bad arguments are provided.
    /// </summary>
    private static int ERROR_BAD_ARGUMENTS = 0xA0;

    /// <summary>
    /// How much to trim off the bottom of the page.
    /// </summary>
    private static int BASE_TRIM = 420;
    /// <summary>
    /// How much to trim off the top margin.
    /// </summary>
    private static int TOP_MARGIN_TRIM = 7;
    /// <summary>
    /// How much to trim off the margins on the rest of the sides.
    /// </summary>
    private static int DEFAULT_MARGIN_TRIM = 10;

    /// <summary>
    /// Main program entrypoint.
    /// </summary>
    /// <param name="args">
    /// Program args:
    /// <list type="bullet">
    /// <item>Arg 1: Path of PDF file to import.</item>
    /// <item>Arg 2: (Options) Path to which to save the PDF file. If no path is specified, it will be saved to a temporary directory.</item>
    /// </list>
    /// </param>
    static void Main(string[] args) {
      Args parsedArgs = Args.Parse(args);
      if (parsedArgs == null) {
        Console.WriteLine("Invalid args provided: {0}", args);
        Environment.Exit(ERROR_BAD_ARGUMENTS);
      }

      PdfDocument document = PdfReader.Open(parsedArgs.ReadPath, PdfDocumentOpenMode.Modify);

      // Iterate through the pages and resize them.
      foreach (PdfPage page in document.Pages) {
        // Set a custom size, as `PageSize` does not contain a preset for 4" x 6".
        XPoint bottomLeft = new XPoint(DEFAULT_MARGIN_TRIM, BASE_TRIM + DEFAULT_MARGIN_TRIM);
        XPoint topRight = new XPoint(page.MediaBox.X2 - DEFAULT_MARGIN_TRIM, page.MediaBox.Y2 - TOP_MARGIN_TRIM);

        PdfRectangle shippingLabelMediaBox = new PdfRectangle(bottomLeft, topRight);
        page.MediaBox = shippingLabelMediaBox;
      }

      // Save document and then open in default viewer.
      document.Save(parsedArgs.WritePath);
      Process.Start(new ProcessStartInfo(parsedArgs.WritePath) {
        UseShellExecute = true,
      });
    }
  }
}

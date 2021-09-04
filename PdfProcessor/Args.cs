using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfProcessor {
  public class Args {
    /// <summary>
    /// The path from which the input pdf file should be read.
    /// </summary>
    public string ReadPath { get; private set; }
    /// <summary>
    /// The path to which the output file should be written.
    /// </summary>
    public string WritePath { get; private set; }

    /// <summary>
    /// Parses and validity checks the terminal arguments.
    /// </summary>
    /// <param name="rawArgs">The raw args provided from terminal.</param>
    public static Args Parse(string[] rawArgs) {
      if (rawArgs.Length == 0) {
        return null;
      }
      if (rawArgs.Length >= 2) {
        return null;
      }

      Args args = new Args();
      args.ReadPath = rawArgs[0];

      if (rawArgs.Length == 2) {
        args.WritePath = rawArgs[2];
      } else {
        args.WritePath = Path.Combine(
          Path.GetDirectoryName(args.ReadPath),
          string.Format(
            "{0}_resized_{1}",
            Path.GetFileNameWithoutExtension(args.ReadPath),
            Path.ChangeExtension(Path.GetRandomFileName(), "pdf")
          )
        );
      }

      return args;
    }
  }
}

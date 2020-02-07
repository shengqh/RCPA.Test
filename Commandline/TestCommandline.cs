using CommandLine;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCPA.Commandline
{
  [TestFixture]
  public class TestCommandline
  {
    public class TestOptions : AbstractOptions
    {
      [Option('i', "inputFile", Required = true, MetaValue = "FILE", HelpText = "Fasta file (fasta format)")]
      public string InputFile { get; set; }

      [Option('o', "outputFile", Required = true, MetaValue = "FILE", HelpText = "Output file")]
      public string OutputFile { get; set; }

      public override bool PrepareOptions()
      {
        if (!string.IsNullOrEmpty(this.InputFile) && !File.Exists(this.InputFile))
        {
          ParsingErrors.Add(string.Format("Input file not exists {0}.", this.InputFile));
          return false;
        }

        return true;
      }
    }

    [Test]
    public void TestMissingOption()
    {
      var options = new TestOptions();

      var args = new string[] { "-i", "aa" };
      var result = CommandLine.Parser.Default.ParseArguments(args, options);
      Assert.IsFalse(result);
    }

    [Test]
    public void TestUnknownOption()
    {
      var options = new TestOptions();
      var args = new string[] { "-i", "aa", "-o", "bb", "-c", "unknown" };
      var result = CommandLine.Parser.Default.ParseArguments(args, options);
      Assert.IsFalse(result);
      Console.Out.WriteLine((from er in options.LastParserState.Errors select er.ToString()).Merge("\n"));
    }
  }
}

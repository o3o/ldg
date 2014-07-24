namespace Talaran.Ldg {
   public enum Action {
      Interactive,
      Insert,
      CreateList,
      CreateResult,
      Module
   }
   public class Options {
      [CommandLine.Option("a", "action", Required = true, DefaultValue = Action.CreateResult,
                          HelpText = "Azione da eseguire (Interactive, Insert, CreateList, CreateResult)")]
      public Action Action { get; set; }

      [CommandLine.Option("y", "year", Required = false, HelpText = "Anno. Solo per Create*")]
      public int YearEdition { get; set; }

      [CommandLine.Option("d", "db", Required = true, HelpText = "Database.")]
      public string Database  { get; set; }

      [CommandLine.Option("i", "input", Required = false, HelpText = "File cvs di input (atleti). Solo in caso di Insert")]
      public string Input { get; set; }

      [CommandLine.Option("c", "config", Required = false, HelpText = "File di configurazione. Solo in caso di Interactive, Create*")]
      public string Categories { get; set; }


      [CommandLine.Option("o", "output", Required = false, HelpText = "File pdf di output. Solo in caso di Create*")]
      public string Output { get; set; }


      [CommandLine.HelpOption(HelpText = "Display this help screen.")]
      public string GetUsage() {
         const string VERSION = "0.2.0";
         var help = new CommandLine.Text.HelpText(new CommandLine.Text.HeadingInfo("Ldg", VERSION));
         help.Copyright = new CommandLine.Text.CopyrightInfo("Gruppo Ragni", 2014, 2019);
         help.AddPreOptionsLine("by ODV");
         help.AddPreOptionsLine("====================");
         help.AddPreOptionsLine("Usage: Ldg -a createresult -y 2011 -d ../../db/ldg.sqlite -c ../../doc/cat.cvs -");
         help.AddOptions(this);
         return help;
      }

   }
}

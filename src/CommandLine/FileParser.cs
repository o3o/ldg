using System.Linq;
using SCG = System.Collections.Generic;
namespace CommandLine {
   public static class FileParser {
      /**
         Unisce le opzioni presentti sul file con le opzioni passate.
         
         L'opzione presente in precedenceArgs ha sempre la precedenza su
         quelle scitte su file
      */
      public static string[] Merge(string filename, string[] precedenceArgs) {
         var merged = new SCG.List<string>();
         merged.AddRange(Parse(filename));
         merged.AddRange(precedenceArgs);
         return merged.Distinct().ToArray();
      }

      /**
         Ottiene la  lista degli argomenti dal file filename. 
         Se il file non esiste resituisce l'elenco di default
      */
      public static string[] Parse(string filename, string[] defaultArgs) {
         if (System.IO.File.Exists(filename)) {
            return Parse(filename);
         } else {
            return defaultArgs;
         }
      }
      
      /**
         Ottiene la  lista degli argomenti dal file filename
         
         Ciascuna riga del file passato deve essere una opzione valida.
         Le righe che iniziano con # oppure con // sono ignorate
      */
      public static string[] Parse(string filename) {
         if (System.IO.File.Exists(filename)) {
            string[] lines = System.IO.File.ReadAllLines(filename);
            var result = lines.Where(line => !(line.StartsWith("#") || line.StartsWith("//")));
            return result.ToArray();
         } else {
            return new string[0];
         }
      }
   }
}
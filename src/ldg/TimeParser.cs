namespace Talaran.Ldg {
   public class TimeParser {
      public string Parse(string time) {
         if (time.ToLower().StartsWith("r")) {
            return "99:99.99";
         } else {
            string rxString = @"(?<m>\d*)[+|\.|/](?<s>\d+)([+|\.|/](?<d>\d+))?";
            var rx = new System.Text.RegularExpressions.Regex(rxString);
            var match = rx.Match(time);
            if (match.Success) {
               int m = 0;
               int s = 0;
               int d = 0;
            
               int.TryParse(match.Result("${m}"), out m);
               int.TryParse(match.Result("${s}"), out s);
               int.TryParse(match.Result("${d}"), out d);
               return m.ToString("00")  + ":" +
                 s.ToString("00") + "." +
                 d.ToString("00");
            } else {
               return string.Empty;
            }
         }
      }
   }
}
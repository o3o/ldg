namespace Talaran.Ldg {
   [FileHelpers.DelimitedRecord(",")]
   [FileHelpers.ConditionalRecord(FileHelpers.RecordCondition.ExcludeIfBegins, "//")]
   public class Athlete {
      public int Id { get; set; }
      public string Name { get; set; }
      public string Surname { get; set; }
      public int Year { get; set; }
      public string Gender { get; set; }
      public string Time { get; set; }
   }
}

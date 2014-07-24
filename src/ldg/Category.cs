namespace Talaran.Ldg {
   [FileHelpers.DelimitedRecord(",")] 
   [FileHelpers.ConditionalRecord(FileHelpers.RecordCondition.ExcludeIfBegins, "//")]
   public class Category {
      /**
         U8, U8F etc
      */
      public string Id;
      public string Title;
      public string Sql;
   }
}
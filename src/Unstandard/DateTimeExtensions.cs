namespace Microline {
   public static class DateTimeExtensions {
      public static System.DateTime EndOfTheDay(this System.DateTime date) {
         return new System.DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
      }

      public static bool IsDate(this string input) {
         if (!string.IsNullOrEmpty(input)) {
            System.DateTime dt;
            return (System.DateTime.TryParse(input, out dt));
         } else {
            return false;
         }
      }

      public static System.DateTime ToDateTime(this string s) {
         System.DateTime dtr;
         bool tryDtr = System.DateTime.TryParse(s, out dtr);
         Ensures.That(tryDtr, "is date");

         return dtr;
      }

   }
}

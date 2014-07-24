using SCG = System.Collections.Generic;
namespace Microline {
   public static class StringExtensions {
      public static string ToStringInv(this float value) {
         return value.ToString(System.Globalization.CultureInfo.InvariantCulture);
      }

      public static string ToStringInv(this double value) {
         return value.ToString(System.Globalization.CultureInfo.InvariantCulture);
      }

      public static string ToStringInv(this byte value) {
         return value.ToString(System.Globalization.CultureInfo.InvariantCulture);
      }

      public static string ToStringInv(this int value) {
         return value.ToString(System.Globalization.CultureInfo.InvariantCulture);
      }

      public static string ToStringInv(this double value, string format) {
         if (string.IsNullOrEmpty(format)) {
            return value.ToStringInv();
         } else {
            return value.ToString(format, System.Globalization.CultureInfo.InvariantCulture);
         }
      }

      public static string ToStringInv(this short value) {
         return value.ToString(System.Globalization.CultureInfo.InvariantCulture);
      }

      public static string ToDollarQuotedString(this string input) {
         return string.IsNullOrEmpty(input)
            ? "$$$$"
            : string.Format("$${0}$$", input);
      }

      public static string Strip(this string input, char character) {
         string s = string.IsNullOrEmpty(input)
            ? input
            : input.Replace(character.ToString(), "");

         return s;
      }

      public static string ToUniversalString(this System.DateTime dateTime) {
         const string UNIVERSAL_SORTABLE_DATE_TIME_PATTERN = "u";
         return dateTime.ToString(UNIVERSAL_SORTABLE_DATE_TIME_PATTERN);
      }

      // Enable quick and more natural string.Format calls
      // "This is a {0}".With("test")
      public static string With(this string s, params object[] args) {
         return string.Format(s, args);
      }

      // Limits the length of text to a defined length.
      public static string TakeFirst(this string source, int maxLength) {
         if (string.IsNullOrEmpty(source) || source.Length <= maxLength) {
            return source;
         } else if (maxLength < 0) {
            return string.Empty;
         } else {
            return source.Substring(0, maxLength);
         }
      }

      /*
       * Ottiene gli ultimi chars caratteri della stringa
       */
      public static string TakeLast(this string source, int chars) {
         if (string.IsNullOrEmpty(source) || source.Length <= chars || chars < 0) {
            return source;
         } else {
            return source.Substring(source.Length - chars, chars);
         }
      }

      /*
       * Concatenates a specified separator String between each element of a specified enumeration,
       * yielding a single concaten
       */
      public static string Concat<T>(this SCG.IEnumerable<T> list, string separator) {
         System.Text.StringBuilder sb = new System.Text.StringBuilder();
         foreach (var obj in list) {
            if (sb.Length > 0) {
               sb.Append(separator);
            }
            sb.Append(obj);
         }
         return sb.ToString();
      }

   }
}

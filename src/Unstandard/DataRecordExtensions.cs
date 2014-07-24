namespace Microline {
   public static class DataRecordExtensions {
      public static string GetStringOrDefault(this System.Data.IDataRecord record, int ordinal) {
         if (record.IsDBNull(ordinal)) {
            return string.Empty;
         } else {
            return record.GetString(ordinal);
         }
      }

      private const long NULL_ID = -1;
      public static long GetLongOrDefault(this System.Data.IDataRecord record, int ordinal) {
         if (record.IsDBNull(ordinal)) {
            return NULL_ID;
         } else {
            return record.GetInt64(ordinal);
         }
      }

      public static int GetIntOrDefault(this System.Data.IDataRecord record, int ordinal) {
         if (record.IsDBNull(ordinal)) {
            return 0;
         } else {
            return record.GetInt32(ordinal);
         }
      }

      public static bool GetBoolOrDefault(this System.Data.IDataRecord record, int ordinal) {
         if (record.IsDBNull(ordinal)) {
            return false;
         } else {
            return record.GetBoolean(ordinal);
         }
      }


      public static short GetShortOrDefault(this System.Data.IDataRecord record, int ordinal) {
         if (record.IsDBNull(ordinal)) {
            return (short)0;
         } else {
            return record.GetInt16(ordinal);
         }
      }

      public static float GetFloatOrDefault(this System.Data.IDataRecord record, int ordinal) {
         if (record.IsDBNull(ordinal)) {
            return 0F;
         } else {
            return record.GetFloat(ordinal);
         }
      }

      public static double GetDoubleOrDefault(this System.Data.IDataRecord record, int ordinal) {
         if (record.IsDBNull(ordinal)) {
            return 0D;
         } else {
            return record.GetDouble(ordinal);
         }
      }

      public static System.DateTime GetDateTimeOrDefault(this System.Data.IDataRecord record, int ordinal) {
         if (!record.IsDBNull(ordinal)) {
            return record.GetDateTime(ordinal);
         } else {
            return new System.DateTime(1900, 1, 1);
         }
      }
   }
}

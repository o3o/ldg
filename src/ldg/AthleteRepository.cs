using Microline;
using SCG = System.Collections.Generic;
namespace Talaran.Ldg {
   public class AthleteRepository: IAthleteRepository {
      private static readonly log4net.ILog log =
         log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
      private readonly System.Data.IDbCommand command;
      public AthleteRepository(System.Data.IDbCommand command) {
         if (command == null) {
            throw new System.ArgumentNullException("command", "command cannot be null");
         } else {
            this.command = command;
         }
      }

      public bool IsNew(Athlete athlete) {
         command.CommandText = "SELECT id FROM at";
         return command.ExecuteScalar() != null;
      }

      public void Update(Athlete athlete) {
         string sql = "UPDATE at SET ";
         sql += string.Format ("name = \"{0}\", SET surname = \"{1}\", SET year = {2}, SET gender = '{3}', SET time = '{4}' ",
               athlete.Name.Strip('"'),
               athlete.Surname.Strip('"'),
               athlete.Year,
               athlete.Gender,
               athlete.Time.Strip(',')
               );
         sql += string.Format("WHERE id={0}", athlete.Id);
         command.CommandText = sql;
         command.ExecuteNonQuery();
      }

      public void UpdateTime(int id, string time) {
         string sql = "UPDATE at SET ";
         sql += string.Format("time = '{0}' ", time);
         sql += string.Format("WHERE id={0}", id);
         command.CommandText = sql;
         command.ExecuteNonQuery();
      }

      public void  DeleteAll() {
         command.CommandText = "DELETE FROM at";
         command.ExecuteNonQuery();
      }

      public void Insert(Athlete athlete) {
         try {
            command.CommandText = string.Format("INSERT INTO at ('id', 'name', 'surname', 'year', 'gender', 'time') VALUES({0},\"{1}\",\"{2}\",{3},'{4}', '{5}');",
                  athlete.Id,
                  athlete.Name.Strip('"'),
                  athlete.Surname.Strip('"'),
                  athlete.Year,
                  athlete.Gender,
                  athlete.Time.Strip(',')
                  );
            command.ExecuteNonQuery();

         } catch (System.Exception ex) {
            if (log.IsDebugEnabled) log.Debug(command.CommandText);
            if (log.IsErrorEnabled) log.Error(ex);
            throw;
         }
      }

      public SCG.List<Athlete> Query(string sql) {
         command.CommandText = sql;
         var list = new SCG.List<Athlete>();
         using (var reader = command.ExecuteReader()) {
            while (reader.Read()) {
               var at = new Athlete();

               at.Id = reader.GetInt32(0);
               at.Name = reader.GetString(1);
               at.Surname = reader.GetString(2);
               at.Year = reader.GetInt32(3);
               at.Gender = reader.GetString(4);
               at.Time = reader.GetString(5);
               list.Add(at);
            }
         }
         return list;
      }
   }
}

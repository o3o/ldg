using SCG = System.Collections.Generic;
namespace Talaran.Ldg {
   public interface IAthleteRepository {
      void Update(Athlete athlete);
      void DeleteAll();
      SCG.List<Athlete> Query(string sql);
   }

}

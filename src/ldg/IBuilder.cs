namespace Talaran.Ldg {
   public interface IBuilder {
      void BeginReport(string title, int year);
      void EndReport();
      void Add(Athlete athete);
   }
}
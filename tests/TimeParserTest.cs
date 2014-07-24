using PetaTest;
namespace Talaran.Ldg.Test {
   [TestFixture]
   public class TimeParserTest {
      [Test("2+5+36", "02:05.36")]
      [Test("+5+36", "00:05.36")]
      [Test("+5", "00:05.00")]
      [Test("2+5", "02:05.00")]
      [Test("2+0", "02:00.00")]

      [Test("2/5/36", "02:05.36")]
      [Test("/5/36", "00:05.36")]
      [Test("/5", "00:05.00")]
      [Test("2/5", "02:05.00")]
      [Test("2/0", "02:00.00")]

      [Test("2.5.36", "02:05.36")]
      [Test(".5.36", "00:05.36")]
      [Test(".5", "00:05.00")]
      [Test("2.5", "02:05.00")]
      [Test("2.0", "02:00.00")]
      [Test("rit", "99:99.99")]
      [Test("R", "99:99.99")]
      public void Parse(string input, string output) {
         var parser = new TimeParser();
         Assert.AreEqual(parser.Parse(input), output);
      }
   }
}

using System;
using KaratInterview;

namespace UnitTests {
   public class RegressionTests {
      [Fact]
      public void Test_00() {
         // Using null forgiving operator to avoid compiler warning
         string[][] nullValue = default!;

         Assert.Throws<ArgumentException>(() => new BadgeLogValidator(nullValue));
         Assert.Throws<ArgumentException>(() => new BadgeLogValidator(new string[0][]));
      }

      [Fact]
      public void Test_01() {
         var log = new BadgeLogValidator(Data.Records2);

         Assert.True(log.GetUsersWhoEnteredWithoutExiting().Count == 0);
         Assert.True(log.GetUsersWhoExitedWithoutEntering().Count == 0);
      }

      [Fact]
      public void Test_02() {
         var log = new BadgeLogValidator(Data.Records3);

         Assert.Equal(new List<string>() { "Paul" }, log.GetUsersWhoEnteredWithoutExiting());
         Assert.Equal(new List<string>() { "Paul" }, log.GetUsersWhoExitedWithoutEntering());
      }

      [Fact]
      public void Test_03() {
         var log = new BadgeLogValidator(Data.Records4);

         Assert.True(log.GetUsersWhoEnteredWithoutExiting().Count == 0);
         Assert.Equal(new List<string>() { "Martha" }, log.GetUsersWhoExitedWithoutEntering());
      }

      [Fact]
      public void Test_04() {
         var log = new BadgeLogValidator(Data.Records1);

         Assert.Equal(new List<string>() { "Steve", "Curtis", "Paul", "Joe" }.OrderBy(x => x), log.GetUsersWhoEnteredWithoutExiting().OrderBy(x => x));
         Assert.Equal(new List<string>() { "Martha", "Pauline", "Curtis", "Joe" }.OrderBy(x => x), log.GetUsersWhoExitedWithoutEntering().OrderBy(x => x));
      }

      [Fact]
      public void Test_05() {
         var log = new BadgeLogValidator(Data.Records5);

         Assert.Equal(new List<string>() { "Martha", "Joe" }.OrderBy(x => x), log.GetUsersWhoEnteredWithoutExiting().OrderBy(x => x));
         Assert.Equal(new List<string>() { "Martha", "Paul" }.OrderBy(x => x), log.GetUsersWhoExitedWithoutEntering().OrderBy(x => x));
      }
   }
}
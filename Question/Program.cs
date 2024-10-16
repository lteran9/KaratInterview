using System;

namespace KaratInterview {
   internal class Program {


      static void Main(string[] args) {
         var logValidator = new BadgeLogValidator(Data.Records1);

         Console.WriteLine(string.Join(", ", logValidator.GetUsersWhoEnteredWithoutExiting()));
         Console.WriteLine(string.Join(", ", logValidator.GetUsersWhoExitedWithoutEntering()));
      }
   }
}
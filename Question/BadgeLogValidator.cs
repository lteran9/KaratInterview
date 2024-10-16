using System;

namespace KaratInterview {
   public class BadgeLogValidator {
      /// <summary>
      /// Data structure to hold all badge log entries. Is readonly to prevent data overwrite after constructor initialization.
      /// </summary>
      /// <returns></returns>
      private readonly Dictionary<string, Queue<string>> _log = new Dictionary<string, Queue<string>>();

      public BadgeLogValidator(string[][] data) {
         if (data?.Length > 0) {
            for (int i = 0; i < data.Length; i++) {
               if (data[i].Length > 1) {
                  if (data[i][1] == "enter") {
                     UserEnteredBuilding(data[i][0]);
                  }

                  if (data[i][1] == "exit") {
                     UserExitedBuilding(data[i][0]);
                  }
               }
            }
         } else {
            throw new ArgumentException("Cannot run program with empty dataset.");
         }
      }

      /// <summary>
      /// Add record of a user entering the building.
      /// </summary>
      /// <param name="name"></param>
      private void UserEnteredBuilding(string name) {
         if (_log.ContainsKey(name) == false) {
            _log.Add(name, new Queue<string>());
         }

         _log[name].Enqueue(nameof(LogRecord.Entry));
      }

      /// <summary>
      /// Add record of a user exiting the building.
      /// </summary>
      /// <param name="name"></param>
      private void UserExitedBuilding(string name) {
         if (_log.ContainsKey(name) == false) {
            _log.Add(name, new Queue<string>());
         }

         _log[name].Enqueue(nameof(LogRecord.Exit));
      }

      /// <summary>
      /// Run algorithm on badge access log to return users who entered and did not exit.
      /// </summary>
      /// <returns></returns>
      public List<string> GetUsersWhoEnteredWithoutExiting() {
         var result = new List<string>();

         if (_log.Any()) {
            foreach (var user in _log) {
               var userQ = new Queue<string>(user.Value);

               while (userQ.Count > 0) {
                  var record = userQ.Dequeue();

                  if (record == nameof(LogRecord.Entry)) {
                     if (userQ.Count > 0) {
                        var mustBeExit = userQ.Dequeue();
                        if (mustBeExit != nameof(LogRecord.Exit)) {
                           // We have an entry record but the following badge log is not an exit
                           result.Add(user.Key);
                        }
                     } else {
                        // We have an entry record but have run out of badge logs
                        result.Add(user.Key);
                     }
                  }
               }
            }
         }

         return result.Distinct().ToList();
      }

      /// <summary>
      /// Run algorithm on badge access log to return users who exited without entering.
      /// </summary>
      /// <returns></returns> 
      public List<string> GetUsersWhoExitedWithoutEntering() {
         var result = new List<string>();

         if (_log.Any()) {
            foreach (var user in _log) {
               var userQ = new Queue<string>(user.Value);

               bool hasEntry = false;
               while (userQ.Count > 0) {
                  var record = userQ.Dequeue();

                  if (record == nameof(LogRecord.Entry)) {
                     hasEntry = true;
                  } else if (record == nameof(LogRecord.Exit)) {
                     // For every exit we should have an entry
                     if (hasEntry) {
                        // Consume entry
                        hasEntry = false;
                     } else {
                        // We have an exit with no associated entry
                        result.Add(user.Key);
                     }
                  }
               }
            }
         }

         return result.Distinct().ToList();
      }

      /// <summary>
      /// Using enum value for comparison instead of raw strings which are error prone.
      /// </summary>
      private enum LogRecord {
         Entry,
         Exit
      }
   }
}
using System.Runtime.InteropServices;
 

namespace VisualAlarm 
{
    class ConsoleManager
    {
        private static ConsoleColor _consoleColor = ConsoleColor.Black;

        private static List<TransientAlarm> alarms = new List<TransientAlarm>();

        public static void AddAlarm(TransientAlarm alarm) {
            alarms.Add(alarm);
        }

        public static ConsoleColor ConsoleColor
        {
            get {
                return _consoleColor;
            }
            set {
                _consoleColor = value;

                // If debugging, can't change the console color, just print it
                if (System.Diagnostics.Debugger.IsAttached) {
                    Console.WriteLine(value.ToString());
                }
                else {
                    Console.BackgroundColor = _consoleColor;
                    Console.Clear();
                }

            }
        }

        public static void RevertConsoleColor(TransientAlarm alarm) {
            var remainingAlarms = alarms.Where(a => a != alarm && a.IsFlashing );

            // If any other alarms are still active, revert to the last one's color
            if (remainingAlarms.Any()) {
                ConsoleColor = remainingAlarms.Last().targetConsoleColor;
            }
            // Otherwise, revert to black
            else {
                ConsoleColor = ConsoleColor.Black;
            }
        }
    }
}
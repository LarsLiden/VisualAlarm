using System.Runtime.InteropServices;
 

namespace VisualAlarm 
{
    class ConsoleManager
    {
        private static ConsoleColor _consoleColor = ConsoleColor.Black;

        private static List<TransientAlarm> alarms = new List<TransientAlarm>();

        private static TransientAlarm? currentAlarm = null;

        public static void AddAlarm(TransientAlarm alarm) {
            alarms.Add(alarm);
        }

        public static void Start() {
            // Start timer for flashing the console
            Timer timer = new Timer(Flash, null, 0, 50);
        }

        private static void Flash(Object? stateInfo) {
            // Get active alarms
            var activeAlarms = alarms.Where(a => a.IsFlashing);

            // If no active alarms, revert to black
            if (!activeAlarms.Any()) {
                ConsoleColor = ConsoleColor.Black;
                return;
            }

            TransientAlarm? previousAlarm = null;

            // Update the current alarm
            if (currentAlarm == null) {
                currentAlarm = activeAlarms.First();
            }
            else if (activeAlarms.Count() == 1) {
                previousAlarm = null;
                currentAlarm = activeAlarms.First();
            }
            else {
                var currentIndex = activeAlarms.ToList().IndexOf(currentAlarm);
                if (currentIndex == activeAlarms.Count() - 1) {
                    previousAlarm = currentAlarm;
                    currentAlarm = activeAlarms.First();
                }
                else {
                    previousAlarm = currentAlarm;
                    currentAlarm = activeAlarms.ElementAt(currentIndex + 1);
                }
            }

            var offColor = (previousAlarm != null) ? previousAlarm.targetConsoleColor : ConsoleColor.Black;
            currentAlarm.Flash(offColor);
        }


        public static ConsoleColor ConsoleColor
        {
            get {
                return _consoleColor;
            }
            set {
                if (_consoleColor == value)
                {
                    return;
                }

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
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace VisualAlarm 
{
    class AlarmManager
    {
        private static ConsoleColor _consoleColor = ConsoleColor.Black;

        private static List<Alarm> alarms = new List<Alarm>();

        private static TransientAlarm? currentTransientAlarm = null;

        private static Timer? timer;

        public static void AddAlarm(Alarm alarm) {
            alarms.Add(alarm);
        }

        public static void Start() {
            // Start timer for flashing the console
            timer = new Timer(Flash, null, 0, 50);
        }

        private static ConsoleColor BackgroundColor {
            get {
                // Get transient alarms
                var waitAlarms = alarms.OfType<WaitAlarm>();

                // Get active alarms
                var activeAlarms = waitAlarms.Where(a => a.isActive);

                //  Only one alarm can be active at a time
                if (activeAlarms.Any()) {
                    return activeAlarms.First().targetConsoleColor;
                }
                return ConsoleColor.Black;
            }
        }

        public static void ReleaseWaitAlarm() {
            // Get transient alarms
            var waitAlarms = alarms.OfType<WaitAlarm>();

            // Get active alarms
            var activeAlarms = waitAlarms.Where(a => a.isActive);

            //  Only one alarm can be active at a time
            if (activeAlarms.Any()) {
                activeAlarms.First().isActive = false;
            }
            ConsoleColor = BackgroundColor;
        }

        private static void Flash(Object? stateInfo) 
        {
            // Get transient alarms
            var transientAlarms = alarms.OfType<TransientAlarm>();

            // Get active alarms
            var activeAlarms = transientAlarms.Where(a => a.IsFlashing);

            // If no active alarms, revert to black
            if (!activeAlarms.Any()) {
                ConsoleColor = BackgroundColor;
                return;
            }
            else {
                TransientAlarm? previousAlarm = null;

                // Update the current alarm
                if (currentTransientAlarm == null) {
                    currentTransientAlarm = activeAlarms.First();
                }
                else if (activeAlarms.Count() == 1) {
                    previousAlarm = null;
                    currentTransientAlarm = activeAlarms.First();
                }
                else {
                    var currentIndex = activeAlarms.ToList().IndexOf(currentTransientAlarm);
                    if (currentIndex == activeAlarms.Count() - 1) {
                        previousAlarm = currentTransientAlarm;
                        currentTransientAlarm = activeAlarms.First();
                    }
                    else {
                        previousAlarm = currentTransientAlarm;
                        currentTransientAlarm = activeAlarms.ElementAt(currentIndex + 1);
                    }
                }

                var offColor = (previousAlarm != null) ? previousAlarm.targetConsoleColor : BackgroundColor;
                currentTransientAlarm.Flash(offColor);
            }
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
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"{Status()}");
                }
            }
        }

        public static string Status() {
            string statusString = "";
            foreach (var alarm in alarms) {
                statusString += $"[{alarm.Status()}] ";
            }
            return statusString;
        }
    }
}
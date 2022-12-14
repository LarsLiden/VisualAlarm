namespace VisualAlarm 
{
    class Alarm {

        public ConsoleColor targetConsoleColor;

        private TimerPlus? timer;

        public Alarm(ConsoleColor targetConsoleColor, double flashFrequency) {
            this.targetConsoleColor = targetConsoleColor;

            // Start timer for frequency of the alarm
            timer = new TimerPlus(this.Trigger, null, 0, flashFrequency);
        }

        virtual protected void Trigger(Object? stateInfo) {}

        private string NextTriggerTime {
            get {
                if (timer != null) {
                    return timer.Next.ToString("HH:mm");;
                }
                else {
                    return null;
                }
            }
        }

        public string Status() {
            return $"{targetConsoleColor} {NextTriggerTime}";
        }
    }
}
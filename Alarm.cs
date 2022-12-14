namespace VisualAlarm 
{
    class Alarm {

        public ConsoleColor targetConsoleColor;

        public Alarm(ConsoleColor targetConsoleColor, double frequency) {
            this.targetConsoleColor = targetConsoleColor;
            var flashFrequency = (int)(frequency * 1000);

            // Start timer for frequency of the alarm
            Timer timer = new Timer(this.Trigger, null, 0, flashFrequency);
        }

        virtual protected void Trigger(Object? stateInfo) {}
    }
}
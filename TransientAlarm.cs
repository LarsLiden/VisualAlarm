namespace VisualAlarm 
{
    class TransientAlarm {

        long endFlashTime;

        long flashDuration;

        public ConsoleColor targetConsoleColor;

        public TransientAlarm(AlarmSetting alarmSetting)  {

            this.targetConsoleColor = alarmSetting.targetConsoleColor;

            this.flashDuration = (int)(alarmSetting.duration * 1000);
            var flashFrequency = (int)(alarmSetting.frequency * 1000);

            // Start timer for frequency of the alarm
            Timer timer = new Timer(this.StartFlash, null, 0, flashFrequency);
        }

        public bool IsFlashing {
            get {
                var curTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                return curTime < endFlashTime;
            }
        }

        private void StartFlash(Object? stateInfo) {
            // Reset endTime to restart flashing
            var startTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            endFlashTime = startTime + flashDuration;
        }

        public void Flash(ConsoleColor offColor)
        {
            var curTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            if (curTime < endFlashTime)
            {
                if (ConsoleManager.ConsoleColor != targetConsoleColor) {
                    ConsoleManager.ConsoleColor = targetConsoleColor;
                }
                else {
                    ConsoleManager.ConsoleColor = offColor;
                }
            }
        }
    }
}
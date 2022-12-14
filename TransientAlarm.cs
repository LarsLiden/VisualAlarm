namespace VisualAlarm 
{
    class TransientAlarm : Alarm {

        long endFlashTime;

        long flashDuration;
        
        public TransientAlarm(AlarmSetting alarmSetting) : base(alarmSetting.targetConsoleColor, alarmSetting.frequency) {
            this.flashDuration = (int)(alarmSetting.duration * 1000);
        }

        public bool IsFlashing {
            get {
                var curTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                return curTime < endFlashTime;
            }
        }

        override protected void Trigger(Object? stateInfo) {
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
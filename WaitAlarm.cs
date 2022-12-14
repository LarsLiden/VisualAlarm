namespace VisualAlarm 
{
    class WaitAlarm : Alarm {

        public bool isActive = false;

        public WaitAlarm(AlarmSetting alarmSetting) : base(alarmSetting.targetConsoleColor, alarmSetting.frequency) {}

        override protected void Trigger(Object? stateInfo) {
            isActive = true;
        }
    }
}
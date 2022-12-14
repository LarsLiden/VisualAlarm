using System.Runtime.InteropServices;
 

namespace VisualAlarm 
{
    internal class VisualAlarm
    {
        const int TEN_MINUTES = 600000;

        static void Main(string[] args)
        {
            AlarmSetting.LoadAlarmSettings();
            foreach (var alarmSetting in AlarmSetting.alarmSettings) {
                ConsoleManager.AddAlarm(new TransientAlarm(alarmSetting));
            }

            var autoEvent = new AutoResetEvent(false);
            ConsoleManager.Start();
            autoEvent.WaitOne();
        }
    }
}
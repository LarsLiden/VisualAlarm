using System.Runtime.InteropServices;
 

namespace VisualAlarm 
{
    internal class VisualAlarm
    {
        const int TEN_MINUTES = 600000;

        static void Main(string[] args)
        {
            AlarmSetting.LoadAlarmSettings();

            ConsoleManager.Start();
            while (true)
            {
                 // If debugging, can't read 
                if (!System.Diagnostics.Debugger.IsAttached) {
                    Console.ReadKey();
                    ConsoleManager.ReleaseWaitAlarm();
                }
            }
        }
    }
}
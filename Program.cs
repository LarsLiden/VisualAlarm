using System.Runtime.InteropServices;
 

namespace VisualAlarm 
{
    internal class VisualAlarm
    {
        const int TEN_MINUTES = 600000;

        static void Main(string[] args)
        {
            var autoEvent = new AutoResetEvent(false);
            var alarm = new TransientAlarm(ConsoleColor.Red, 60, 1);
            var alarm2 = new TransientAlarm(ConsoleColor.Yellow, 30, 2);
            var alarm3 = new TransientAlarm(ConsoleColor.Blue, 60, 3);
            ConsoleManager.Start();
            autoEvent.WaitOne();
        }
    }
}
using System.Runtime.InteropServices;
 

namespace VisualAlarm 
{
    internal class VisualAlarm
    {
        const int TEN_MINUTES = 600000;

        static void Main(string[] args)
        {
            var autoEvent = new AutoResetEvent(false);
            var alarm = new TransientAlarm(ConsoleColor.Red, 30);
            var alarm2 = new TransientAlarm(ConsoleColor.Yellow, 15);
            autoEvent.WaitOne();
        }
    }
}
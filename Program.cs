using System.Runtime.InteropServices;
 

namespace VisualAlarm 
{
    internal class VisualAlarm
    {
        const int TEN_MINUTES = 600000;

        static void Main(string[] args)
        {
            var autoEvent = new AutoResetEvent(false);

            var consoleManager = new ConsoleManager();
            Timer timer = new Timer(consoleManager.StartFlash, autoEvent, 1000, TEN_MINUTES);

            autoEvent.WaitOne();
        }
    }

    class ConsoleManager
    {           
        private int flashCount = 0;
        private ConsoleColor consoleColor = ConsoleColor.Black;

        const int FLASH_ITERATIONS = 100;

        public ConsoleManager() {
            ChangeConsoleBackground(ConsoleColor.Black);
        }

        public void StartFlash(Object? stateInfo) {
            var autoEvent = new AutoResetEvent(false);

            var consoleManager = new ConsoleManager();
            Timer timer = new Timer(consoleManager.Flash, autoEvent, 0, 20);
            autoEvent.WaitOne();
            timer.Dispose();
        }

        private void EndFlash() {
            flashCount = 0;
            ChangeConsoleBackground(ConsoleColor.Black);
        }

        public void Flash(Object stateInfo)
        {
            AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;
            if(++flashCount == FLASH_ITERATIONS)
            {
                // Reset the counter and signal the waiting thread.
                EndFlash();
                autoEvent.Set();
            }
            else if (consoleColor != ConsoleColor.Red) {
                ChangeConsoleBackground(ConsoleColor.Red);
            }
            else {
                ChangeConsoleBackground(ConsoleColor.Yellow);
            }
        }

        private void ChangeConsoleBackground(ConsoleColor color)
        {
            consoleColor = color;
            Console.BackgroundColor = consoleColor;
            Console.Clear();
        }
    }
}
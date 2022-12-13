namespace VisualAlarm 
{
    class TransientAlarm {

        const int FLASH_ITERATIONS = 100;

        // How often to trigger the alarm in seconds
        int frequency;

        // How many times has it flashed
        private int flashCount = FLASH_ITERATIONS;

        public ConsoleColor targetConsoleColor;

        public TransientAlarm(ConsoleColor targetConsoleColor, int frequency = 10) {
            this.targetConsoleColor = targetConsoleColor;
            this.frequency = frequency;

            // Start timer for frequency of the alarm
           // var autoEvent = new AutoResetEvent(false);
            Timer timer = new Timer(this.StartFlash, null, 1000, (frequency * 1000));
          //  autoEvent.WaitOne();
            ConsoleManager.AddAlarm(this);
        }

        public bool IsFlashing {
            get {
                return flashCount < FLASH_ITERATIONS;
            }
        }

        // Start timer for alarm flashing
        private void StartFlash(Object? stateInfo) {
            flashCount = 0;

            // Start the flash timer
            var flashAutoEvent = new AutoResetEvent(false);
            Timer timer = new Timer(Flash, flashAutoEvent, 0, 20);
            flashAutoEvent.WaitOne();

            // When disposed end the flash
            timer.Dispose();
            ConsoleManager.RevertConsoleColor(this);
        }

        private void Flash(Object? stateInfo)
        {
            if (stateInfo == null) {
                throw new ArgumentNullException(nameof(stateInfo));
            }

            AutoResetEvent flashAutoEvent = (AutoResetEvent)stateInfo;
            if(++flashCount == FLASH_ITERATIONS)
            {
                flashAutoEvent.Set();
            }
            else if (ConsoleManager.ConsoleColor != targetConsoleColor) {
                ConsoleManager.ConsoleColor = targetConsoleColor;
            }
            else {
                ConsoleManager.RevertConsoleColor(this);
            }
        }
    }
}
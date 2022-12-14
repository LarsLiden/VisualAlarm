namespace VisualAlarm 
{
public class TimerPlus : IDisposable
    {
        private readonly TimerCallback _realCallback;
        private readonly Timer _timer;
        private TimeSpan _period;
        private DateTime _next;

        public TimerPlus(TimerCallback callback, object? state, double dueTime, double period)
        {
            TimeSpan dueTimeSpan = TimeSpan.FromSeconds(dueTime);
            TimeSpan periodTimeSpan = TimeSpan.FromSeconds(period);

            _timer = new Timer(Callback, state, dueTimeSpan, periodTimeSpan);
            _realCallback = callback;
            _period = periodTimeSpan;
            _next = DateTime.Now.Add(dueTimeSpan);
        }

        private void Callback(object? state)
        {
            _next = DateTime.Now.Add(_period);
            _realCallback(state);
        }

        public TimeSpan Period => _period;
        public DateTime Next => _next;
        public TimeSpan DueTime => _next - DateTime.Now;

        public bool Change(TimeSpan dueTime, TimeSpan period)
        {
            _period = period;
            _next = DateTime.Now.Add(dueTime);
            return _timer.Change(dueTime, period);
        }

        public void Dispose() => _timer.Dispose();
    }
}
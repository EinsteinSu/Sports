using System;
using System.Threading;
using System.Threading.Tasks;
using Sports.Timing.Interfaces;

namespace Sports.Timing
{
    public class TimingController : ITimingController, IDisposable
    {
        private readonly ManualResetEvent _resetEvent = new ManualResetEvent(false);
        private long _initializeSeconds;
        protected TimeSpan Time;
        protected Task TimingTask;
        protected CancellationTokenSource TokenSource = new CancellationTokenSource();

        public TimingController()
        {
            TimingTask = new Task(Timing, TokenSource.Token);
            TimingTask.Start();
        }

        public TimingType Type { get; set; }

        public long InitializeSeconds
        {
            get => _initializeSeconds;
            set
            {
                _initializeSeconds = value;
                Time = TimeSpan.FromSeconds(value);
            }
        }

        public Action<string> DisplayAction { get; set; }

        public virtual string DisplayTime => $"{Time.Minutes:00}:{Time.Seconds:00}";

        public void Dispose()
        {
            TokenSource.Cancel();
        }

        public void Start()
        {
            if (TimingTask.Status == TaskStatus.RanToCompletion)
            {
                TimingTask = new Task(Timing, TokenSource.Token);
                TimingTask.Start();
            }
            _resetEvent.Set();
        }

        public void Pause()
        {
            _resetEvent.Reset();
        }

        public void Reset()
        {
            Time = TimeSpan.FromSeconds(InitializeSeconds);
        }

        protected virtual int TimeInterval => 100;

        private void Timing()
        {
            while (true)
            {
                if (Time.Milliseconds < 0)
                    break;
                Console.WriteLine(TimingTask.Status);
                _resetEvent.WaitOne();
                Time = Type == TimingType.Increase
                    ? Time.Add(TimeSpan.FromMilliseconds(TimeInterval))
                    : Time.Add(-TimeSpan.FromMilliseconds(TimeInterval));
                if (TokenSource.Token.IsCancellationRequested)
                {
                    Console.WriteLine("Cancelled");
                    break;
                }
                if (Type == TimingType.Decrease && Time.Seconds == 0 && Time.Milliseconds == 0)
                {
                    Console.WriteLine("Time up");
                    break;
                }
                DisplayAction?.Invoke(DisplayTime);
                Thread.Sleep(TimeInterval);
            }
        }
    }
}
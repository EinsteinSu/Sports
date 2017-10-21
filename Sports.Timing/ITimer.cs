namespace Sports.Timing
{
    public interface ITimer
    {
        string Timing { get; }
        void Start();

        void Stop();

        void Reset();

        void Pause();
    }
}
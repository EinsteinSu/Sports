namespace Sports.Timing.Interfaces
{
    public interface ITimingController
    {
        void Start();

        void Pause();

        void Reset();
    }

    public enum TimingType
    {
        Increase,
        Decrease
    }
}

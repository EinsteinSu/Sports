using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sports.Timing
{
    public class DecreaseTimer : ITimer
    {
        private readonly TimeSpan _timeSpan;
        protected TimeSpan UsableTimeSpan;
        private Task timingTask;
        public DecreaseTimer(int hour, int second, int millionSecond)
        {
            _timeSpan = new TimeSpan(hour, second, millionSecond);
            UsableTimeSpan = _timeSpan;
            timingTask = new Task(TimingProcessing);
        }

        public virtual string DisplayFormat { get; set; }

        public string Timing => UsableTimeSpan.ToString(DisplayFormat);

        protected virtual void TimingProcessing()
        {
            //todo: reduce the timespan
        }

        public void Start()
        {
            timingTask.Start();
        }

        public void Stop()
        {
            
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }
    }
}

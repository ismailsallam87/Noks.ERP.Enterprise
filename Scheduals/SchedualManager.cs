using Scheduals.Event;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduals
{
  public  class ScheduleManager
    {
        private static Timer _timer = new Timer(state => ScheduleJobs(), null, Timeout.Infinite, Timeout.Infinite);

        private static void ScheduleJobs()
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
            _schedule.Sort();

            if (!_schedule.Any())
                return;

            var firstJob = _schedule.First();
            if (firstJob.RunTime <= Now)
            {
                RunJob(firstJob);
                if (firstJob.CalculateNextRun == null)
                {
                    // probably a ToRunNow().DelayFor() job, there's no CalculateNextRun
                }
                else
                {
                    firstJob.RunTime = firstJob.CalculateNextRun(Now.AddMilliseconds(1));
                }

                if (firstJob.RunTime <= Now || firstJob.IsRunOnce)
                {
                    _schedule.Remove(firstJob);
                }

                firstJob.IsRunOnce = false;
                ScheduleJobs();
                return;
            }

            var interval = firstJob.RunTime - Now;

            if (interval <= TimeSpan.Zero)
            {
                ScheduleJobs();
                return;
            }
            else
            {
                if (interval.TotalMilliseconds > _maxTimerInterval)
                    interval = TimeSpan.FromMilliseconds(_maxTimerInterval);

                _timer.Change(interval, interval);
            }
        }

        internal  int addSchedual(Schedule schedule)
        {

            var l = new List<Schedule>();
            l.Add(schedule);
            CalcNextRun(l).ToList().ForEach(RunJob);
            ScheduleJobs();
            return schedule.Id;
        }

       

        static ScheduleCollection _schedule = new ScheduleCollection();
        private const uint _maxTimerInterval = (uint)0xfffffffe;

        public static DateTime Now { get { return DateTime.Now; } }
        public static void Intialised(Register register)
        {
            StartIntialised(register.scedualList);
            Start();
        }

        private static void Start()
        {
            ScheduleJobs();
        }
     
        private static void StartIntialised(List<Schedule> list)
        {
            CalcNextRun(list).ToList().ForEach(RunJob); ;

        }
        public static event Action<JobExceptionInfo> JobException;

        /// <summary>
        /// Event raised when a job starts.
        /// </summary>
       
        public static event Action<JobStartInfo> JobStart;

        /// <summary>
        /// Evemt raised when a job ends.
        /// </summary>
      
        public static event Action<JobEndInfo> JobEnd;
        private static void RunJob(Schedule schedule)
        {
            if (schedule.ISDisposed)
            {
                return;
            }
            var task = new Task(() =>
            {
                schedule.State = "Running";
                var start = Now;

                if (JobStart != null)
                {
                    JobStart(
                        new JobStartInfo
                        {
                            Name = schedule.Name,
                            StartTime = start,
                        }
                    );
                }

                var stopwatch = new Stopwatch();

                try
                {
                    stopwatch.Start();
                    schedule.Jops.ForEach(action => Task.Factory.StartNew(action.Excute).Wait());
                }
                catch (Exception e)
                {
                    if (JobException != null)
                    {
                        var aggregate = e as AggregateException;

                        if (aggregate != null && aggregate.InnerExceptions.Count == 1)
                            e = aggregate.InnerExceptions.Single();

                        JobException(
                           new JobExceptionInfo
                           {
                               Name = schedule.Name,
                               Exception = e,
                           }
                       );
                    }
                }
                finally
                {

                    schedule.State = "Waiting!!!!!";
                    if (JobEnd != null)
                    {
                        JobEnd(
                            new JobEndInfo
                            {
                                Name = schedule.Name,
                                StartTime = start,
                                Duration = stopwatch.Elapsed,
                                NextRun = schedule.RunTime,
                            }
                        );
                    }
                }
            }, TaskCreationOptions.PreferFairness);
            task.Start();


        }

        private static IEnumerable<Schedule> CalcNextRun(List<Schedule> list)
        {
            foreach (var item in list)
            {
                if (item.CalculateNextRun==null)
                {
                    if (item.ISDisposed)
                    {

                    }
                    else
                    {
 yield return item;
                    }
                   
                }
                else
                {
                    item.RunTime = item.CalculateNextRun(Now.Add(item.DelayRunFor));
                    _schedule.Add(item);

                }
            }
           
        }
        public static void Stop()
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
        }
        public static void StopSchedual(int id)
        {
            _schedule.Stop(id);
            ScheduleJobs();
        }
        public static IEnumerable<Schedule> GetCurrent()
        {
            return _schedule.All();
        }
    }
}

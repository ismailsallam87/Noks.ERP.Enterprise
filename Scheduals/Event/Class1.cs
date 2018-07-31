using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduals.Event
{
    public class JobStartInfo
    {
        /// <summary>
        /// Name of the job.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Date and time of the start.
        /// </summary>
        public DateTime StartTime { get; set; }
    }
    public class JobExceptionInfo
    {
        /// <summary>
        /// Name of the job.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Job's exception.
        /// </summary>
        public Exception Exception { get; set; }
    }
    public class JobEndInfo
    {
        /// <summary>
        /// Name of the job.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Date and time of the start.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// The elapsed time of the job.
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Date and time of next run.
        /// </summary>
        public DateTime? NextRun { get; set; }
    }
}

namespace TestHelper
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SynchronousTaskScheduler : TaskScheduler
    {
        protected override IEnumerable<Task> GetScheduledTasks()
        {
            yield break;
        }

        protected override void QueueTask(Task task)
        {
            this.TryExecuteTask(task);
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            return this.TryExecuteTask(task);
        }
    }
}

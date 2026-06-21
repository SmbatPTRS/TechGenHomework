namespace JobSimulation;

public class Scheduler
{
    private JobQueue queue;

    public Scheduler(JobQueue queue)
    {
        this.queue = queue;
    }

    public event EventHandler<JobEventArgs>? JobStateChanged;

    public void ExecuteAll()
    {
        foreach (Job job in queue)
        {
            job.Status = JobStatus.Running;
            JobStateChanged?.Invoke(this, new JobEventArgs(job, "JobStarted", null));

            try
            {
                job.Executor(job);
                job.Status = JobStatus.Completed;
                JobStateChanged?.Invoke(this, new JobEventArgs(job, "JobCompleted", null));
            }
            catch (Exception ex)
            {
                job.Status = JobStatus.Failed;
                JobStateChanged?.Invoke(this, new JobEventArgs(job, "JobFailed", ex));
            }

            Console.WriteLine($"Job {job.Id} ({job.Name}) finished with status {job.Status}");
        }
    }
}
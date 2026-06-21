namespace JobSimulation
{
    public class Job
    {
        public int Id{get;set;}
        public string Name {get;set;}
        public JobStatus Status  {get;set;}
        public JobExecutor Executor  {get;set;}

        public int RetryFailuresBeforeSuccess;

        public Job(int id, string name, JobExecutor executor, int retryFailuresBeforeSuccess=3)
        {
            Id = id;
            Status = JobStatus.Pending;
            Name = name;
            Executor = executor;
            RetryFailuresBeforeSuccess = retryFailuresBeforeSuccess;
        }
    }
}
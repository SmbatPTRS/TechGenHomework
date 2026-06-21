namespace JobSimulation;

class Program
{
    static void Main(string[] args)
    {
        Job job1 = new Job(1, "fast_job1", Executors.FastExecutor, 3);
        Job job2 = new Job(2, "fail-fast", Executors.FastExecutor, 4);
        Job job3 = new Job(3, "fail-safe", Executors.SafeExecutor, 5);
        Job job4 = new Job(4, "slow-job", Executors.RetryExecutor, 2);
        Job job5 = new Job(5, "fail-retry", Executors.RetryExecutor, 6);

        JobQueue queue = new JobQueue(5);
        queue.Enqueue(job1);
        queue.Enqueue(job2);
        queue.Enqueue(job3);
        queue.Enqueue(job4);
        queue.Enqueue(job5);

        Scheduler scheduler = new Scheduler(queue);
        MonitoringService monitoringService = new MonitoringService();
        StatisticsService statisticsService = new StatisticsService();
        LoggerService loggerService = new LoggerService();

        scheduler.JobStateChanged += monitoringService.Handle;
        scheduler.JobStateChanged += statisticsService.Handle;
        scheduler.JobStateChanged += loggerService.Handle;
        
        scheduler.ExecuteAll();
        statisticsService.Print();
    }
    
    
}
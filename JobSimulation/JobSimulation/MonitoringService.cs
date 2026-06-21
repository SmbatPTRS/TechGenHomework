namespace JobSimulation;

public class MonitoringService
{
    public void Handle(object? sender, JobEventArgs e)
    {
        Console.WriteLine($"[Monitoring] {e.EventName} - Job {e.Job.Id} ({e.Job.Name}) - Status: {e.Job.Status}");
    }
}
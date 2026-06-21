namespace JobSimulation;

public class LoggerService
{
    public DateTime DateTime;

    public void Handle(object? sender, JobEventArgs e)
    {
        if (e.Error != null)
        {
            Console.WriteLine($"[DateTime] {DateTime.Now:HH:mm:ss} [Monitoring] {e.EventName} - Job {e.Job.Id} ({e.Job.Name}) - Status: {e.Job.Status},error message {e.Error.Message}");
        }
        Console.WriteLine($"[DateTime] {DateTime.Now:HH:mm:ss} [Monitoring] {e.EventName} - Job {e.Job.Id} ({e.Job.Name}) - Status: {e.Job.Status}");

    }
}
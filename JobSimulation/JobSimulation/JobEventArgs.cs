namespace JobSimulation;

public class JobEventArgs: EventArgs
{
    public Job Job { get; }
    public string EventName { get; }
    public  Exception? Error { get; }

    public JobEventArgs(Job job, string eventName, Exception? error)
    {
        Job = job;
        EventName = eventName;
        Error = error;
    }
}
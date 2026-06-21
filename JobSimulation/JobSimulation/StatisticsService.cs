namespace JobSimulation;

public class StatisticsService
{
    private int _startedCount = 0;
    private int _completedCount = 0;
    private int _faliedCount = 0;


    public void Handle(object? sender, JobEventArgs e)
    {
        if (e.EventName == "JobCompleted")
        {
            _completedCount++;
        }

        else if (e.EventName == "JobStarted")
        {
            _startedCount++;
        }

        else 
        {
            _faliedCount++;
        }
    }

    public void Print()
    {
        Console.WriteLine($"Started Count: {_startedCount},  Completed Count: {_completedCount}, Falied: {_faliedCount}");
    }
}
namespace practice.File_Based_Event_Log_System.Subscribers;

public class ConsoleWriter
{
    public void LogConsole(object sender, LogEventArgs logEventArgs)
    {
        Console.WriteLine($"{logEventArgs.Message},{logEventArgs.level},{logEventArgs.TimeStamp}");
        Console.WriteLine();
    }
}
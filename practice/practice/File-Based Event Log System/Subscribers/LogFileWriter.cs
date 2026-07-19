namespace practice.File_Based_Event_Log_System.Subscribers;

public class LogFileWriter
{
    public readonly string _filePath;
    public LogFileWriter(string filePath)
    {
        _filePath = filePath;
    }

    public void WriteLog(object sender, LogEventArgs logEventArgs)
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        using (FileStream writer = new FileStream(_filePath, FileMode.Append, FileAccess.Write))
        {
            using (StreamWriter streamWriter = new StreamWriter(writer))
            {
                streamWriter.WriteLine($"{logEventArgs.Message}, {logEventArgs.level}, {logEventArgs.TimeStamp}");
            }
        }
        
    }
}
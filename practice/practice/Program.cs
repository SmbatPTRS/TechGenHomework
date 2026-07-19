using System.Collections;
using practice.File_Based_Event_Log_System.Subscribers;

namespace practice;
using practice.File_Based_Event_Log_System;
class Program
{
    public static void Main(string[] args)
    {
        // -------### File-Based Event Log System Implementation ###----------
        
        // string filepath = Path.Combine(Environment.CurrentDirectory, "log.txt");
        // Logger log = new Logger();
        // ConsoleWriter consoleWriter = new ConsoleWriter();
        // LogFileWriter logFileWriter = new LogFileWriter(filepath);
        // log.LogRaised += consoleWriter.LogConsole;
        // log.LogRaised += logFileWriter.WriteLog;
        //
        // log.Log("System started", LogLevel.Info);
        // log.Log("Low disk space warning", LogLevel.Warning);
        // log.Log("Database connection dropped!", LogLevel.Error);
        //
        // Console.WriteLine("\n--- Printing History from the Logger Storage ---");
        //
        // foreach (LogEventArgs entry in log)
        //
        // {
        //     Console.WriteLine($"History: [{entry.TimeStamp}] {entry.Message}");
        // }
    }
    
}




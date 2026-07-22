using System.Collections;
using practice.File_Based_Event_Log_System.Subscribers;

namespace practice;
using practice.File_Based_Event_Log_System;
using practice.Generic_Priority_Queue_with_Custom_Comparison;
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

        MyTask task1 = new MyTask("clean room", 2);
        MyTask task2 = new MyTask("cook breakfast", 3);
        MyTask task3 = new MyTask("study programming", 4);
        MyTask task4 = new MyTask("sleep", 6);
        
        MyComparer comparer = new MyComparer();

        Comparison<MyTask> comparison=(x,y)=> x.Name.Length.CompareTo(y.Name.Length);
        
        PriorityQueue<MyTask> priorityQueue = new PriorityQueue<MyTask>();
        
        PriorityQueue<MyTask> priorityQueue2 = new PriorityQueue<MyTask>(comparer);
        
        PriorityQueue<MyTask> priorityQueue3 = new PriorityQueue<MyTask>(comparison);
        
        priorityQueue.Enqueue(task1);
        priorityQueue.Enqueue(task2);
        priorityQueue.Enqueue(task3);
        
        priorityQueue.Enqueue(task4);

        foreach (MyTask task in priorityQueue)
        {
            Console.WriteLine(task.ToString());
        }

    }
    
    
    
}

    




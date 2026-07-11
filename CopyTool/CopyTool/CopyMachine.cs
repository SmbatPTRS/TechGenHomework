namespace CopyTool;
using System.Diagnostics;
public class CopyMachine
{
    public static void CopyFileWithProgress(string sourcePath,string destPath,int bufferSize)
    {
        
        using (FileStream source = new FileStream(sourcePath, FileMode.Open, FileAccess.Read)) 

        using (FileStream destination = new FileStream(destPath, FileMode.Create, FileAccess.Write))
        {
            long totalBytes = source.Length;
            
            long totalBytesCopied = 0;
            
            byte[] buffer = new byte[bufferSize];
            
            int BytesRead = 0;// for the source.Read
            
            
            // Start timer right now, so we can measure how much real time pass
            Stopwatch stopwatch = Stopwatch.StartNew();
            

            while ((BytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
            {
                destination.Write(buffer, 0, BytesRead);
                totalBytesCopied += BytesRead;
                double percentComplete = (double)totalBytesCopied / totalBytes * 100;
                
                double elapsedSeconds = stopwatch.Elapsed.TotalSeconds;
                
                double estimatedTotalSeconds = elapsedSeconds / (percentComplete / 100);

                double estimatedSecondsLeft = estimatedTotalSeconds - elapsedSeconds;


                Console.Write("\rProgress: " + percentComplete.ToString("0.0") +
                              "%ETA: " + estimatedSecondsLeft.ToString("0.0") + "s   ");
            }
        
            stopwatch.Stop();
        }
        
        
        

    }
}
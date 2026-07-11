namespace CopyTool;
using System.IO;
class Program
{
    static void Main(string[] args)
    {
        string sourcePath = null;
        string destPath = null;
        int bufferSize = 4096;
        
        for (int i = 0; i < args.Length; i++)
        {
            if (args.Length % 2 != 0)
            {
                Console.WriteLine("argument number is wrong");
                return;
            }
            if (args[i] == "--source" && i + 1 < args.Length)
            {
                sourcePath = args[i + 1];
            }
        
            if (args[i] == "--dest" && i + 1 < args.Length)
            {
                destPath = args[i + 1];
            }
        
            if (args[i] == "--buffer" && i + 1 < args.Length)
            {
                if (!int.TryParse(args[i + 1], out bufferSize))
                {
                    bufferSize = 4096;
                }
            }
        }
        
        if (string.IsNullOrWhiteSpace(sourcePath) || string.IsNullOrWhiteSpace(destPath))
        {
            Console.WriteLine("Both --source and --dest must be specified");
            return;
        }
        
        // I think we can do it as in our case there are no threads
        if (!File.Exists(sourcePath))
        {
            Console.WriteLine("source file does not exist: " + sourcePath);
            return;
        }
        
        
        string destDirectory = Path.GetDirectoryName(destPath);
        
        if (!string.IsNullOrEmpty(destDirectory))
        {
            Directory.CreateDirectory(destDirectory);
        }
        
        try
        {
            CopyMachine.CopyFileWithProgress(sourcePath, destPath, bufferSize);
            Console.WriteLine("copied file successfully");
        
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            Console.WriteLine(" 1 ");
        }

        // File.WriteAllText("sourcefile.txt", "Hello User, this is my test file.\nSecond line.\nThird line.");
        //
        // File.WriteAllText("destinationfile.txt","");

    }
}
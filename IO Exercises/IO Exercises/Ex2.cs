namespace IO_Exercises;

public class Ex2
{
    public static void Main2()
    {
        
        string folderPath = "inbox";

        Directory.CreateDirectory(folderPath);

        int fileCount = 0;

 
        foreach (string filePath in Directory.EnumerateFiles(folderPath))
        {
            string fileName = Path.GetFileName(filePath);

            FileInfo fileInfo = new FileInfo(filePath);

            Console.WriteLine($"Name : {fileName}");
            Console.WriteLine($"Size : {fileInfo.Length} bytes");
            Console.WriteLine();

            fileCount++;
        }

        Console.WriteLine($"Total files: {fileCount}");
    }
}
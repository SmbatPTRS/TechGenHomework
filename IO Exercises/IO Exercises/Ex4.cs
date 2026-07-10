namespace IO_Exercises;
using System.IO;
using System.Text;
public class Ex4
{
    public static void Main4()
    {
        string path = "logs.txt";

        using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
        {
            writer.WriteLine("INFO Application started");
            writer.WriteLine("ERROR Failed to connect to database");
            writer.WriteLine("INFO User logged in");
            writer.WriteLine("Ошибка: сервер недоступен");
            writer.WriteLine("ERROR Invalid password");
        }

        int errorCount = 0;

        using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
        {
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                Console.WriteLine(line);

                if (line.Contains("ERROR"))
                {
                    errorCount++;
                }
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Total ERROR lines: {errorCount}");
    }
}
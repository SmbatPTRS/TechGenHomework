namespace IO_Exercises;
using System.IO;
public class Ex3
{
    public static void Main3()
    {
        string path = "download.bin";

        byte[] blockA = { 1, 2, 3, 4 };
        byte[] blockB = { 9, 8, 7, 6 };

        using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
        {
            fs.Write(blockA);

            fs.Seek(1024, SeekOrigin.Begin);

            fs.Write(blockB);
        }

        using (FileStream fs = new FileStream(path, FileMode.Open))
        {
            byte[] readA = new byte[4];
            byte[] readB = new byte[4];

            fs.Read(readA);

            fs.Seek(1024, SeekOrigin.Begin);

            fs.Read(readB);

            Console.WriteLine("Blok A:");
            Console.WriteLine(string.Join(",", readA));

            Console.WriteLine();

            Console.WriteLine("Blok B:");
            Console.WriteLine(string.Join(",", readB));
        }
    }
}
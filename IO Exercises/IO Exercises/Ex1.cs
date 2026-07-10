namespace IO_Exercises;

public class Ex1
{
    public static void Main1()
    {
        string input = """
                       this is the first line of the report
                       this is the second line of the report
                       this is the third line of the report
                       report is finished here.
                       """;
        
        Directory.CreateDirectory("reports");   
        string filepath = Path.Combine("reports",  "report.txt");

        try
        {
            File.WriteAllText(filepath, input);
            Console.WriteLine("Report written successfully");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        // reading from a file
        string input2=null;
        try
        {
            Console.WriteLine("");
            Console.WriteLine("Handing out the report");
            Console.WriteLine();
            input2 = File.ReadAllText(filepath);
            Console.WriteLine(input2);
        }
        catch (IOException e )
        {
            Console.WriteLine(e.Message);
        }

        if (input2 == input)
        {
            Console.WriteLine($"the input was :-- {input}--:\n the file contains :-- {input2}--:\n");
            Console.WriteLine();
            Console.WriteLine("Report saved successfully");
        }
    }
    
}
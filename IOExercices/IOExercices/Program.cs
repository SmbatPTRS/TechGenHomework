using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Xml.Serialization;

namespace IOExercises
{
    class Program
    {
        static void Main(string[] args)
        {
            Student s1 = new Student("Smbat", 22, "null" , DateTime.Today);
            string folderPath = Path.Combine("Data", "X");
            Directory.CreateDirectory(folderPath);
            string filePath = Path.Combine(folderPath, "students.json");
     
            string xmlpath=Path.ChangeExtension(filePath, "xml"); 
            var serializer = new XmlSerializer(typeof(Student));
            using (FileStream fs = new FileStream(xmlpath, FileMode.OpenOrCreate))
            {
                serializer.Serialize(fs, s1);
            }
        
        //     Console.WriteLine(File.ReadAllText(filePath));
        //     
        }
        

        
    }

    public class Student
    {
        public string firstName { get; set; }
        [XmlAttribute ("age")]    
        public int Age{ get; set; }
        public string? middleName{ get; set; }
        public DateTime enrollmentDate{ get; set; }
        
        public Student(){}
        public  Student(string firstName, int age, string? middleName, DateTime enrollmentDate)
        {
            this.firstName = firstName;
            this.Age = age;
            this.middleName = middleName??null;
            this.enrollmentDate = enrollmentDate;
        }
    }
}
using System.Reflection.Metadata;
using Microsoft.Data.Sqlite;

namespace Mini_social_app_backend;

class Program
{
    static void Main(string[] args)
    {
       Database database = new Database("sandbox","Users.db");
       database.GetConnection();
       
       database.Initialize();



    }
    
    
}
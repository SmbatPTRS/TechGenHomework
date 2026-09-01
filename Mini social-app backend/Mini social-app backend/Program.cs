using System.Reflection.Metadata;
using Microsoft.Data.Sqlite;

namespace Mini_social_app_backend;

class Program
{
    static void Main(string[] args)
    {
       Database database = new Database("sandbox","Users.db"); 
       
       
       //database.Initialize();
       //database.EnsureFriendshipTableExists();
       
       var userInventory = new UserInventory();

       //userInventory.RegisterUser(database, "smbat", "MySecurePass123", "Smbat", "Something");
       //userInventory.RegisterUser(database, "anna", "AnnaPass456", "Anna", "Something");

       //bool loginResult = userInventory.Login(database, "smbat", "MySecurePass123");

       
       userInventory.AddFriend(database, 1, 2);

       



    }
    
    
}
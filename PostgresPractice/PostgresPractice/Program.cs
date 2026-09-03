using System.Data;
using System.Security.Cryptography;
using System.Text;
using Npgsql;

namespace PostgresPractice;

class Program
{

    

    static void Main(string[] args)
    {
        //Database.EnsureSchema();
        
        UserRepository userRepo = new UserRepository();
        
        userRepo.Register("alice", "alicepass123", "Alice", "Smith", "1998-04-12");
        userRepo.Register("bob", "bobpass456", "Bob", "Johnson", "1995-11-03");
        userRepo.Register("charlie", "charliepass789", "Charlie", "Brown", "2000-07-22");
        userRepo.Register("dave", "davepass000", "Dave", "Wilson", "1999-01-15");
        
        
        List<string> others = userRepo.GetAllUsersExceptMe("alice");

        Console.WriteLine("Users other than alice:");
        foreach (string username in others)
        {
            Console.WriteLine($" - {username}");
        }
        
        
        FriendReopsitory friendRepo = new FriendReopsitory();

        bool result1 = friendRepo.AddFriend(1, 2); // alice <-> bob
        Console.WriteLine($"Add alice-bob: {result1}");

        bool result2 = friendRepo.AddFriend(1, 3); // alice <-> charlie
        Console.WriteLine($"Add alice-charlie: {result2}");

    }






  
}
using System.Security.Cryptography;
using Microsoft.Data.Sqlite;

namespace Mini_social_app_backend;

public class UserInventory
{
    public void RegisterUser(Database database,string username, string password, string firstname, string lastname)
    {
        byte[] saltBytes = RandomNumberGenerator.GetBytes(16);
        
        byte[] hashbytes = Rfc2898DeriveBytes.Pbkdf2(
            password,
            saltBytes,
            100_000,
            HashAlgorithmName.SHA256,
            32
        );
        
        
        string StringHash = Convert.ToBase64String(hashbytes);
        string StringSalt = Convert.ToBase64String(saltBytes);
        
        string now = DateTime.UtcNow.ToString("o");

        using (var conn = database.GetConnection())
        {
            var command = conn.CreateCommand();
            command.CommandText = """
                                  INSERT INTO Users(Username, PasswordHash, FirstName, LastName,CreatedAt,ModifiedAt,Salt)
                                  VALUES (@username, @passwordHash, @firstName, @lastName, @createdAt, @modifiedAt, @salt);
                                  """;
            
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@passwordHash", StringHash);
            command.Parameters.AddWithValue("@firstName", firstname);
            command.Parameters.AddWithValue("@lastName", lastname);
            command.Parameters.AddWithValue("@createdAt", now);
            command.Parameters.AddWithValue("@modifiedAt", now);
            command.Parameters.AddWithValue("@salt", StringSalt);
            
            command.ExecuteNonQuery();
        }

        Console.WriteLine($"Successfully registered user {username}");
        
        
    }

    
    
    
    public bool Login(Database database, string username, string password)
    {
        string? storedSalt = null;
        string? storedHash = null;
        using (var conn = database.GetConnection())
        {
            var command = conn.CreateCommand();
            command.CommandText = @"
                SELECT Salt, PasswordHash FROM Users WHERE Username = @username;
            ";
            command.Parameters.AddWithValue("@username", username);
            
                  // returns SqliteDataReader object
                  using (var reader = command.ExecuteReader())
                  {
                      if (!reader.Read())
                      {
                          // No user with that username exists.
                          Console.WriteLine("Login failed: user not found.");
                          return false;
                      }

                      storedSalt = reader.GetString(0); 
                      storedHash = reader.GetString(1); 
                  }
        }
        
        byte[] saltBytes = Convert.FromBase64String(storedSalt);
        byte[] attemptHashBytes = Rfc2898DeriveBytes.Pbkdf2(
            password: password,
            salt: saltBytes,
            iterations: 100_000,
            hashAlgorithm: HashAlgorithmName.SHA256,
            outputLength: 32
        );
        string attemptHashString = Convert.ToBase64String(attemptHashBytes);
        
        bool isMatch = (attemptHashString == storedHash);

        if (isMatch)
        {
            Console.WriteLine("login successful");
        }
        else
        {
            Console.WriteLine("login failed");
        }
        return isMatch;
    }



    public void AddFriend(Database database,int userId1, int userId2)
    {
        if (userId1 == userId2)
        {
            Console.WriteLine("A user cannot be friends with themselves.");
            return;
        }
        
        string now = DateTime.UtcNow.ToString("o");

        using (var conn = database.GetConnection())
        {
            var command = conn.CreateCommand();
            command.CommandText = """
                                  INSERT INTO Friendship (UserId1, UserId2, CreatedAt)
                                  VALUES (@userId1, @userId2, @createdAt);
                                  """;

            command.Parameters.AddWithValue("@userId1", userId1);
            command.Parameters.AddWithValue("@userId2", userId2);
            command.Parameters.AddWithValue("@createdAt", now);

            command.ExecuteNonQuery();
        }

        Console.WriteLine($"User {userId1} and User {userId2} are now friends.");
    }
    
}


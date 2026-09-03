using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace PostgresPractice;

public class UserRepository
{
      public bool Login(string username, string password)
    {
        string? storedHash;
        using IDbConnection conn = Database.Open();
        
        using IDbCommand cmd = conn.CreateCommand();

        cmd.CommandText = @"

        SELECT PasswordHash FROM Users WHERE UserName = @username";
        
        IDbDataParameter param = cmd.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        
        cmd.Parameters.Add(param);
        
        object? result = cmd.ExecuteScalar();

        if (result == null)
        {
            Console.WriteLine("Username not found");
            return false;
        }
        
        storedHash = (string)result;

        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes("smbat"));
        byte[] attemptHashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        
        string attemptHashString = Convert.ToBase64String(attemptHashBytes);
        
        bool isMatch = (attemptHashString == storedHash);
        
        if (isMatch)
        {
            Console.WriteLine($"login successful, hello {username}");
        }
        else
        {
            Console.WriteLine("login failed");
        }
        return isMatch;
    }


    public void Register(string username, string password, string firstName, string lastName, string dateOfBirth)
    {
        using IDbConnection conn = Database.Open();
        using IDbCommand cmd = conn.CreateCommand();

        
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes("smbat"));
        
        byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        string passwordHash = Convert.ToBase64String(hashBytes);
        cmd.CommandText = @"
            INSERT INTO Users(Username, PasswordHash, FirstName, LastName, DateOfBirth)
            VALUES (@username, @passwordHash, @firstName, @lastName, @dateOfBirth);
       
        ";
        
        IDbDataParameter param = cmd.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        cmd.Parameters.Add(param);
        
        param = cmd.CreateParameter();
        param.ParameterName = "@passwordHash";
        param.Value = passwordHash;
        cmd.Parameters.Add(param);
        
        param = cmd.CreateParameter();
        param.ParameterName = "@firstName";
        param.Value = firstName;
        cmd.Parameters.Add(param);
        
        param = cmd.CreateParameter();
        param.ParameterName = "@lastName";
        param.Value = lastName;
        cmd.Parameters.Add(param);
        
        param = cmd.CreateParameter();
        param.ParameterName = "@dateOfBirth";
        param.Value = dateOfBirth;
        cmd.Parameters.Add(param);
        
        cmd.ExecuteNonQuery();
        
        Console.WriteLine($"Successfully registered user {username}");


    }

    public List<string> GetAllUsersExceptMe(string username)
    {
        List<string> otherUsers = new List<string>();
        
        using IDbConnection conn = Database.Open();
        using IDbCommand command = conn.CreateCommand();

        command.CommandText = @"
        SELECT USERNAME FROM Users WHERE UserName <> @username;
        ";

        IDataParameter parameter = command.CreateParameter();
        parameter.ParameterName =  "@username";
        parameter.Value= username;
        
        command.Parameters.Add(parameter);
        
        
        using IDataReader reader = command.ExecuteReader();
        
        while (reader.Read())
        {
            string name = reader.GetString(reader.GetOrdinal("UserName"));
            otherUsers.Add(name);
        }

        return otherUsers;
    }
}
namespace Mini_social_app_backend;
using Microsoft.Data.Sqlite;


public class Database
{ 
    private readonly string _connectionString;
    
    
    public Database(string folderName,string filename)
    {
        Directory.CreateDirectory(folderName);
        
        
        string fullPath = Path.Combine(folderName, filename);
        
        _connectionString = new SqliteConnectionStringBuilder
        {
            DataSource = fullPath,
            ForeignKeys = true,
        }.ConnectionString;
    }

    public SqliteConnection GetConnection()
    {
        var connection = new SqliteConnection(_connectionString);
        connection.Open();
        return connection;
    }

    public void Initialize()
    {
        using var conn = GetConnection();
        using var cmd = conn.CreateCommand();

        cmd.CommandText = @"
            CREATE TABLE IF NOT EXISTS Users (
                UserId INTEGER PRIMARY KEY AUTOINCREMENT,
                Username TEXT NOT NULL UNIQUE,
                PasswordHash TEXT NOT NULL,
                FirstName TEXT NOT NULL,
                LastName TEXT NOT NULL,
                CreatedAt TEXT NOT NULL,
                ModifiedAt TEXT NOT NULL,
                Salt TEXT NOT NULL      
            );";
        
        cmd.ExecuteNonQuery();
    }
    
  
    
    

}
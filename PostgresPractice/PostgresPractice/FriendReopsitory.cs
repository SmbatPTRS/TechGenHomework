using System.Data;

namespace PostgresPractice;

public class FriendReopsitory
{
    public bool AddFriend(int userId, int friendUserId)
{
    if (userId == friendUserId)
    {
        Console.WriteLine("You cannot add yourself as a friend.");
        return false;
    }

    using IDbConnection conn = Database.Open();


    using IDbTransaction transaction = conn.BeginTransaction();

    try
    {

        using (IDbCommand checkCmd = conn.CreateCommand())
        {
            checkCmd.Transaction = transaction; // MUST associate this command with the transaction
            checkCmd.CommandText = "SELECT COUNT(*) FROM Users WHERE UserId = @friendUserId";

            IDbDataParameter friendParam = checkCmd.CreateParameter();
            friendParam.ParameterName = "@friendUserId";
            friendParam.Value = friendUserId;
            checkCmd.Parameters.Add(friendParam);

            long friendExists = (long)checkCmd.ExecuteScalar();
            if (friendExists == 0)
            {
                Console.WriteLine("The user you're trying to add does not exist.");
                transaction.Rollback();
                return false;
            }
        }

        // Check 3: are they already friends?
        using (IDbCommand checkCmd = conn.CreateCommand())
        {
            checkCmd.Transaction = transaction;
            checkCmd.CommandText = @"
                SELECT COUNT(*) FROM Friends 
                WHERE UserId = @userId AND FriendUserId = @friendUserId";

            IDbDataParameter userParam = checkCmd.CreateParameter();
            userParam.ParameterName = "@userId";
            userParam.Value = userId;
            checkCmd.Parameters.Add(userParam);

            IDbDataParameter friendParam = checkCmd.CreateParameter();
            friendParam.ParameterName = "@friendUserId";
            friendParam.Value = friendUserId;
            checkCmd.Parameters.Add(friendParam);

            long alreadyFriends = (long)checkCmd.ExecuteScalar();
            if (alreadyFriends > 0)
            {
                Console.WriteLine("You are already friends with this user.");
                transaction.Rollback();
                return false;
            }
        }

        // Insert direction 1: userId -> friendUserId
        using (IDbCommand insertCmd1 = conn.CreateCommand())
        {
            insertCmd1.Transaction = transaction;
            insertCmd1.CommandText = @"
                INSERT INTO Friends (UserId, FriendUserId) 
                VALUES (@userId, @friendUserId)";

            IDbDataParameter userParam = insertCmd1.CreateParameter();
            userParam.ParameterName = "@userId";
            userParam.Value = userId;
            insertCmd1.Parameters.Add(userParam);

            IDbDataParameter friendParam = insertCmd1.CreateParameter();
            friendParam.ParameterName = "@friendUserId";
            friendParam.Value = friendUserId;
            insertCmd1.Parameters.Add(friendParam);

            insertCmd1.ExecuteNonQuery();
        }

        // Insert direction 2: friendUserId -> userId (this is what makes it symmetric)
        using (IDbCommand insertCmd2 = conn.CreateCommand())
        {
            insertCmd2.Transaction = transaction;
            insertCmd2.CommandText = @"
                INSERT INTO Friends (UserId, FriendUserId) 
                VALUES (@friendUserId, @userId)";

            IDbDataParameter userParam = insertCmd2.CreateParameter();
            userParam.ParameterName = "@friendUserId";
            userParam.Value = friendUserId;
            insertCmd2.Parameters.Add(userParam);

            IDbDataParameter friendParam = insertCmd2.CreateParameter();
            friendParam.ParameterName = "@userId";
            friendParam.Value = userId;
            insertCmd2.Parameters.Add(friendParam);

            insertCmd2.ExecuteNonQuery();
        }

        transaction.Commit();
        Console.WriteLine("Friend added successfully.");
        return true;
    }
    catch (Exception ex)
    {
        transaction.Rollback();
        Console.WriteLine($"Failed to add friend: {ex.Message}");
        return false;
    }
}
}
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Services;
using Microsoft.AspNetCore.Identity;
public class AuthService : IAuthService
{
    private readonly BookContext _db;

    private readonly PasswordHasher<User> _hasher = new();

    public AuthService(BookContext db)
    {
        _db = db;
    }
    
    public async Task<User?> RegisterAsync(string username, string password)
    {
        // is the username taken?
        var alreadyExists = await _db.Users.AnyAsync(u => u.Username == username);
        if (alreadyExists)
        {
            return null; // the controller turns null into 409
        }

        // build the new user WITHOUT a password yet.
        var user = new User { Username = username };

        //blend the password (salt + slow hashing happen inside).
        // The plain password exists only in this method and is never saved.
        user.PasswordHash = _hasher.HashPassword(user, password);

        // save. Add = "remember this new row",
        // SaveChangesAsync = "send the INSERT to Postgres now".
        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        // After saving, Postgres has generated user.Id and EF filled it in.
        return user;
    }


    public async Task<User?> ValidateCredentialsAsync(string username, string password)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user is null)
        {
            return null; // no such username
        }
        
        
        var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, password);

        if (result == PasswordVerificationResult.Failed)
        {
            return null; // wrong password
        }

        return user; // credentials are correct
    }

}
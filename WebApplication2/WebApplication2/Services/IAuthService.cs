using WebApplication2.Models;

namespace WebApplication2.Services;

public interface IAuthService
{
    // Returns the new user, or null if the username is already taken.
    Task<User?> RegisterAsync(string username, string password);
}
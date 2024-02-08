using BlazorApp.Server.Models;

namespace BlazorApp.Server.Auth;

public interface IUserManager
{
    public User? GetUser(string name);
    public IEnumerable<User> GetUsers();
    public IEnumerable<string> GetRoles(User user);
    public bool HasAdminRole(IEnumerable<string> userRoles);
    public bool HasUserRole(IEnumerable<string> userRoles);
}

public class UserManager : IUserManager
{
    private const string Admin = "admin";
    private const string User = "user";
    private const string UsersConfigSection = "Users";
    private readonly IConfiguration _configuration;

    public UserManager(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public User? GetUser(string name)
    {
        return GetUsers().FirstOrDefault(n => n.Name == name);
    }

    public IEnumerable<User> GetUsers()
    {
        return _configuration.GetSection(UsersConfigSection).Get<List<User>>();
    }

    public IEnumerable<string> GetRoles(User user)
    {
        return user.Roles.FindAll(n => n == Admin || n == User);
    }

    public bool HasAdminRole(IEnumerable<string> userRoles)
    {
        return userRoles.Any(x => x == Admin);
    }

    public bool HasUserRole(IEnumerable<string> userRoles)
    {
        return userRoles.Any(x => x == User);
    }
}
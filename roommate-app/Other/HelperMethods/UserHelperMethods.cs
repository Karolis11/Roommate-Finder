using roommate_app.Models;

namespace roommate_app.HelperMethods;

public class UserHelperMethods
{

    List<User> _users;

    private readonly ApplicationDbContext _context;

    public UserHelperMethods(ApplicationDbContext context)
    {
        _context = context;
        _users = _context.Users.ToList();
    }


    public User GetById(int id)
    {
        return _users.FirstOrDefault(x => x.Id == id);
    }

    private List<User> LoadUsers()
    {
        using StreamReader r = new StreamReader("./Data/users.json");
        string json = r.ReadToEnd();
        return JsonSerializer.Deserialize<List<User>>(json);
    }

}
using APBD_Cw1_s29756.Exceptions;
using APBD_Cw1_s29756.Models;

namespace APBD_Cw1_s29756.Services;

public class UserService : IUserService
{
    private readonly List<User> _users = [];

    public void Add(User user)
    {
        _users.Add(user);
    }

    public User GetById(Guid id)
    {
        return _users.FirstOrDefault(u => u.Id == id)
               ?? throw new UserNotFoundException(id);
    }

    public List<User> GetAll()
    {
        return _users.ToList();
    }
}

using APBD_Cw1_s29756.Models;

namespace APBD_Cw1_s29756.Services;

public interface IUserService
{
    void Add(User user);
    User GetById(Guid id);
    List<User> GetAll();
}

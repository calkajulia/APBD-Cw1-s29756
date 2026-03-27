namespace APBD_Cw1_s29756.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(Guid id)
        : base($"User with id {id} not found.") { }
}

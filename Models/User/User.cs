namespace APBD_Cw1_s29756.Models;

public abstract class User
{
    public Guid Id { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public abstract int RentalsLimit { get; }

    protected User(string firstName, string lastName)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
    }
}

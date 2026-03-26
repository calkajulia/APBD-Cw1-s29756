namespace APBD_Cw1_s29756.Models;

public class Employee : User
{
    public override int RentalsLimit => 5;

    public Employee(string firstName, string lastName) : base(firstName, lastName) { }
}

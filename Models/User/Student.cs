namespace APBD_Cw1_s29756.Models;

public class Student : User
{
    public override int RentalsLimit => 2;

    public Student(string firstName, string lastName) : base(firstName, lastName) { }
}

namespace APBD_Cw1_s29756.Models;

public class Rental
{
    public Guid Id { get; }
    public User User { get; }
    public Equipment Equipment { get; }
    public DateTime RentalDate { get; }
    public DateTime DueDate { get; }
    public DateTime? ReturnDate { get; private set; }
    public decimal Penalty { get; private set; }

    public bool IsActive => ReturnDate == null;
    public bool IsOverdue => IsActive && DateTime.Now > DueDate;

    public Rental(User user, Equipment equipment, DateTime dueDate)
    {
        Id = Guid.NewGuid();
        User = user;
        Equipment = equipment;
        RentalDate = DateTime.Now;
        DueDate = dueDate;
        Penalty = 0;
    }

    public void CompleteReturn(DateTime returnDate, decimal penalty)
    {
        ReturnDate = returnDate;
        Penalty = penalty;
    }
}

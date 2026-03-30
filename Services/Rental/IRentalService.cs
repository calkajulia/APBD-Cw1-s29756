using APBD_Cw1_s29756.Models;

namespace APBD_Cw1_s29756.Services;

public interface IRentalService
{
    Rental Rent(User user, Equipment equipment);
    // used only in demo seeder
    Rental RentWithDueDate(User user, Equipment equipment, DateTime dueDate);
    void Return(Guid rentalId);
    Rental GetById(Guid id);
    List<Rental> GetAll();
    List<Rental> GetOverdueRentals();
    List<Rental> GetUsersActiveRentals(Guid userId);
}

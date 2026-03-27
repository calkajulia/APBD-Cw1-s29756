using APBD_Cw1_s29756.Config;
using APBD_Cw1_s29756.Enums;
using APBD_Cw1_s29756.Exceptions;
using APBD_Cw1_s29756.Models;

namespace APBD_Cw1_s29756.Services;

public class RentalService : IRentalService
{
    private readonly List<Rental> _rentals = [];

    public Rental Rent(User user, Equipment equipment)
    {
        if (equipment.Status != EquipmentStatus.Available)
        {
            throw new EquipmentUnavailableException(equipment.Name);
        }

        var usersActiveRentals = GetUsersActiveRentals(user.Id);
        if (usersActiveRentals.Count >= user.RentalsLimit)
        {
            throw new RentalLimitExceededException($"{user.FirstName} {user.LastName}", user.RentalsLimit);
        }

        var dueDate = DateTime.Now.AddDays(RentalPolicy.DefaultRentalDays);
        var rental = new Rental(user, equipment, dueDate);
        equipment.Status = EquipmentStatus.Rented;
        _rentals.Add(rental);
        return rental;
    }

    public void Return(Guid rentalId)
    {
        var rental = GetById(rentalId);
        var returnDate = DateTime.Now;
        var penalty = CalculatePenalty(rental, returnDate);
        rental.CompleteReturn(returnDate, penalty);
        rental.Equipment.Status = EquipmentStatus.Available;
    }

    public Rental GetById(Guid id)
    {
        return _rentals.FirstOrDefault(r => r.Id == id)
               ?? throw new RentalNotFoundException(id);
    }

    public List<Rental> GetAll()
    {
        return _rentals.ToList();
    }
    
    public List<Rental> GetOverdueRentals()
    {
        return _rentals.Where(r => r.IsOverdue).ToList();
    }
    
    public List<Rental> GetUsersActiveRentals(Guid userId)
    {
        return _rentals.Where(r => r.User.Id == userId && r.IsActive).ToList();
    }

    private static decimal CalculatePenalty(Rental rental, DateTime returnDate)
    {
        if (returnDate <= rental.DueDate)
        {
            return 0;
        }

        var daysLate = (int)(returnDate - rental.DueDate).TotalDays;
        return daysLate * RentalPolicy.DailyPenaltyRate;
    }
}

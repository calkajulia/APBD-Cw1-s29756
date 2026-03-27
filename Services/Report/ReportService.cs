using APBD_Cw1_s29756.Enums;

namespace APBD_Cw1_s29756.Services;

public class ReportService
{
    private readonly IEquipmentService _equipmentService;
    private readonly IUserService _userService;
    private readonly IRentalService _rentalService;

    public ReportService(IEquipmentService equipmentService, IUserService userService, IRentalService rentalService)
    {
        _equipmentService = equipmentService;
        _userService = userService;
        _rentalService = rentalService;
    }

    public void PrintReport()
    {
        var allEquipment = _equipmentService.GetAll();
        var allUsers = _userService.GetAll();
        var allRentals = _rentalService.GetAll();
        var overdueRentals = _rentalService.GetOverdueRentals();

        Console.WriteLine("========== RENTAL SYSTEM REPORT ==========");
        Console.WriteLine($"Equipment total: {allEquipment.Count}");
        Console.WriteLine($"  Available: {allEquipment.Count(e => e.Status == EquipmentStatus.Available)}");
        Console.WriteLine($"  Rented: {allEquipment.Count(e => e.Status == EquipmentStatus.Rented)}");
        Console.WriteLine($"  Unavailable: {allEquipment.Count(e => e.Status == EquipmentStatus.Unavailable)}");
        Console.WriteLine($"Users total: {allUsers.Count}");
        Console.WriteLine($"Active rentals: {allRentals.Count(r => r.IsActive)}");
        Console.WriteLine($"Overdue rentals: {overdueRentals.Count}");
        Console.WriteLine($"Total penalties: {allRentals.Sum(r => r.Penalty):F2} PLN");
        Console.WriteLine("==========================================");
    }
}

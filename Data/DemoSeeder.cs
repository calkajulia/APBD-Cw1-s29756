using APBD_Cw1_s29756.Models;
using APBD_Cw1_s29756.Services;

namespace APBD_Cw1_s29756.Data;

public class DemoSeeder
{
    private readonly IEquipmentService _equipmentService;
    private readonly IUserService _userService;
    private readonly IRentalService _rentalService;

    public DemoSeeder(IEquipmentService equipmentService, IUserService userService, IRentalService rentalService)
    {
        _equipmentService = equipmentService;
        _userService = userService;
        _rentalService = rentalService;
    }

    public void Seed()
    {
        var student = new Student("Anna", "Kowalska");
        var employee = new Employee("Jan", "Nowak");
        _userService.Add(student);
        _userService.Add(employee);
        
        var laptop = new Laptop("Dell XPS 15", "Dell", "Windows 11", "16GB");
        var projector = new Projector("EB-X51", "Epson", "1024x768", true);
        var camera = new Camera("EOS R50", "Canon", "MP4", false);
        var laptopUnavailable = new Laptop("MacBook Pro", "Apple", "macOS", "32GB");

        _equipmentService.Add(laptop);
        _equipmentService.Add(projector);
        _equipmentService.Add(camera);
        _equipmentService.Add(laptopUnavailable);
        
        _rentalService.Rent(student, laptop);
        _rentalService.RentWithDueDate(employee, projector, DateTime.Now.AddDays(-5));
        _equipmentService.SetUnavailable(laptopUnavailable.Id);
    }
}

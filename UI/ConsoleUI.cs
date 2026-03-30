using APBD_Cw1_s29756.Models;
using APBD_Cw1_s29756.Services;

namespace APBD_Cw1_s29756.UI;

public class ConsoleUI
{
    private readonly IEquipmentService _equipmentService;
    private readonly IUserService _userService;
    private readonly IRentalService _rentalService;
    private readonly ReportService _reportService;

    public ConsoleUI(IEquipmentService equipmentService, IUserService userService,
        IRentalService rentalService, ReportService reportService)
    {
        _equipmentService = equipmentService;
        _userService = userService;
        _rentalService = rentalService;
        _reportService = reportService;
    }

    public void Run()
    {
        while (true)
        {
            PrintMenu();
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddUser(); break;
                case "2": AddEquipment(); break;
                case "3": ShowAllEquipment(); break;
                case "4": ShowAvailableEquipment(); break;
                case "5": RentEquipment(); break;
                case "6": ReturnEquipment(); break;
                case "7": SetEquipmentUnavailable(); break;
                case "8": SetEquipmentAvailable(); break;
                case "9": ShowActiveRentalsForUser(); break;
                case "10": ShowOverdueRentals(); break;
                case "11": _reportService.PrintReport(); break;
                case "0": return;
                default: Console.WriteLine("Invalid option."); break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    private static void PrintMenu()
    {
        Console.WriteLine("===== EQUIPMENT RENTAL SYSTEM =====");
        Console.WriteLine("1.  Add user");
        Console.WriteLine("2.  Add equipment");
        Console.WriteLine("3.  Show all equipment");
        Console.WriteLine("4.  Show available equipment");
        Console.WriteLine("5.  Rent equipment");
        Console.WriteLine("6.  Return equipment");
        Console.WriteLine("7.  Mark equipment as unavailable");
        Console.WriteLine("8.  Mark equipment as available");
        Console.WriteLine("9.  Show active rentals for user");
        Console.WriteLine("10. Show overdue rentals");
        Console.WriteLine("11. Generate report");
        Console.WriteLine("0.  Exit");
        Console.Write("Choose: ");
    }

    private void AddUser()
    {
        Console.WriteLine("User type: 1=Student, 2=Employee");
        var type = Console.ReadLine();
        Console.Write("First name: ");
        var firstName = Console.ReadLine()!;
        Console.Write("Last name: ");
        var lastName = Console.ReadLine()!;

        try
        {
            User user = type switch
            {
                "1" => new Student(firstName, lastName),
                "2" => new Employee(firstName, lastName),
                _ => throw new InvalidOperationException("Invalid user type.")
            };

            _userService.Add(user);
            Console.WriteLine($"User {firstName} {lastName} added. ID: {user.Id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private void AddEquipment()
    {
        Console.WriteLine("Equipment type: 1=Laptop, 2=Projector, 3=Camera");
        var type = Console.ReadLine();
        Console.Write("Name: ");
        var name = Console.ReadLine()!;
        Console.Write("Brand: ");
        var brand = Console.ReadLine()!;

        try
        {
            Equipment equipment = type switch
            {
                "1" => CreateLaptop(name, brand),
                "2" => CreateProjector(name, brand),
                "3" => CreateCamera(name, brand),
                _ => throw new InvalidOperationException("Invalid equipment type.")
            };

            _equipmentService.Add(equipment);
            Console.WriteLine($"Equipment '{name}' added. ID: {equipment.Id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static Laptop CreateLaptop(string name, string brand)
    {
        Console.Write("Operating system: ");
        var os = Console.ReadLine()!;
        Console.Write("RAM (GB): ");
        var ram = Console.ReadLine()!;
        return new Laptop(name, brand, os, ram);
    }

    private static Projector CreateProjector(string name, string brand)
    {
        Console.Write("Resolution: ");
        var resolution = Console.ReadLine()!;
        Console.Write("Is portable? (y/n): ");
        var portable = Console.ReadLine()?.ToLower() == "y";
        return new Projector(name, brand, resolution, portable);
    }

    private static Camera CreateCamera(string name, string brand)
    {
        Console.Write("Recording format: ");
        var format = Console.ReadLine()!;
        Console.Write("Is waterproof? (y/n): ");
        var waterproof = Console.ReadLine()?.ToLower() == "y";
        return new Camera(name, brand, format, waterproof);
    }

    private void ShowAllEquipment()
    {
        var equipment = _equipmentService.GetAll();
        if (equipment.Count == 0) { Console.WriteLine("No equipment found."); return; }

        foreach (var e in equipment)
        {
            Console.WriteLine($"[{e.Status}] {e.Name} ({e.Brand}) - ID: {e.Id}");
        }
    }

    private void ShowAvailableEquipment()
    {
        var equipment = _equipmentService.GetAvailable();
        if (equipment.Count == 0) { Console.WriteLine("No available equipment."); return; }

        foreach (var e in equipment)
        {
            Console.WriteLine($"{e.Name} ({e.Brand}) - ID: {e.Id}");
        }
    }

    private void RentEquipment()
    {
        Console.WriteLine("-- Users --");
        foreach (var u in _userService.GetAll())
        {
            Console.WriteLine($"  {u.FirstName} {u.LastName} - ID: {u.Id}");
        }

        Console.WriteLine("-- Available Equipment --");
        foreach (var e in _equipmentService.GetAvailable())
        {
            Console.WriteLine($"  {e.Name} ({e.Brand}) - ID: {e.Id}");
        }

        Console.Write("User ID: ");
        if (!Guid.TryParse(Console.ReadLine(), out var userId)) { Console.WriteLine("Invalid ID."); return; }
        Console.Write("Equipment ID: ");
        if (!Guid.TryParse(Console.ReadLine(), out var equipmentId)) { Console.WriteLine("Invalid ID."); return; }

        try
        {
            var user = _userService.GetById(userId);
            var equipment = _equipmentService.GetById(equipmentId);
            var rental = _rentalService.Rent(user, equipment);
            Console.WriteLine($"Rented successfully. Due date: {rental.DueDate:yyyy-MM-dd}. Rental ID: {rental.Id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private void ReturnEquipment()
    {
        Console.Write("Rental ID: ");
        if (!Guid.TryParse(Console.ReadLine(), out var rentalId)) { Console.WriteLine("Invalid ID."); return; }

        try
        {
            _rentalService.Return(rentalId);
            var rental = _rentalService.GetById(rentalId);
            Console.WriteLine(rental.Penalty > 0
                ? $"Returned with penalty: {rental.Penalty:F2} PLN"
                : "Returned on time. No penalty.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private void SetEquipmentUnavailable()
    {
        Console.Write("Equipment ID: ");
        if (!Guid.TryParse(Console.ReadLine(), out var id)) { Console.WriteLine("Invalid ID."); return; }

        try
        {
            _equipmentService.SetUnavailable(id);
            Console.WriteLine("Equipment marked as unavailable.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private void SetEquipmentAvailable()
    {
        Console.Write("Equipment ID: ");
        if (!Guid.TryParse(Console.ReadLine(), out var id)) { Console.WriteLine("Invalid ID."); return; }

        try
        {
            _equipmentService.SetAvailable(id);
            Console.WriteLine("Equipment marked as available.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private void ShowActiveRentalsForUser()
    {
        Console.Write("User ID: ");
        if (!Guid.TryParse(Console.ReadLine(), out var userId)) { Console.WriteLine("Invalid ID."); return; }

        var rentals = _rentalService.GetUsersActiveRentals(userId);
        if (rentals.Count == 0) { Console.WriteLine("No active rentals."); return; }

        foreach (var r in rentals)
        {
            Console.WriteLine($"{r.Equipment.Name} - due: {r.DueDate:yyyy-MM-dd} - ID: {r.Id}");
        }
    }

    private void ShowOverdueRentals()
    {
        var rentals = _rentalService.GetOverdueRentals();
        if (rentals.Count == 0) { Console.WriteLine("No overdue rentals."); return; }

        foreach (var r in rentals)
        {
            Console.WriteLine($"{r.User.FirstName} {r.User.LastName} - {r.Equipment.Name} - due: {r.DueDate:yyyy-MM-dd} - ID: {r.Id}");
        }
    }
}

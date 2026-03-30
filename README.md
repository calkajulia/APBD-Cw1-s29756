# Academic Equipment Rental System

A CLI application written in C# for managing a university equipment rental system. Supports registering equipment and users, renting and returning devices, marking equipment as unavailable, tracking overdue rentals and generating reports.

## How to run

```bash
dotnet run
```

## Interactive menu

The application runs as a loop-based interactive console menu.

```
===== EQUIPMENT RENTAL SYSTEM =====
1.  Add user
2.  Add equipment
3.  Show all equipment
4.  Show available equipment
5.  Rent equipment
6.  Return equipment
7.  Mark equipment as unavailable
8.  Mark equipment as available
9.  Show active rentals for user
10. Show overdue rentals
11. Generate report
0.  Exit
```

## Design decisions

### Layer separation
- **Models**: hold data
- **Services**: contain all business logic and validate domain rules
- **UI**: reads input, calls services and prints results
- `Program.cs` only wires service instances together and launches `ConsoleUI`

### Cohesion
Each class has its own responsibility:
- `RentalService`: full rental lifecycle (validation, creating rentals, returns, penalties)
- `EquipmentService`: managing equipment
- `UserService`: managing users
- `ReportService`: generates a summary of the current system state
- `ConsoleUI`: launches the interactive menu and handles all console interaction

### Coupling
- Interfaces `IEquipmentService`, `IUserService` and `IRentalService` are used everywhere instead of concrete classes - this way the UI does not depend on specific implementations and they can be swapped freely
- `RentalService` does not depend on `EquipmentService` - `Equipment` objects are passed in as arguments by the caller, so the services stay independent of each other

### Rental limits and penalty rules
- `RentalsLimit` is an abstract property on `User` - `Student` always returns 2, `Employee` always returns 5 - the value cannot be set from outside, which prevents accidental misuse
- Penalty rate and default rental period are constants in `RentalPolicy` - business rules can be easily changed in one place without touching the rest of the code

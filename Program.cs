using APBD_Cw1_s29756.Services;
using APBD_Cw1_s29756.UI;

var equipmentService = new EquipmentService();
var userService = new UserService();
var rentalService = new RentalService();
var reportService = new ReportService(equipmentService, userService, rentalService);

var ui = new ConsoleUI(equipmentService, userService, rentalService, reportService);
ui.Run();

using APBD_Cw1_s29756.Enums;

namespace APBD_Cw1_s29756.Models;

public abstract class Equipment
{
    public Guid Id { get; }
    public string Name { get; set; }
    public string Brand { get; set; }
    public EquipmentStatus Status { get; set; }
    
    protected Equipment(string name, string brand)
    {
        Id = Guid.NewGuid();
        Name = name;
        Brand = brand;
        Status = EquipmentStatus.Available;
    }
}
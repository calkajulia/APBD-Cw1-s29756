using APBD_Cw1_s29756.Enums;
using APBD_Cw1_s29756.Exceptions;
using APBD_Cw1_s29756.Models;

namespace APBD_Cw1_s29756.Services;

public class EquipmentService : IEquipmentService
{
    private readonly List<Equipment> _equipment = [];

    public void Add(Equipment equipment)
    {
        _equipment.Add(equipment);
    }
    
    public Equipment GetById(Guid id)
    {
        return _equipment.FirstOrDefault(e => e.Id == id)
               ?? throw new EquipmentNotFoundException(id);
    }

    public List<Equipment> GetAll()
    {
        return _equipment.ToList();
    }

    public List<Equipment> GetAvailable()
    {
        return _equipment.Where(e => e.Status == EquipmentStatus.Available).ToList();
    }
    
    public void SetUnavailable(Guid id)
    {
        GetById(id).Status = EquipmentStatus.Unavailable;
    }

    public void SetAvailable(Guid id)
    {
        GetById(id).Status = EquipmentStatus.Available;
    }
}

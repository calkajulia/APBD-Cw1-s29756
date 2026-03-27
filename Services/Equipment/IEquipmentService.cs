using APBD_Cw1_s29756.Models;

namespace APBD_Cw1_s29756.Services;

public interface IEquipmentService
{
    void Add(Equipment equipment);
    Equipment GetById(Guid id);
    List<Equipment> GetAll();
    List<Equipment> GetAvailable();
    void SetUnavailable(Guid id);
    void SetAvailable(Guid id);
}

namespace APBD_Cw1_s29756.Exceptions;

public class EquipmentNotFoundException : Exception
{
    public EquipmentNotFoundException(Guid id)
        : base($"Equipment with id {id} not found.") { }
}

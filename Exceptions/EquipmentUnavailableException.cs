namespace APBD_Cw1_s29756.Exceptions;

public class EquipmentUnavailableException : Exception
{
    public EquipmentUnavailableException(string equipmentName)
        : base($"Equipment '{equipmentName}' is not available for rental.") { }
}

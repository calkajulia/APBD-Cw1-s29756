namespace APBD_Cw1_s29756.Exceptions;

public class RentalNotFoundException : Exception
{
    public RentalNotFoundException(Guid id)
        : base($"Rental with id {id} not found.") { }
}

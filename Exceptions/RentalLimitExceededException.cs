namespace APBD_Cw1_s29756.Exceptions;

public class RentalLimitExceededException : Exception
{
    public RentalLimitExceededException(string userName, int limit)
        : base($"User '{userName}' has reached the maximum rental limit of {limit}.") { }
}

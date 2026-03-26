namespace APBD_Cw1_s29756.Models;

public class Projector : Equipment
{
    public string Resolution { get; set; }
    public bool IsPortable { get; set; }

    public Projector(string name, string brand, string resolution, bool isPortable) 
        : base(name, brand)
    {
        Resolution = resolution;
        IsPortable = isPortable;
    }
}
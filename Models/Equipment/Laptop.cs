namespace APBD_Cw1_s29756.Models;

public class Laptop : Equipment
{
    public string OperatingSystem { get; set; }
    public string RamGb { get; set; }
    
    public Laptop(string name, string brand, string operatingSystem, string ramGb) 
        : base(name, brand)
    {
        OperatingSystem = operatingSystem;
        RamGb = ramGb;
    }
}
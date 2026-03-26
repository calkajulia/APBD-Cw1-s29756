namespace APBD_Cw1_s29756.Models;

public class Camera : Equipment
{
    public string RecordingFormat { get; set; }
    public bool IsWaterproof { get; set; }

    public Camera(string name, string brand, string recordingFormat, bool isWaterproof)
        : base(name, brand)
    {
        RecordingFormat = recordingFormat;
        IsWaterproof = isWaterproof;
    }
}
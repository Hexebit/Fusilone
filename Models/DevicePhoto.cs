namespace Fusilone.Models;

public class DevicePhoto
{
    public int Id { get; set; }
    public string DeviceId { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty; // Relative path stored in DB
    public string Description { get; set; } = string.Empty;
    
    // UI Binding Helper (not stored in DB)
    public string FullPath { get; set; } = string.Empty;
}

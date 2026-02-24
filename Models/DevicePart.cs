namespace Fusilone.Models;

public class DevicePart
{
    public int Id { get; set; }
    public string DeviceId { get; set; } = "";
    public string PartName { get; set; } = "";
    public string PartNumber { get; set; } = "";
    public int Quantity { get; set; }
    public string Status { get; set; } = "";
    public string Notes { get; set; } = "";
    public DateTime AddedAt { get; set; } = DateTime.Now;

    public string DisplayName => string.IsNullOrWhiteSpace(PartNumber) ? PartName : $"{PartName} ({PartNumber})";
}

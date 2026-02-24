using System;

namespace Fusilone.Models;

public class MaintenanceRecord
{
    public int Id { get; set; }
    public string DeviceId { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Cost { get; set; }
    public string Notes { get; set; } = string.Empty;
}

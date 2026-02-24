namespace Fusilone.Models;

public class PartMovement
{
    public int Id { get; set; }
    public string DeviceId { get; set; } = "";
    public string PartName { get; set; } = "";
    public string Action { get; set; } = "";
    public int Quantity { get; set; }
    public decimal Cost { get; set; }
    public string Notes { get; set; } = "";
    public DateTime Date { get; set; } = DateTime.Now;
}

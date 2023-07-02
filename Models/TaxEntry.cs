

namespace CongestionTaxCalculator.Models;

public class TaxEntry
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; }
    public DateTime EntryTime { get; set; }
    public DateTime ExitTime { get; set; }
    public decimal TaxAmount { get; set; }
}

namespace CongestionTaxCalculator.Models;

public class TaxConfiguration
{
    public int Id { get; set; }
    public decimal MaxDailyAmount { get; set; }
    public List<TaxBand> TaxBands { get; set; }
    public List<DateTime> PublicHolidays { get; set; }
}

public enum TollFreeVehicles
{
    Motorcycle = 0,
    Tractor = 1,
    Emergency = 2,
    Diplomat = 3,
    Foreign = 4,
    Military = 5
}
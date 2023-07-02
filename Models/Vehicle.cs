

namespace CongestionTaxCalculator.Models;

public class Vehicle
{
    public int Id { get; set; }
    public string RegistrationNumber { get; set; }
    public VehicleType Type { get; set; }


    public string GetVehicleType()
    {
        return Type.ToString();
    }
}
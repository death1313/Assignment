using CongestionTaxCalculator.Models;
using CongestionTaxCalculator.Services.Locations;

class Program
{
    static void Main(string[] args)
    {


        var calculator = new GothenburgTaxCalculator();

        var vehicle = new Vehicle
        {
            Type = VehicleType.Car,

        };


        var dates = new DateTime[]
        {
            new DateTime(2023, 7, 3, 8, 15, 0),
        };


        decimal taxFee = calculator.GetTax(vehicle, dates);

        var Config = calculator.GetLocationConfig();

        Console.WriteLine($"Total tax fee for {Config.Name}: {taxFee} {Config.Currency}");
    }
}
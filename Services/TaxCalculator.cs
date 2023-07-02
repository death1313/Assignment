using CongestionTaxCalculator.Models;


namespace CongestionTaxCalculator.Services;

abstract public class TaxCalculator
{

    protected abstract TaxConfiguration GetTaxConfiguration();

    public abstract LocationConfiguration GetLocationConfig();

    abstract public int GetTollFee(DateTime date, Vehicle vehicle);


    public int GetTax(Vehicle vehicle, DateTime[] dates)
    {
        int totalFee = 0;
        DateTime intervalStart = dates[0];

        foreach (DateTime date in dates)
        {
            int nextFee = GetTollFee(date, vehicle);
            int tempFee = GetTollFee(intervalStart, vehicle);

            double diffInMinutes = (date - intervalStart).TotalMinutes;

            if (diffInMinutes <= 60)
            {
                if (totalFee > 0) totalFee -= tempFee;
                if (nextFee >= tempFee) tempFee = nextFee;
                totalFee += tempFee;
            }
            else
            {
                totalFee += nextFee;
            }

            intervalStart = date;
        }

        if (totalFee > 60) totalFee = 60;
        return totalFee;
    }


    protected bool IsTollFreeDate(DateTime date)
    {
        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            return true;

        if (GetTaxConfiguration().PublicHolidays.Contains(date.Date))
            return true;

        return false;
    }

    protected bool IsTollFreeVehicle(Vehicle vehicle)
    {
        if (vehicle == null) return false;
        string vehicleType = vehicle.GetVehicleType();
        return Enum.IsDefined(typeof(TollFreeVehicles), vehicleType);
    }
}

using CongestionTaxCalculator.Models;
using Microsoft.Data.Sqlite;
using Dapper;

namespace CongestionTaxCalculator.Services.Locations;

public class GothenburgTaxCalculator : TaxCalculator
{
    public override LocationConfiguration GetLocationConfig()
    {
        return new LocationConfiguration()
        {
            Currency = "SEK",
            Name = "Gothenburg"
        };
    }

    public override int GetTollFee(DateTime date, Vehicle vehicle)
    {
        {
            if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

            int hour = date.Hour;
            int minute = date.Minute;

            if ((hour == 6 && minute >= 0 && minute <= 29) ||
                (hour == 8 && minute >= 30 && minute <= 59) ||
                (hour == 15 && minute >= 0 && minute <= 29) ||
                (hour == 17 && minute >= 0 && minute <= 59) ||
                (hour == 18 && minute >= 0 && minute <= 29))
            {
                return 8;
            }
            else if ((hour == 6 && minute >= 30 && minute <= 59) ||
                     (hour == 8 && minute >= 0 && minute <= 29) ||
                     (hour >= 9 && hour <= 14) ||
                     (hour == 15 && minute >= 30 && minute <= 59) ||
                     (hour == 16 && minute <= 59) ||
                     (hour == 18 && minute >= 30) ||
                     (hour >= 19 || hour <= 5))
            {
                return 13;
            }
            else if ((hour == 7 && minute >= 0 && minute <= 59) ||
                     (hour == 15 && minute >= 0 && minute <= 29))
            {
                return 18;
            }

            return 0;
        }
    }

    protected override TaxConfiguration GetTaxConfiguration()
    {
        var taxConfiguration = new TaxConfiguration();

        using (var connection = new SqliteConnection("Data Source=db.sqlite"))
        {
            PrepareData(connection);

            var publicHolidays = connection.Query<DateTime>("SELECT Date FROM PublicHolidays");
            taxConfiguration.PublicHolidays = publicHolidays.ToList();

        }

        //taxConfiguration.PublicHolidays = new List<DateTime> {
        //    new DateTime(2023, 1, 1),
        //    new DateTime(2023, 12, 25),
        //};
        return taxConfiguration;
    }

    private static void PrepareData(SqliteConnection connection)
    {
        connection.Open();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "DROP TABLE IF EXISTS PublicHolidays";
            command.ExecuteNonQuery();
            command.CommandText = @"
                               CREATE TABLE PublicHolidays(
                               Id INTEGER PRIMARY KEY,
                               Date TEXT
                               );";
            command.ExecuteNonQuery();
        }

        using (var command = connection.CreateCommand())
        {
            command.CommandText = @"
                INSERT INTO PublicHolidays (Date)
                VALUES ('2023-01-01'),
                       ('2023-07-03'),
                       ('2023-12-25');";
            command.ExecuteNonQuery();
        }
    }
}

using System;
using System.Linq;
using DarkSky.Models;

namespace ColourBlanket
{
    class Program
    {
        static void Main(string[] args)
        {
            var darkSky = new DarkSky.Services.DarkSkyService("API_KEY_HERE");

            var date = new DateTime(2020,1,1);

            do
            {
                var forecast = darkSky.GetForecast(53.812174, -1.096165,
                    new OptionalParameters{MeasurementUnits ="uk2", ForecastDateTime = date}).Result;

                var temperatureLow = Math.Round(forecast.Response.Daily.Data[0].TemperatureMin.Value, 0, MidpointRounding.AwayFromZero);
                var temperatureHigh = Math.Round(forecast.Response.Daily.Data[0].TemperatureMax.Value, 0,
                    MidpointRounding.AwayFromZero);
                var temperatureAverage =
                    Math.Round(forecast.Response.Hourly.Data.Select(x => x.Temperature.Value).Average(), 0, MidpointRounding.AwayFromZero);
                
                Console.WriteLine($"Date: {date.ToShortDateString()};   Min: {temperatureLow,2};   Max: {temperatureHigh,2};   Avg: {temperatureAverage,2}");
                
                date = date.AddDays(1);
            } while (date<DateTime.Today);
        }
    }
}
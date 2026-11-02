using WeatherMonitoring.Business.Models;

namespace WeatherMonitoring.Business.Parsers;

/// <summary>Converts raw input into weather data.</summary>
public interface IWeatherDataParser
{
    /// <summary>Returns a validated reading; throws for invalid input.</summary>
    WeatherData Parse(string input);
}
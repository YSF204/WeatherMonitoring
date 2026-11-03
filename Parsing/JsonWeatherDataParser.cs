using System.Text.Json;
using WeatherMonitoring.Models;

namespace WeatherMonitoring.Parsing;

public class JsonWeatherDataParser : IWeatherDataParser
{

    public WeatherData Parse(string input)
    {
        var weather = JsonSerializer.Deserialize<WeatherData>(input)
               ?? throw new JsonException("Weather data cannot be null.");
        weather.Validate();
        return weather;
    }
}

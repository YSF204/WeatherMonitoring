using System.Text.Json;
using WeatherMonitoring.Business.Models;
namespace WeatherMonitoring.Business.Parsers;

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
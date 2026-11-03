using System.Xml.Linq;
using WeatherMonitoring.Models;

namespace WeatherMonitoring.Parsing;

public class XmlWeatherDataParser : IWeatherDataParser
{
    public WeatherData Parse(string input)
    {
        var root = XElement.Parse(input);

        if (root.Name !=  "WeatherData") throw new FormatException("Root element must be WeatherData");

        var weather = new WeatherData
        {
            Location = (string?)root.Element("Location")
                       ?? throw new FormatException("Location is required."),

            Temperature = (double?)root.Element("Temperature")
                          ?? throw new FormatException("Temperature is required."),

            Humidity = (double?)root.Element("Humidity")
                       ?? throw new FormatException("Humidity is required.")
        };
            weather.Validate();
            return weather;
    }
}

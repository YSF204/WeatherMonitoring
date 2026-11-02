namespace WeatherMonitoring.Business.Models;

public class WeatherData
{
    public required string Location { get; init; }
    public required double Temperature { get; init; }
    public required double Humidity { get; init; }

    public void Validate()
    {
        if(string.IsNullOrWhiteSpace(Location)) throw new FormatException("Location is required");
        if(!double.IsFinite(Temperature)) throw new FormatException("Temperature must be a finite number");
        if(!double.IsFinite(Humidity) || Humidity < 0 || Humidity > 100) throw new FormatException("Humidity must be between 0 and 100");
    }
}
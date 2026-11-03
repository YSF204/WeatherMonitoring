namespace WeatherMonitoring.Configuration;

public class TemperatureBotConfiguration
{
    public required bool Enabled { get; init; }
    public required double TemperatureThreshold { get; init; }
    public required string Message { get; init; }
    public void Validate()
    {
        if(!double.IsFinite(TemperatureThreshold)) throw new FormatException("Temperature threshold must be finite");
        if(string.IsNullOrWhiteSpace(Message)) throw new FormatException("Message must not be blank");
    }
}

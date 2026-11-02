namespace WeatherMonitoring.Business.Models.Configurations;

public class HumidityBotConfiguration
{
    public required bool Enabled { get; init; }
    public required double HumidityThreshold { get; init; }
    public required string Message { get; init; }

    public void Validate()
    {
        if (!double.IsFinite(HumidityThreshold)
            || HumidityThreshold < 0
            || HumidityThreshold > 100)
        {
            throw new FormatException("Humidity threshold must be between 0 and 100");
        }
        if (string.IsNullOrWhiteSpace(Message)) throw new FormatException("Bot message must not be blank");
    }
}
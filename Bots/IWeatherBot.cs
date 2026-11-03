using WeatherMonitoring.Models;

namespace WeatherMonitoring.Bots;

/// <summary>Reacts to weather updates using configured rules.</summary>
public interface IWeatherBot
{
    /// <summary>The bot's display name.</summary>
    string Name { get; }

    /// <summary>Returns the activation message, or null when inactive. Rejects invalid readings.</summary>
    string? GetNotificationMessage(WeatherData weather);
}

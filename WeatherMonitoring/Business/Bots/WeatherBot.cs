using WeatherMonitoring.Business.Models;

namespace WeatherMonitoring.Business.Bots;

/// <summary>Shared weather update workflow for configured bots.</summary>
public abstract class WeatherBot : IWeatherBot
{
    public abstract string Name { get; }

    protected abstract bool IsEnabledInConfiguration { get; }

    protected abstract string ConfiguredMessage { get; }

    public string? GetNotificationMessage(WeatherData weather)
    {
        ArgumentNullException.ThrowIfNull(weather);
        weather.Validate();

        if (!IsEnabledInConfiguration) return null;

        return MatchesWeatherCondition(weather) ? ConfiguredMessage : null;
    }
    protected abstract bool MatchesWeatherCondition(WeatherData weather);
}

using WeatherMonitoring.Business.Models;
using WeatherMonitoring.Business.Models.Configurations;

namespace WeatherMonitoring.Business.Bots;

public class SnowBot : WeatherBot
{
    private readonly TemperatureBotConfiguration  _configuration;

    public SnowBot(TemperatureBotConfiguration  configurations)
    {
        ArgumentNullException.ThrowIfNull(configurations);
        configurations.Validate();
        
        _configuration = configurations;
    }
    public override string Name => "SnowBot";

    protected override bool IsEnabledInConfiguration => _configuration.Enabled;

    protected override string ConfiguredMessage => _configuration.Message;

    protected override bool MatchesWeatherCondition(WeatherData weather)
        => weather.Temperature < _configuration.TemperatureThreshold;
}
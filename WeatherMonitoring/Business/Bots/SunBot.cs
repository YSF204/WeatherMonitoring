using WeatherMonitoring.Business.Models;
using WeatherMonitoring.Business.Models.Configurations;

namespace WeatherMonitoring.Business.Bots;

public class SunBot : WeatherBot
{
    private readonly TemperatureBotConfiguration  _configuration;
    public SunBot(TemperatureBotConfiguration configurations)
    {
        ArgumentNullException.ThrowIfNull(configurations);
        configurations.Validate();
        
        _configuration = configurations;
    }
    
    public override string Name => "SunBot";

    protected override bool IsEnabledInConfiguration => _configuration.Enabled;

    protected override string ConfiguredMessage => _configuration.Message;

    protected override bool MatchesWeatherCondition(WeatherData weather)
        => weather.Temperature > _configuration.TemperatureThreshold;
}
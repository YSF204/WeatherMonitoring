using WeatherMonitoring.Business.Models;
using WeatherMonitoring.Business.Models.Configurations;
namespace WeatherMonitoring.Business.Bots;

public class RainBot : WeatherBot
{
    private readonly HumidityBotConfiguration _configuration;

    public RainBot(HumidityBotConfiguration configurations)
    {
        ArgumentNullException.ThrowIfNull(configurations);
        configurations.Validate();
        _configuration = configurations;
    }
    
    public override string Name => "RainBot";

    protected override bool IsEnabledInConfiguration => _configuration.Enabled;

    protected override string ConfiguredMessage => _configuration.Message;

    protected override bool MatchesWeatherCondition(WeatherData weather)
        => weather.Humidity > _configuration.HumidityThreshold;
}
using WeatherMonitoring.Business.Bots;

namespace WeatherMonitoring.Business.Models.Configurations;

public class WeatherBotConfiguration
{
    public required HumidityBotConfiguration RainBot  { get; init; }
    public required TemperatureBotConfiguration SunBot  { get; init; }
    public required TemperatureBotConfiguration SnowBot  { get; init; }

    public void Validate()
    {
        if(RainBot is null || SunBot is null || SnowBot is null)  throw new FormatException("Configurations must include all bots (Rain,Sun and Snow) bots");
        RainBot.Validate();
        SnowBot.Validate();
        SunBot.Validate();     
    }
}
using WeatherMonitoring.Business.Bots;
using WeatherMonitoring.Business.Models.Configurations;
using WeatherMonitoring.Business.Parsers;
using WeatherMonitoring.Business.Services;
using WeatherMonitoring.DataAccess.Repositories;
using WeatherMonitoring.Presentation;

var filePath = Path.Combine(AppContext.BaseDirectory,"bots.json");

var repository = new JsonConfigurationRepository(filePath);
var configurationService = new ConfigurationService(repository);

WeatherBotConfiguration configurations;

try
{
    configurations = configurationService.Load();
}catch (InvalidOperationException exception)
{
    Console.WriteLine($"Startup failed: {exception.Message}");
    return;
}
catch (FormatException exception)
{
    Console.WriteLine($"Invalid configuration: {exception.Message}");
    return;
}
catch (Exception exception)
{
    Console.Error.WriteLine("An unexpected error occurred.");
    Console.Error.WriteLine(exception);
    return;
} 

var weatherPublisher = new WeatherUpdatePublisher();
weatherPublisher.Subscribe(new RainBot(configurations.RainBot));
weatherPublisher.Subscribe(new SnowBot(configurations.SnowBot));
weatherPublisher.Subscribe(new SunBot(configurations.SunBot));

var parsers =  new Dictionary<char,IWeatherDataParser>
{
    {'{' , new JsonWeatherDataParser()},
    {'<', new XmlWeatherDataParser()}
};
var application = new ConsoleWeatherApplication(parsers, weatherPublisher);

application.Run();
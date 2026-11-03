using WeatherMonitoring.Bots;
using WeatherMonitoring.Configuration;
using WeatherMonitoring.ConsoleUI;
using WeatherMonitoring.Notifications;
using WeatherMonitoring.Parsing;

var filePath = Path.Combine(AppContext.BaseDirectory,"bots.json");

var configurationLoader = new JsonConfigurationLoader(filePath);

WeatherBotConfiguration configurations;

try
{
    configurations = configurationLoader.Load();
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

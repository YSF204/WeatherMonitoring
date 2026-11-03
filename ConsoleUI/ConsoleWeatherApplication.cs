using System.Text.Json;
using System.Xml;
using WeatherMonitoring.Notifications;
using WeatherMonitoring.Parsing;

namespace WeatherMonitoring.ConsoleUI;

public class ConsoleWeatherApplication
{
    private readonly Dictionary<char, IWeatherDataParser> _parsers;
    private readonly WeatherUpdatePublisher _weatherPublisher;

    public ConsoleWeatherApplication(
        Dictionary<char, IWeatherDataParser> parsers,
        WeatherUpdatePublisher weatherPublisher)
    {
        _parsers = parsers;
        _weatherPublisher = weatherPublisher;
    }

    public void Run()
    {
        Console.WriteLine("Enter one JSON or XML Per line , type exit to quit the application ");

        while (true)
        {
            Console.WriteLine("Enter weather Data");
            var input = Console.ReadLine();
            if (input is null) break;
            input = input.Trim();

            if (input.Equals("exit", StringComparison.InvariantCultureIgnoreCase)) break;

            if (input.Length == 0)
            {
                Console.WriteLine("Please enter a weather data");
                continue;
            }

            if (!_parsers.TryGetValue(input[0], out var parser))
            {
                Console.WriteLine("Please enter only JSON OR XML");
                continue;
            }

            try
            {
                var weather = parser.Parse(input);
                var notifications = _weatherPublisher.NotifyBots(weather);

                foreach (var notification in notifications)
                {
                    Console.WriteLine($"{notification.BotName} activated!");
                    Console.WriteLine(
                        $"{notification.BotName}: \"{notification.Message}\"");
                }

                if (notifications.Count == 0)
                {
                    Console.WriteLine("No bots acctivated");
                }
            }
            catch (JsonException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (XmlException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (FormatException exception)
            {
                Console.WriteLine($"Invalid weather data: {exception.Message}");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}

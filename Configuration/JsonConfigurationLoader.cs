using System.Text.Json;

namespace WeatherMonitoring.Configuration;

public class JsonConfigurationLoader
{
    private readonly string _filePath;

    public JsonConfigurationLoader(string filePath)
    {
        _filePath = filePath;
    }

    public WeatherBotConfiguration Load()
    {
        WeatherBotConfiguration? configurations;

        try
        {
            var json = File.ReadAllText(_filePath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            configurations = JsonSerializer.Deserialize<WeatherBotConfiguration>(json, options);
        }
        catch (IOException exception)
        {
            throw new InvalidOperationException("could not read the config file", exception);
        }
        catch (UnauthorizedAccessException exception)
        {
            throw new InvalidOperationException(
                "Access to the configuration file was denied.", exception);
        }
        catch (JsonException exception)
        {
            throw new InvalidOperationException(
                "The configuration file contains invalid JSON settings.", exception);
        }

        if (configurations is null)
        {
            throw new InvalidOperationException("The configuration file contains not settings.");
        }
        configurations.Validate();
        return configurations;
    }
}

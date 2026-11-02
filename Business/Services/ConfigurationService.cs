using System.Text.Json;
using WeatherMonitoring.Business.Models.Configurations;
using WeatherMonitoring.DataAccess.Repositories;

namespace WeatherMonitoring.Business.Services;

public class ConfigurationService
{
    private readonly IConfigurationsRepository _repository;

    public ConfigurationService(IConfigurationsRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);
        _repository = repository;
    }

    public WeatherBotConfiguration Load()
    {
        WeatherBotConfiguration? configurations;

        try
        {
            configurations = _repository.Load();
        }
        catch (IOException exception)
        {
            throw new InvalidOperationException("could not read the config file", exception);
        }
        catch (UnauthorizedAccessException exception)
        {
            throw new InvalidOperationException(
                "Access to the configuration file was denied.", exception);
        } catch (JsonException exception)
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
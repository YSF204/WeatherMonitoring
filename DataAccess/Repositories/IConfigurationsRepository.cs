using WeatherMonitoring.Business.Models.Configurations;

namespace WeatherMonitoring.DataAccess.Repositories;

/// <summary>Reads stored bot settings.</summary>
public interface IConfigurationsRepository
{
    /// <summary>Loads settings, possibly null. The service handles failures and validation.</summary>
    WeatherBotConfiguration? Load();
}
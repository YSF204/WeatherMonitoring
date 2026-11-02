using System.Text.Json;
using WeatherMonitoring.Business.Models.Configurations;

namespace WeatherMonitoring.DataAccess.Repositories;

public class JsonConfigurationRepository : IConfigurationsRepository
{
    private readonly string _filePath;

    public JsonConfigurationRepository(string filePath)
    {
        _filePath = filePath;
    }
    
    public WeatherBotConfiguration? Load()
    {
            var json = File.ReadAllText(_filePath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            return JsonSerializer.Deserialize<WeatherBotConfiguration>(json, options);
        
    }
}
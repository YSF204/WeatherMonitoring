using WeatherMonitoring.Bots;
using WeatherMonitoring.Models;

namespace WeatherMonitoring.Notifications;

public class WeatherUpdatePublisher
{
    private readonly List<IWeatherBot> _subscribedBots = new();

    public void Subscribe(IWeatherBot bot)
    {
        ArgumentNullException.ThrowIfNull(bot);

        if (!_subscribedBots.Contains(bot))
        {
            _subscribedBots.Add(bot);
        }
    }

    public void Unsubscribe(IWeatherBot bot)
    {
        ArgumentNullException.ThrowIfNull(bot);
        if (_subscribedBots.Contains(bot))
        {
            _subscribedBots.Remove(bot);
        }
    }

    public List<BotNotification> NotifyBots(WeatherData weather)
    {
        ArgumentNullException.ThrowIfNull(weather);
        weather.Validate();

        var notifications = new List<BotNotification>();
        foreach (var bot in _subscribedBots)
        {
            var messsage = bot.GetNotificationMessage(weather);
            if (messsage != null)
            {
                notifications.Add(new BotNotification
                {
                    BotName = bot.Name,
                    Message = messsage,
                });
            }
        }
        return notifications;
    }
}

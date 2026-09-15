using AppointmentService.Application.Interfaces;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace AppointmentService.Infrastructure.Services;

public class RabbitMqEventPublisher : IEventPublisher
{
    private readonly IConnection _connection;

    public RabbitMqEventPublisher(IConnection connection)
    {
        _connection = connection;
    }

    public async Task PublishAsync<T>(string eventName, T message)
    {
        using var channel = await _connection.CreateChannelAsync();

        // Declaramos el exchange — así RabbitMQ sabe dónde enrutar el mensaje
        await channel.ExchangeDeclareAsync(
            exchange: "medic_events",
            type: ExchangeType.Topic,
            durable: true);

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        // Publicamos el evento con su routing key (ej: "appointment.created")
        await channel.BasicPublishAsync(
            exchange: "medic_events",
            routingKey: eventName,
            body: body);
    }
}
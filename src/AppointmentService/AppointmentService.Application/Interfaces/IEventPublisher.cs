using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentService.Application.Interfaces
{
    // Contrato para publicar eventos en RabbitMQ
    // Application no sabe que existe RabbitMQ — solo sabe que puede publicar eventos
    public interface IEventPublisher
    {
        Task PublishAsync<T>(string eventName, T message);
    }

}

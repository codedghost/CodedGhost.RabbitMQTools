using CodedGhost.RabbitMQTools.Models;

namespace CodedGhost.RabbitMQTools.Interfaces
{
    public interface IRabbitMessagePublisher
    {
        void Publish<T>(T rabbitMessage) where T: RabbitMessage;
    }
}
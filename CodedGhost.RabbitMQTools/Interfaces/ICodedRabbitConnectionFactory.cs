using RabbitMQ.Client;

namespace CodedGhost.RabbitMQTools.Interfaces
{
    public interface ICodedRabbitConnectionFactory
    {
        IConnection GetRabbitConnection();
    }
}
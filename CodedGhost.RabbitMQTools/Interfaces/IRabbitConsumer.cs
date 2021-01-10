using System.Threading.Tasks;

namespace CodedGhost.RabbitMQTools.Interfaces
{
    public interface IRabbitConsumer
    {

    }

    public interface IRabbitConsumer<T> : IRabbitConsumer
    {
        public abstract Task ProcessMessage(T message);
    }
}
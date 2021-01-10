using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CodedGhost.RabbitMQTools.Interfaces;
using CodedGhost.RabbitMQTools.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CodedGhost.RabbitMQTools.Abstractions
{
    public abstract class RabbitConsumer<T>: IRabbitConsumer<T> where T: RabbitMessage
    {
        private IConnection _connection;
        private IModel _channel;

        public RabbitConsumer(ICodedRabbitConnectionFactory codedConnectionFactory)
        {
            _connection = codedConnectionFactory.GetRabbitConnection();
            _channel = _connection.CreateModel();

            RegisterConsumer();
        }

        private void RegisterConsumer()
        {
            var queueName = $"{typeof(T).FullName}Queue";

            _channel.QueueDeclare(queueName, true, false, false, null);

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += ConsumeMessage;

            _channel.BasicConsume(queueName, false, consumer);
        }

        private async void ConsumeMessage(object sender, BasicDeliverEventArgs e)
        {
            try
            {
                var messageJson = Encoding.UTF8.GetString(e.Body.ToArray());

                var messageModel = JsonSerializer.Deserialize<T>(messageJson);

                await ProcessMessage(messageModel);

                ((EventingBasicConsumer)sender).Model.BasicAck(e.DeliveryTag, false);
            }
            catch (Exception ex)
            {

            }
        }

        public abstract Task ProcessMessage(T message);
    }
}

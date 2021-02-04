using System;
using System.Text;
using System.Text.Json;
using CodedGhost.RabbitMQTools.Interfaces;
using CodedGhost.RabbitMQTools.Models;
using RabbitMQ.Client;

namespace CodedGhost.RabbitMQTools.Abstractions
{
    public class RabbitMessagePublisher : IRabbitMessagePublisher
    {
        private readonly ICodedRabbitConnectionFactory _codedConnectionFactory;

        public RabbitMessagePublisher(ICodedRabbitConnectionFactory codedConnectionFactory)
        {
            _codedConnectionFactory = codedConnectionFactory;
        }

        public void Publish<T>(T rabbitMessage) where T: RabbitMessage
        {
            var connection = _codedConnectionFactory.GetRabbitConnection();

            using (var channel = connection.CreateModel())
            {
                var queueName = $"{rabbitMessage.GetType().FullName}Queue";
                channel.QueueDeclare(
                    queueName,
                    true,
                    false,
                    false,
                    null);
                
                var messageJson = JsonSerializer.Serialize(rabbitMessage);

                var messageBody = Encoding.UTF8.GetBytes(messageJson);

                channel.BasicPublish(string.Empty, queueName, null, messageBody);

                Console.WriteLine($"Sent message {messageJson}");
            }
        }
    }
}
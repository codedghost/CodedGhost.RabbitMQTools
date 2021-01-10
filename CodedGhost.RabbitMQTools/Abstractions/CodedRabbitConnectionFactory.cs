using System;
using System.Collections.Generic;
using CodedGhost.RabbitMQTools.Interfaces;
using CoreCodedChatbot.Secrets;
using RabbitMQ.Client;

namespace CodedGhost.RabbitMQTools.Abstractions
{
    public class CodedRabbitConnectionFactory : ICodedRabbitConnectionFactory
    {
        private IConnection _connection;

        public CodedRabbitConnectionFactory(ISecretService secretService)
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = secretService.GetSecret<string>("RabbitHostName"),
                Port = secretService.GetSecret<int>("RabbitPortNumber"),
                UserName = secretService.GetSecret<string>("RabbitUsername"),
                Password = secretService.GetSecret<string>("RabbitPassword")
            };

            _connection = connectionFactory.CreateConnection();
        }

        public IConnection GetRabbitConnection()
        {
            return _connection;
        }
    }
}
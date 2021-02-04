﻿using CodedGhost.RabbitMQTools.Models;

namespace CodedGhost.RabbitMQTools.Interfaces
{
    public interface IRabbitMessagePublisher
    {
        void Publish(IRabbitMessage rabbitMessage);
    }
}
﻿using System;

namespace CodedGhost.RabbitMQTools.Models
{
    public abstract class RabbitMessage
    {
        public RabbitMessage()
        {
            MessageId = Guid.NewGuid();
        }

        public RabbitMessage(Guid id)
        {
            MessageId = id;
        }

        public Guid MessageId { get; set; }
    }
}
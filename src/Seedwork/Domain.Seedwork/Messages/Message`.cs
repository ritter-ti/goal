using System;
using MediatR;

namespace Goal.Domain.Seedwork.Messages
{
    public abstract class Message<T> : IRequest<T>, IMessage
    {
        public string MessageType { get; protected set; }
        public Guid AggregateId { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}

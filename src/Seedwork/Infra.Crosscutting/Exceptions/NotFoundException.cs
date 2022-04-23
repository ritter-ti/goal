using System;
using System.Runtime.Serialization;

namespace Goal.Infra.Crosscutting.Exceptions
{
    [Serializable]
    public class NotFoundException : ApplicationException
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string message)
            : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected NotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

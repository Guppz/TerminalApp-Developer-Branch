using ENTITIES_POJO;
using System;
using System.Runtime.Serialization;

namespace TerminalApp.Controllers
{
    [Serializable]
    internal class BussinessException : Exception
    {
        public int ExceptionId;
        public AppMessage AppMessage { get; set; }

        public BussinessException()
        {
        }

        public BussinessException(string message) : base(message)
        {
        }

        public BussinessException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BussinessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
using ENTITIES_POJO;
using System;

namespace Exceptions
{
    public class BusinessException : Exception
    {
        public int ExceptionId;
        public AppMessage AppMessage { get; set; }

        public BusinessException()
        {

        }

        public BusinessException(int exceptionId)
        {
            ExceptionId = exceptionId;
        }

        public BusinessException(int exceptionId, Exception innerException)
        {
            ExceptionId = exceptionId;
        }
    }
}

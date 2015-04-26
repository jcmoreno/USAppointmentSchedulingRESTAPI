using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace US.AppointmentScheduling.RESTAPI.DAO.Exceptions
{
    [Serializable]
    public class InvalidRangeException : ApplicationException
    {
        public InvalidRangeException() { }
        public InvalidRangeException(string message) : base(message) { }
        public InvalidRangeException(string message, Exception inner) : base(message, inner) { }
        protected InvalidRangeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    [Serializable]
    public class RecordNotFoundException : Exception
    {
        public RecordNotFoundException() { }
        public RecordNotFoundException(string message) : base(message) { }
        public RecordNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected RecordNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewApi.Common.Exceptions
{
    [Serializable]
    public class ProcessingException : Exception
    {
        public ProcessingException(string message) : base(message)
        {

        }

        public ProcessingException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

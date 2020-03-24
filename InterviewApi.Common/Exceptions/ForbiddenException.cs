using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewApi.Common.Exceptions
{
    [Serializable]
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string message) : base(message)
        {

        }

        public ForbiddenException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
